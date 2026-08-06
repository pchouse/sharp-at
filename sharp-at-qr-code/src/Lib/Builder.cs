/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Globalization;
using System.Reflection;
using System.Xml.Serialization;
using Microsoft.Extensions.Logging;
using PChouse.AT.Generics;
using PChouse.AT.QRCode.Library;


namespace PChouse.AT.QRCode;

public class Builder : IBuilder
{

    /// <summary>
    /// The instance logger
    /// </summary>
    protected ILogger Logger;

    /// <summary>
    /// The generic validator to validate the Portuguese TIN
    /// </summary>
    protected IValidator Validator;

    /// <summary>
    /// The QR code string builder
    /// </summary>
    /// <param name="logger">The LogFactory</param>
    /// <param name="validator">The generic validator to validate TIN</param>
    public Builder(ILogger<Builder> logger, IValidator validator)
    {
        Logger = logger;
        Logger.LogDebug($"Instance of {nameof(Builder)} created");
        Validator = validator;
    }

    /// <summary>
    /// Build the String to the QRCode asynchronous
    /// </summary>
    /// <param name="document"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public virtual Task<BuildStringAsyncResult> BuildStringAsync(ADocument document, BuilderOptions options)
    {
        return Task.Run(() =>
        {
            var errors = new List<ErrorField>();
            var qrCodeString = BuildString(ref errors, document, options);

            return new BuildStringAsyncResult { QrCodeString = qrCodeString, Errors = errors };
        });
    }

    /// <summary>
    /// Build the QRcode string
    /// </summary>
    /// <param name="errors"></param>
    /// <param name="document"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public virtual string BuildString(ref List<ErrorField> errors, ADocument document, BuilderOptions options)
    {
        var qrCodeString = BuildString(document, options, out var buildErrors);
        errors.AddRange(buildErrors);
        return qrCodeString;
    }

    /// <summary>
    /// Build the QRcode string 
    /// </summary>
    /// <param name="document"></param>
    /// <param name="options"></param>
    /// <param name="errors"></param>
    /// <returns></returns>
    /// <exception cref="BuilderException"></exception>
    public virtual string BuildString(ADocument document, BuilderOptions options, out List<ErrorField> errors)
    {
        errors = [];

        Logger.LogTrace($"Start building qr code string");

        TinValidations(document, options, errors);

        var properties = Extract(document);

        if (!properties.Any())
        {
            Logger.LogError($"Instance of {document.GetType().Name} does not have QRcode fields");
            throw new BuilderException($"Instance of {document.GetType().Name} does not have QRcode fields");

        }

        if (options.ValidateDocumentNumber && !Validator.DocumentNumber(document.DocumentNumber, out var documentNumberError))
        {

            var documentNumberErrorField = new ErrorField
            {
                Error = Error.DOCUMENT_NUMBER,
                Field = typeof(Document).GetProperty(nameof(Document.DocumentNumber))!.GetCustomAttribute<FieldAttribute>()!
            };

            errors.Add(documentNumberErrorField);

            if (options.ThrowExceptionOnError)
            {
                throw new BuilderFieldException(documentNumberError?.ToString())
                {
                    Error = documentNumberErrorField.Error,
                    Field = documentNumberErrorField.Field
                };
            }

        }

        var qrCodeStringStack = new List<string>();

        foreach (var property in properties)
        {
            var info = property.PropertyInfo;
            var field = property.Field;
            var fieldName = field.Fields[0];
            string? ifEmptyValue = string.IsNullOrEmpty(field.IfEmptyValue) ? null : field.IfEmptyValue;

            var value = property.PropertyInfo.GetValue(document);

            if (value is null)
            {
                Logger.LogInformation($"Property '{info?.Name}' for Field {fieldName} was null");
                TestNullOrEmptyMandatory(property, 0, options, errors);

                if (ifEmptyValue is not null)
                {
                    var valueBuild = $"{property.Field.Fields[0]}:{ifEmptyValue}";

                    Logger.LogInformation($"Value was empty build with default: '{valueBuild}'");

                    qrCodeStringStack.Add(valueBuild);
                }

                continue;
            }
            if (value is decimal @decimal)
            {
                AddDecimal(@decimal, property, 0, options, qrCodeStringStack, errors);
                continue;
            }

            if (value is string @string)
            {
                if (string.IsNullOrEmpty(@string))
                {
                    TestNullOrEmptyMandatory(property, 0, options, errors);

                    if (ifEmptyValue is null) continue;

                    @string = ifEmptyValue;
                }

                TestValueLength(@string, property, 0, options, errors);

                var valueBuild = $"{fieldName}:{@string}";

                Logger.LogInformation(valueBuild);

                qrCodeStringStack.Add(valueBuild);

                continue;
            }

            if (value is Enum @enum)
            {
                AddEnum(@enum, property.Field, 0, qrCodeStringStack);
                continue;
            }

            if (value is DateOnly @dateOnly)
            {
                var valueBuild = $"{fieldName}:{@dateOnly.ToString("yyyyMMdd", CultureInfo.InvariantCulture)}";

                Logger.LogInformation(valueBuild);

                qrCodeStringStack.Add(valueBuild);

                continue;
            }

            if (value is IEnumerable<Vat> vats)
            {
                if (!vats.Any())
                {
                    var valueBuild = $"{fieldName}:{ifEmptyValue ?? "0"}";

                    Logger.LogInformation(valueBuild);

                    qrCodeStringStack.Add(valueBuild);

                    continue;
                }

                var vatIndex = -1;

                var fieldsName = new[] { "I", "J", "K" };

                foreach (var vat in vats)
                {
                    vatIndex++;

                    ValidateVat(vat, vatIndex, fieldsName[vatIndex], options, errors);

                    var vatProperties = Extract(vat);

                    foreach (var vatProperty in vatProperties)
                    {
                        var vatValue = vatProperty.PropertyInfo.GetValue(vat);

                        if (vatValue is null) continue;

                        if (vatValue is Enum @vatEnum)
                        {
                            AddEnum(@vatEnum, vatProperty.Field, vatIndex, qrCodeStringStack);
                        }
                        else if (vatValue is decimal @vatDecimal)
                        {
                            AddDecimal(@vatDecimal, vatProperty, vatIndex, options, qrCodeStringStack, errors);
                        }
                        else
                        {
                            Logger.LogError($"Library is missing parser for type {vatValue.GetType().Name} in {nameof(Vat)}");
                            throw new BuilderException($"Library is missing parser for type {vatValue.GetType().Name} in {nameof(Vat)}");
                        }
                    }
                }

                continue;
            }

            if (value is int @int)
            {

                if (@int < 0)
                {
                    Logger.LogError($"Field {fieldName} of property {info.Name} has a negative value");

                    errors.Add(new ErrorField { Error = Error.NEGATIVE_INTEGER, Field = field });

                    if (options.ThrowExceptionOnError)
                    {
                        throw new BuilderFieldException($"Field {fieldName} of property {info.Name} has A negative value")
                        {
                            Field = field,
                            Error = Error.NEGATIVE_INTEGER
                        };
                    }
                }

                var valueBuild = $"{fieldName}:{@int}";

                Logger.LogInformation(valueBuild);

                qrCodeStringStack.Add(valueBuild);

                continue;
            }

            if (value is IOtherInfo @otherInfo)
            {

                if (string.IsNullOrEmpty(@otherInfo.Info())) continue;

                var valueBuild = $"{fieldName}:{@otherInfo.Info()?.Replace('*', ';').Replace(':', ';')}";

                Logger.LogInformation(valueBuild);

                qrCodeStringStack.Add(valueBuild);

                continue;
            }

            Logger.LogError($"Library is missing parser for type {value.GetType().Name}");
            throw new BuilderException($"Library is missing parser for type {value.GetType().Name}");
        }

        var qrCodeString = string.Join('*', qrCodeStringStack).Trim();

        Logger.LogError($"Builded: ${qrCodeString}");

        return qrCodeString;
    }

    /// <summary>
    /// Tin validations
    /// </summary>
    /// <param name="document"></param>
    /// <param name="options"></param>
    /// <param name="errors"></param>
    /// <exception cref="BuilderException"></exception>
    protected void TinValidations(ADocument document, BuilderOptions options, List<ErrorField> errors)
    {
        string issuerTin = document.IssuerTin;
        string? buyerTin = document.BuyerTin;
        Country? country = document.CountryCode;

        if (options.ValidateIssuerTin != TinValidation.NONE)
        {

            if (!Validator.PortugueseTin(issuerTin, options.ValidateIssuerTin == TinValidation.STRICT, out var tinError))
            {
                var msg = $"Issuer tin {issuerTin} fail validation with error {tinError}";

                Logger.LogError(msg);

                errors.Add(new ErrorField
                {
                    Error = Error.ISSUER_TIN_VALIDATION,
                    Field = typeof(Document).GetProperty(nameof(Document.IssuerTin))!.GetCustomAttribute<FieldAttribute>()!
                });

                if (options.ThrowExceptionOnError)
                {
                    throw new BuilderException(msg)
                    {
                        Error = Error.ISSUER_TIN_VALIDATION,
                    };
                }
            }
        }

        if (options.ValidateBuyerPortugueseTin != TinValidation.NONE && country == Country.PT && !string.IsNullOrEmpty(buyerTin))
        {
            if (!Validator.PortugueseTin(buyerTin, options.ValidateBuyerPortugueseTin == TinValidation.STRICT, out var tinError))
            {
                var msg = $"Buyer tin {buyerTin} fail validation with error {tinError}";

                errors.Add(new ErrorField
                {
                    Error = Error.BUYER_TIN_VALIDATION,
                    Field = typeof(Document).GetProperty(nameof(Document.BuyerTin))!.GetCustomAttribute<FieldAttribute>()!
                });

                Logger.LogError(msg);

                if (options.ThrowExceptionOnError)
                {
                    throw new BuilderException(msg)
                    {
                        Error = Error.BUYER_TIN_VALIDATION,
                    };
                }
            }
        }
    }

    /// <summary>
    /// Extract PropertyInfo and FieldAttribute of instance properties
    /// </summary>
    /// <param name="instance">The instance to be extracted</param>
    /// <returns></returns>
    protected List<Properties> Extract(object instance)
    {
        Logger.LogTrace($"Starting extract properties of a instance of {instance.GetType().Name}");

        var propertiesInfo = instance.GetType().GetProperties(
            BindingFlags.Public | BindingFlags.Instance
            );


        var list = new List<Properties>();

        Logger.LogTrace($"Instance {instance.GetType().Name} has ${propertiesInfo.Length} properties");

        foreach (var propertyInfo in propertiesInfo)
        {
            if (propertyInfo is null) continue;

            Logger.LogInformation($"Instance {instance.GetType().Name} extracting for ${propertyInfo.Name} property");

            var field = propertyInfo.GetCustomAttribute<FieldAttribute>();

            if (field is null)
            {
                Logger.LogInformation($"Instance {instance.GetType().Name} property ${propertyInfo.Name} does not have FieldAttribute");
                continue;
            }

            list.Add(new Properties { PropertyInfo = propertyInfo, Field = field });

            Logger.LogTrace($"Instance {instance.GetType().Name} property ${propertyInfo.Name} FieldAttribute extracted");
        }

        return [.. list.OrderBy(p => p.Field.Position)];
    }

    /// <summary>
    /// Test for empty or null for mandatory fields
    /// </summary>
    /// <param name="properties"></param>
    /// <param name="fieldIndex"></param>
    /// <param name="options"></param>
    /// <param name="errors"></param>
    /// <exception cref="BuilderException"></exception>
    /// <exception cref="BuilderFieldException"></exception>
    protected void TestNullOrEmptyMandatory(Properties properties, int fieldIndex, BuilderOptions options, List<ErrorField> errors)
    {
        var info = properties.PropertyInfo;
        var field = properties.Field;
        var fieldName = field.Fields[fieldIndex] ?? throw new BuilderException($"Fields does no has the index {fieldIndex}");

        string? ifEmptyValue = string.IsNullOrEmpty(field.IfEmptyValue) ? null : field.IfEmptyValue;

        if (field.IsMandatory && ifEmptyValue is null)
        {
            Logger.LogError($"Field {fieldName} of property {info.Name} has no value and is mandatory");

            errors.Add(new ErrorField { Error = Error.MANDATORY_FIELD_WITHOUT_VALUE, Field = field });

            if (options.ThrowExceptionOnError)
            {
                throw new BuilderFieldException($"Field {fieldName} of property {info.Name} has no value and is mandatory")
                {
                    Field = field,
                    Error = Error.MANDATORY_FIELD_WITHOUT_VALUE
                };
            }
        }
    }

    /// <summary>
    /// Test the length of the value after being converted to string
    /// </summary>
    /// <param name="value"></param>
    /// <param name="field"></param>
    /// <param name="fieldIndex"></param>
    /// <param name="propertyInfo"></param>
    /// <param name="options"></param>
    /// <param name="errors"></param>
    /// <exception cref="BuilderFieldException"></exception>
    protected void TestValueLength(
        string value,
        Properties properties,
        int fieldIndex,
        BuilderOptions options,
        List<ErrorField> errors
        )
    {
        Logger.LogTrace($"Testing length of string value of property {properties.PropertyInfo.Name} for field {properties.Field.Fields[fieldIndex]}");

        if (value.Length <= properties.Field.MaximLength) return;

        errors.Add(new ErrorField { Error = Error.FAIL_LENGTH, Field = properties.Field });

        var msg = $"Field {properties.Field.Fields[fieldIndex]} of property {properties.PropertyInfo.Name} has {value.Length} characters and maxim length is {properties.Field.MaximLength}";

        Logger.LogError(msg);

        if (options.ThrowExceptionOnError) throw new BuilderFieldException(msg)
        {
            Error = Error.FAIL_LENGTH,
            Field = properties.Field
        };

    }

    /// <summary>
    /// Add a decimal value to the stack
    /// </summary>
    /// <param name="value"></param>
    /// <param name="property"></param>
    /// <param name="fieldIndex"></param>
    /// <param name="options"></param>
    /// <param name="qrCodeStringStack"></param>
    /// <param name="errors"></param>
    /// <exception cref="BuilderFieldException"></exception>
    protected void AddDecimal(
        decimal value,
        Properties property,
        int fieldIndex,
        BuilderOptions options,
        List<string> qrCodeStringStack,
        List<ErrorField> errors
        )
    {
        if (value < 0)
        {
            var msg = $"Field {property.Field.Fields[fieldIndex]} of property {property.PropertyInfo.Name} has a negative decimal value";

            Logger.LogDebug(msg);

            errors.Add(new ErrorField { Error = Error.NEGATIVE_DECIMAL, Field = property.Field });

            if (options.ThrowExceptionOnError) throw new BuilderFieldException(msg)
            {
                Error = Error.NEGATIVE_DECIMAL,
                Field = property.Field
            };

            return;
        }

        if (value == 0m && property.Field.ZeroIsEmpty)
        {
            TestNullOrEmptyMandatory(property, 0, options, errors);
            return;
        }

        var valueString = value.ToString("F2", CultureInfo.InvariantCulture);

        TestValueLength(valueString, property, 0, options, errors);

        var valueBuild = $"{property.Field.Fields[fieldIndex]}:{valueString}";

        Logger.LogInformation(valueBuild);

        qrCodeStringStack.Add(valueBuild);
    }

    /// <summary>
    /// Add a enum value to the stack
    /// </summary>
    /// <param name="value">The field value (Enum)</param>
    /// <param name="field">The field attribute</param>
    /// <param name="fieldIndex">The field name (token) in the The field attribute Fields array</param>
    /// <param name="qrCodeStringStack">The QR code field builded</param>
    /// <exception cref="BuilderException"></exception>
    protected void AddEnum(
        Enum value,
        FieldAttribute field,
        int fieldIndex,
        List<string> qrCodeStringStack
        )
    {

        var valueBuild = string.Empty;

        if (value is DocumentType || value is DocumentStatus)
        {
            valueBuild = $"{field.Fields[fieldIndex]}:{value}";
        }
        else
        {
            // Get value of the SAF-T(PT) xml value
            var xmlEnum = value.GetType().GetField(value.ToString())?.GetCustomAttribute<XmlEnumAttribute>()
                    ?? throw new BuilderException($"Enum {value.GetType().Name} does not have the {nameof(XmlEnumAttribute)}");

            valueBuild = $"{field.Fields[fieldIndex]}:{xmlEnum.Name}";
        }

        Logger.LogInformation(valueBuild);

        qrCodeStringStack.Add(valueBuild);
    }

    /// <summary>
    /// Validate if all VAT fields are empty, except TaxCountryRegion
    /// </summary>
    /// <param name="vat">The Vat instance to validate</param>
    /// <param name="vatIndex">The vat in stack</param>
    /// <param name="fieldName">The field name (token)</param>
    /// <param name="options">The builder options</param>
    /// <param name="errors">The errors list where to add the errors</param>
    /// <exception cref="BuilderFieldException"></exception>
    protected void ValidateVat(
        Vat vat,
        int vatIndex,
        string fieldName,
        BuilderOptions options,
        List<ErrorField> errors
    )
    {
        Logger.LogTrace($"Starting to validate {nameof(Vat)} in index {vatIndex}");

        var hasVatValues = typeof(Vat).GetProperties().Any(p => p.GetValue(vat) is decimal);

        if (hasVatValues) return;

        var fieldAttribute = new FieldAttribute
        {
            Fields = [fieldName],
            Position = 0,
            MaximLength = 0,
            IsMandatory = false,
        };

        errors.Add(new ErrorField
        {
            Error = Error.VAT_VALUES_ALL_EMPTY,
            Field = fieldAttribute
        });

        Logger.LogError($"Vat is empty for index {vatIndex}");

        if (options.ThrowExceptionOnError)
        {
            throw new BuilderFieldException($"Vat is empty for field {fieldName}")
            {
                Error = Error.VAT_VALUES_ALL_EMPTY,
                Field = fieldAttribute
            };
        }
    }
}

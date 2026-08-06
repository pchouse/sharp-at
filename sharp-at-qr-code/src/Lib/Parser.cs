/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using PChouse.AT.Generics;
using PChouse.AT.QRCode.Library;

namespace PChouse.AT.QRCode;


/// <summary>
/// Parse a QRCode string
/// </summary>
public partial class Parser(ILogger<Parser> logger, IEnumParse enumParse) : IParser
{

    /// <summary>
    /// Regexp to identify the Vat fields
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex("^(I|J|K)[1-8]:.+", RegexOptions.IgnoreCase)]
    protected partial Regex VatFieldRegexp { get; }

    /// <summary>
    /// The instance logger
    /// </summary>
    protected ILogger Logger = logger;

    /// <summary>
    /// The enum parser
    /// </summary>
    protected IEnumParse EnumParse = enumParse;

    /// <summary>
    /// Parse a QR code string
    /// </summary>
    /// <param name="qrCodeString"></param>
    /// <returns></returns>
    /// <exception cref="BuilderParseStringException"></exception>
    public virtual Document Parse(string qrCodeString)
    {

        Logger.LogInformation($"Stating parse QR code string {qrCodeString}");

        var document = Activator.CreateInstance<Document>();

        var documentType = typeof(Document);

        var fieldProperties = new Dictionary<string, PropertyInfo>();
        var propertiesInfo = documentType.GetProperties();

        foreach (var property in propertiesInfo)
        {
            var field = property.GetCustomAttribute<FieldAttribute>();
            if (field is null) continue;

            fieldProperties.Add(field.Fields[0], property);
        }

        var parts = qrCodeString.Split('*').Select(p => p.Trim()).ToList();

        foreach (var part in parts)
        {
            if (VatFieldRegexp.IsMatch(part)) continue;

            var fieldRawParts = part.Split(':');

            if (fieldRawParts.Length < 2) continue;

            var key = fieldRawParts[0].Trim();

            var rawValue = fieldRawParts.Length > 2
                    ? string.Join(';', fieldRawParts.Skip(1))
                    : fieldRawParts[1].Trim();

            Logger.LogTrace($"Parse field {key} with raw value '{rawValue}'");

            if (string.IsNullOrWhiteSpace(key) || string.IsNullOrWhiteSpace(rawValue))
            {
                Logger.LogInformation($"Field {key} with raw value empty, skip parse");
                continue;
            }

            var propertyInfo = fieldProperties.FirstOrDefault(p => p.Key.Equals(key, StringComparison.OrdinalIgnoreCase)).Value;

            if (propertyInfo is null)
            {
                Logger.LogError($"Field is not part of QR code, Field = {key}");
                throw new ParseStringException("Field is not part of QR code") { Field = key };
            }

            if (propertyInfo.PropertyType == typeof(string))
            {
                propertyInfo.SetValue(document, rawValue);
                continue;
            }

            if (propertyInfo.PropertyType == typeof(decimal) || propertyInfo.PropertyType == typeof(decimal?))
            {
                if (decimal.TryParse(rawValue, CultureInfo.InvariantCulture, out var @decimal))
                {
                    propertyInfo.SetValue(document, @decimal);
                    continue;
                }

                Logger.LogError($"The decimal {rawValue} value is not valid for Field '{key}' and property {propertyInfo.Name}");
                throw new ParseStringException("The decimal value is not valid") { Field = key };
            }

            if (propertyInfo.PropertyType == typeof(int) || propertyInfo.PropertyType == typeof(int?))
            {
                if (int.TryParse(rawValue, out var @int))
                {
                    propertyInfo.SetValue(document, @int);
                    continue;
                }

                Logger.LogError($"The int {rawValue} value is not valid for Field '{key}' and property {propertyInfo.Name}");
                throw new ParseStringException("The int value is not valid") { Field = key };
            }

            if (propertyInfo.PropertyType == typeof(DateOnly) || propertyInfo.PropertyType == typeof(DateOnly?))
            {
                if (DateOnly.TryParseExact(rawValue, "yyyyMMdd", out var @dateOnly))
                {
                    propertyInfo.SetValue(document, @dateOnly);
                    continue;
                }

                Logger.LogError($"The dateOnly {rawValue} value is not valid for Field '{key}' and property {propertyInfo.Name}");
                throw new ParseStringException("The DateOnly value is not valid") { Field = key };
            }

            Type? enumType = null;

            if (propertyInfo.PropertyType.IsEnum)
            {
                enumType = propertyInfo.PropertyType;

            }
            else if (propertyInfo.PropertyType.Name == typeof(System.Nullable<>).Name)
            {
                var genericArgs = propertyInfo.PropertyType.GetGenericArguments();

                if (genericArgs[0]?.IsEnum ?? false)
                {
                    enumType = genericArgs[0];
                }
            }

            if (enumType != null)
            {
                if (EnumParse.TryFirstFromAttributes(enumType, rawValue, true, out var @enum))
                {
                    propertyInfo.SetValue(document, @enum);
                    continue;
                }

                Logger.LogError($"The enumeration {rawValue} value is not valid for Field '{key}' and property {propertyInfo.Name}");
                throw new ParseStringException("The enum value is not valid") { Field = key };
            }

            Logger.LogError($"No parser for type {propertyInfo.PropertyType}");
            throw new ParseStringException($"No parser for type {propertyInfo.PropertyType}");
        }

        var nonVatField = parts.FirstOrDefault(p => p.ToUpper().Equals("I1:0"));

        if (nonVatField is not null) return document;


        var vatPropertiesInfo = new Dictionary<int, PropertyInfo>();

        foreach (var vatPropertyInfo in typeof(Vat).GetProperties())
        {
            var vatField = vatPropertyInfo.GetCustomAttribute<FieldAttribute>();

            if (vatField is null) continue;

            vatPropertiesInfo.Add(
                   int.Parse(vatField.Fields[0].Substring(1, 1)),
                   vatPropertyInfo
            );
        }

        foreach (var vatFiledId in new string[] { "I", "J", "K" })
        {
            var vatFieldRaw = parts.FirstOrDefault(p => p.ToUpper().StartsWith($"{vatFiledId}1:"));

            if (string.IsNullOrWhiteSpace(vatFieldRaw))
            {
                Logger.LogInformation($"Vat field {vatFiledId}1 not exist");
                continue;
            }

            Logger.LogTrace($"Start parse VAT for fields {vatFiledId}");

            Vat vat;

            if (EnumParse.TryFirstFromAttributes<TaxCountryRegion>(vatFieldRaw.Substring(3), out var taxCountryRegion))
            {
                vat = new Vat() { TaxCountryRegion = (TaxCountryRegion)taxCountryRegion };
                document.Vats.Add(vat);
            }
            else
            {
                Logger.LogError($"Tax country region is not valid Field  {vatFieldRaw[..2]}");
                throw new ParseStringException("Tax country region is not valid") { Field = vatFieldRaw[..2] };
            }

            for (var n = 2; n <= 8; n++)
            {
                var vatValueRaw = parts.FirstOrDefault(p => p.ToUpper().StartsWith($"{vatFiledId}{n}:"));
                if (vatValueRaw is null) continue;

                if (decimal.TryParse(vatValueRaw.Split(':')[1], out var @decimal))
                {
                    vatPropertiesInfo[n].SetValue(vat, @decimal);
                }
                else
                {
                    Logger.LogError($"The decimal value is not valid field ${vatFiledId}:{n}");
                    throw new ParseStringException("The decimal value is not valid") { Field = $"{vatFiledId}:{n}" };
                }
            }
        }

        Logger.LogTrace("Finish parse qr code string");

        return document;
    }

}
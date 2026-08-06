#pragma warning disable CA2200 // Rethrows

using System.Data;
using System.Reflection;
using System.Xml.Serialization;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using PChouse.AT.Generics;
using PChouse.AT.QRCode.Library;

namespace PChouse.AT.QRCode.Test;

[TestClass]
public sealed class BuilderTest
{
    [TestMethod]
    public void TestValidateVat()
    {
        var vat = new Vat
        {
            TaxCountryRegion = Generics.TaxCountryRegion.PT_AC,
            ReducedBaseVat = 9.99m
        };

        var method = typeof(Builder).GetMethod(
            "ValidateVat",
            BindingFlags.NonPublic | BindingFlags.IgnoreCase | BindingFlags.Instance
         );

        Assert.IsNotNull(method);

        var builder = new Builder(
            NullLogger<Builder>.Instance,
            new Generics.Validator(NullLogger<Validator>.Instance)
         );

        var errors = new List<ErrorField>();

        method.Invoke(
            builder,
            [
                vat,
                1,
                "I",
                new BuilderOptions{
                    ValidateBuyerPortugueseTin = TinValidation.NONE,
                    ValidateIssuerTin = TinValidation.NONE,
                    ThrowExceptionOnError = true,
                    ValidateDocumentNumber = false
                    },
                errors
            ]
        );

        Assert.IsEmpty(errors);
    }

    [TestMethod]
    public void TestValidateEmptyVatNoThrow()
    {
        var vat = new Vat
        {
            TaxCountryRegion = Generics.TaxCountryRegion.PT_AC,
        };

        var method = typeof(Builder).GetMethod(
            "ValidateVat",
            BindingFlags.NonPublic | BindingFlags.IgnoreCase | BindingFlags.Instance
            );

        Assert.IsNotNull(method);

        var builder = new Builder(
            NullLogger<Builder>.Instance,
            new Generics.Validator(NullLogger<Validator>.Instance)
         );

        var errors = new List<ErrorField>();

        method.Invoke(
            builder,
            [
                vat,
                1,
                "I",
                new BuilderOptions{
                    ValidateBuyerPortugueseTin = TinValidation.NONE,
                    ValidateIssuerTin = TinValidation.NONE,
                    ThrowExceptionOnError = false,
                    ValidateDocumentNumber = false
                    },
                errors
            ]
        );

        Assert.HasCount(1, errors);
        Assert.AreEqual(Error.VAT_VALUES_ALL_EMPTY, errors[0].Error);
        Assert.AreEqual("I", errors[0].Field.Fields[0]);
    }

    [TestMethod]
    public void TestValidateEmptyVatThrow()
    {
        var exception = Assert.Throws<BuilderFieldException>(() =>
        {
            var errors = new List<ErrorField>();

            try
            {
                var vat = new Vat
                {
                    TaxCountryRegion = Generics.TaxCountryRegion.PT_AC,
                };

                var method = typeof(Builder).GetMethod(
                    "ValidateVat",
                    System.Reflection.BindingFlags.NonPublic |
                    System.Reflection.BindingFlags.IgnoreCase |
                    System.Reflection.BindingFlags.Instance
                    );

                Assert.IsNotNull(method);

                var builder = new Builder(
                        NullLogger<Builder>.Instance,
                        new Generics.Validator(NullLogger<Validator>.Instance)
                );

                method.Invoke(
                    builder,
                    [
                        vat,
                1,
                "I",
                new BuilderOptions{
                    ValidateBuyerPortugueseTin = TinValidation.NONE,
                    ValidateIssuerTin = TinValidation.NONE,
                    ThrowExceptionOnError = true,
                    ValidateDocumentNumber = false
                    },
                errors
                    ]
                );
            }
            catch (Exception e)
            {
                if (e.InnerException is null) Assert.Fail($"Should throw {nameof(BuilderFieldException)}");

                Assert.HasCount(1, errors);
                Assert.AreEqual(Error.VAT_VALUES_ALL_EMPTY, errors[0].Error);
                Assert.AreEqual("I", errors[0].Field.Fields[0]);

                throw e.InnerException;
            }
        });

        Assert.AreEqual(Error.VAT_VALUES_ALL_EMPTY, exception?.Error);
        Assert.AreEqual("I", exception?.Field.Fields[0]);
    }

    enum AddEnum
    {
        [XmlEnum("B999")]
        A999
    }

    [TestMethod]
    [DataRow(new string[] { "X", "Y" }, 0)]
    [DataRow(new string[] { "X", "Y" }, 1)]
    public void AddEnumTest(string[] fields, int fieldIndex)
    {
        var enums = new Enum[]
        {
            DocumentStatus.A,
            DocumentType.FT,
            AddEnum.A999
        };

        foreach (var enumeration in enums)
        {
            var method = typeof(Builder).GetMethod(
                            "AddEnum",
                            System.Reflection.BindingFlags.NonPublic |
                            System.Reflection.BindingFlags.IgnoreCase |
                            System.Reflection.BindingFlags.Instance
                        );

            Assert.IsNotNull(method);

            var builder = new Builder(
                NullLogger<Builder>.Instance,
                new Generics.Validator(NullLogger<Validator>.Instance)
            );

            var qrCodeStringStack = new List<string>();

            method.Invoke(builder, [
               enumeration,
                new FieldAttribute{
                    Fields = fields,
                    IsMandatory = true,
                    MaximLength = 0,
                    Position = 0
                },
                fieldIndex,
                qrCodeStringStack,
            ]);

            Assert.HasCount(1, qrCodeStringStack);

            var builded = qrCodeStringStack[0];

            if (enumeration is DocumentType || enumeration is DocumentStatus)
            {
                Assert.AreEqual($"{fields[fieldIndex]}:{enumeration}", builded);
            }
            else
            {
                Assert.AreEqual($"{fields[fieldIndex]}:B999", builded);
            }
        }
    }

    enum AddEnumNoXmlAttribute
    {
        A999
    }

    [TestMethod]
    public void AddEnumExceptionTest()
    {
        var exception = Assert.Throws<Exception>(() =>
        {

            var method = typeof(Builder).GetMethod(
                            "AddEnum",
                            BindingFlags.NonPublic | BindingFlags.IgnoreCase | BindingFlags.Instance
                        );

            Assert.IsNotNull(method);

            var builder = new Builder(
                    NullLogger<Builder>.Instance,
                    new Generics.Validator(NullLogger<Validator>.Instance)
            );

            var qrCodeStringStack = new List<string>();

            method.Invoke(builder, [
               AddEnumNoXmlAttribute.A999,
                new FieldAttribute{
                    Fields = ["X"],
                    IsMandatory = true,
                    MaximLength = 0,
                    Position = 0
                },
                0,
                qrCodeStringStack,
            ]);
        });

        Assert.IsTrue(exception.InnerException is BuilderException);
    }

    [TestMethod]
    public void AddNegativeDecimalNoThrowsException()
    {
        var mockPropertyInfo = new Mock<PropertyInfo>();
        mockPropertyInfo.Setup(p => p.Name).Returns("PropName");

        var method = typeof(Builder).GetMethod(
                        "AddDecimal",
                        BindingFlags.NonPublic | BindingFlags.IgnoreCase | BindingFlags.Instance
                    );

        Assert.IsNotNull(method);

        var builder = new Builder(
            NullLogger<Builder>.Instance,
            new Generics.Validator(NullLogger<Validator>.Instance)
         );

        var qrCodeStringStack = new List<string>();
        var errors = new List<ErrorField>();

        method.Invoke(builder, [
           -9m,
                new Properties{
                        PropertyInfo = mockPropertyInfo.Object,
                        Field = new FieldAttribute{
                            Fields = ["X"],
                            IsMandatory = true,
                            MaximLength = 0,
                            Position = 0
                        }
                },
                0,
                new BuilderOptions{
                     ThrowExceptionOnError = false,
                     ValidateBuyerPortugueseTin = TinValidation.NONE,
                     ValidateIssuerTin = TinValidation.NONE,
                    ValidateDocumentNumber = false
                },
                qrCodeStringStack,
                errors
        ]);

        Assert.HasCount(1, errors);
        Assert.AreEqual(Error.NEGATIVE_DECIMAL, errors[0].Error);
        Assert.AreEqual("X", errors[0].Field.Fields[0]);
    }

    [TestMethod]
    public void AddNegativeDecimalThrowsException()
    {
        var mockPropertyInfo = new Mock<PropertyInfo>();
        mockPropertyInfo.Setup(p => p.Name).Returns("PropName");

        var exception = Assert.Throws<BuilderFieldException>(() =>
        {

            var errors = new List<ErrorField>();

            try
            {
                var method = typeof(Builder).GetMethod(
                                "AddDecimal",
                                BindingFlags.NonPublic | BindingFlags.IgnoreCase | BindingFlags.Instance
                            );

                Assert.IsNotNull(method);

                var builder = new Builder(
                        NullLogger<Builder>.Instance,
                        new Generics.Validator(NullLogger<Validator>.Instance)
                );

                var qrCodeStringStack = new List<string>();

                method.Invoke(builder, [
                   -9m,
                new Properties{
                        PropertyInfo =  mockPropertyInfo.Object,
                        Field = new FieldAttribute{
                            Fields = ["X"],
                            IsMandatory = true,
                            MaximLength = 0,
                            Position = 0
                        }
                },
                0,
                new BuilderOptions{
                     ThrowExceptionOnError = true,
                     ValidateBuyerPortugueseTin = TinValidation.NONE,
                     ValidateIssuerTin = TinValidation.NONE,
                    ValidateDocumentNumber = false
                },
                qrCodeStringStack,
                errors
                ]);

            }
            catch (Exception e)
            {
                if (e.InnerException is null) Assert.Fail($"Should throw {nameof(BuilderFieldException)}");

                Assert.AreEqual("X", errors[0].Field.Fields[0]);
                Assert.AreEqual(Error.NEGATIVE_DECIMAL, errors[0].Error);

                throw e.InnerException;
            }
        });

        Assert.AreEqual("X", exception.Field.Fields[0]);
        Assert.AreEqual(Error.NEGATIVE_DECIMAL, exception.Error);
    }

    [TestMethod]
    public void AddZeroDecimalMandatoryNoThrowsException()
    {
        var mockPropertyInfo = new Mock<PropertyInfo>();
        mockPropertyInfo.Setup(p => p.Name).Returns("PropName");

        var method = typeof(Builder).GetMethod(
                        "AddDecimal",
                        System.Reflection.BindingFlags.NonPublic |
                        System.Reflection.BindingFlags.IgnoreCase |
                        System.Reflection.BindingFlags.Instance
                    );

        Assert.IsNotNull(method);

        var builder = new Builder(
            NullLogger<Builder>.Instance,
            new Generics.Validator(NullLogger<Validator>.Instance)
         );

        var qrCodeStringStack = new List<string>();
        var errors = new List<ErrorField>();

        method.Invoke(builder, [
                0m,
                new Properties{
                        PropertyInfo = mockPropertyInfo.Object,
                        Field = new FieldAttribute{
                            Fields = ["X"],
                            IsMandatory = true,
                            MaximLength = 0,
                            Position = 0,
                            IfEmptyValue = "",
                            ZeroIsEmpty = true,
                        }
                    },
                0,
                new BuilderOptions{
                     ThrowExceptionOnError = false,
                     ValidateBuyerPortugueseTin = TinValidation.NONE,
                     ValidateIssuerTin = TinValidation.NONE,
                    ValidateDocumentNumber = false
                },
                qrCodeStringStack,
                errors
        ]);

        Assert.HasCount(1, errors);
        Assert.AreEqual(Error.MANDATORY_FIELD_WITHOUT_VALUE, errors[0].Error);
        Assert.AreEqual("X", errors[0].Field.Fields[0]);
    }

    [TestMethod]
    public void AddZeroDecimalMandatoryThrowsException()
    {
        var mockPropertyInfo = new Mock<PropertyInfo>();
        mockPropertyInfo.Setup(p => p.Name).Returns("PropName");

        var exception = Assert.Throws<BuilderFieldException>(() =>
        {

            var errors = new List<ErrorField>();

            try
            {

                var method = typeof(Builder).GetMethod(
                                "AddDecimal",
                                System.Reflection.BindingFlags.NonPublic |
                                System.Reflection.BindingFlags.IgnoreCase |
                                System.Reflection.BindingFlags.Instance
                            );

                Assert.IsNotNull(method);

                var builder = new Builder(
                    NullLogger<Builder>.Instance,
                    new Generics.Validator(NullLogger<Validator>.Instance)
                );

                var qrCodeStringStack = new List<string>();

                method.Invoke(builder, [
                    0m,
                new Properties{
                        PropertyInfo = mockPropertyInfo.Object,
                        Field = new FieldAttribute{
                            Fields = ["X"],
                            IsMandatory = true,
                            MaximLength = 0,
                            Position = 0
                        }
                    },
                0,
                new BuilderOptions{
                     ThrowExceptionOnError = true,
                     ValidateBuyerPortugueseTin = TinValidation.NONE,
                     ValidateIssuerTin = TinValidation.NONE,
                    ValidateDocumentNumber = false
                },
                qrCodeStringStack,
                errors
                ]);

            }
            catch (Exception e)
            {
                if (e.InnerException is null) Assert.Fail($"Should throw {nameof(BuilderFieldException)}");

                Assert.AreEqual("X", errors[0].Field.Fields[0]);
                Assert.AreEqual(Error.MANDATORY_FIELD_WITHOUT_VALUE, errors[0].Error);

                throw e.InnerException;
            }
        });

        Assert.AreEqual("X", exception.Field.Fields[0]);
        Assert.AreEqual(Error.MANDATORY_FIELD_WITHOUT_VALUE, exception.Error);
    }

    [TestMethod]
    public void AddZeroDecimalNotMandatory()
    {
        var mockPropertyInfo = new Mock<PropertyInfo>();
        mockPropertyInfo.Setup(p => p.Name).Returns("PropName");

        var method = typeof(Builder).GetMethod(
                        "AddDecimal",
                        System.Reflection.BindingFlags.NonPublic |
                        System.Reflection.BindingFlags.IgnoreCase |
                        System.Reflection.BindingFlags.Instance
                    );

        Assert.IsNotNull(method);

        var builder = new Builder(
            NullLogger<Builder>.Instance,
            new Generics.Validator(NullLogger<Validator>.Instance)
         );

        var qrCodeStringStack = new List<string>();
        var errors = new List<ErrorField>();

        method.Invoke(builder, [
                0m,
                new Properties{
                        PropertyInfo = mockPropertyInfo.Object,
                        Field = new FieldAttribute{
                            Fields = ["X"],
                            IsMandatory = false,
                            MaximLength = 0,
                            Position = 0,
                            IfEmptyValue = "",
                            ZeroIsEmpty = true,
                        }
                    },
                0,
                new BuilderOptions{
                     ThrowExceptionOnError = false,
                     ValidateBuyerPortugueseTin = TinValidation.NONE,
                     ValidateIssuerTin = TinValidation.NONE,
                    ValidateDocumentNumber = false
                },
                qrCodeStringStack,
                errors
        ]);

        Assert.HasCount(0, errors);
        Assert.HasCount(0, qrCodeStringStack);

    }

    [TestMethod]
    [DataRow(new string[] { "X", "Y" }, 0, 9, "9.00")]
    [DataRow(new string[] { "X", "Y" }, 1, 9.99, "9.99")]
    [DataRow(new string[] { "X", "Y" }, 1, 999.985, "999.99")]
    public void AddDecimal(string[] fields, int fieldIndex, double value, string expected)
    {
        var mockPropertyInfo = new Mock<PropertyInfo>();
        mockPropertyInfo.Setup(p => p.Name).Returns("PropName");

        var method = typeof(Builder).GetMethod(
                        "AddDecimal",
                        System.Reflection.BindingFlags.NonPublic |
                        System.Reflection.BindingFlags.IgnoreCase |
                        System.Reflection.BindingFlags.Instance
                    );

        Assert.IsNotNull(method);

        var builder = new Builder(
            NullLogger<Builder>.Instance,
            new Generics.Validator(NullLogger<Validator>.Instance)
         );

        var qrCodeStringStack = new List<string>();
        var errors = new List<ErrorField>();

        method.Invoke(builder, [
                decimal.Parse(value.ToString()),
                new Properties{
                        PropertyInfo = mockPropertyInfo.Object,
                        Field = new FieldAttribute{
                            Fields = fields,
                            IsMandatory = false,
                            MaximLength = 16,
                            Position = 0,
                            IfEmptyValue = "",
                            ZeroIsEmpty = true,
                        }
                },
                fieldIndex,
                new BuilderOptions{
                     ThrowExceptionOnError = false,
                     ValidateBuyerPortugueseTin = TinValidation.NONE,
                     ValidateIssuerTin = TinValidation.NONE,
                    ValidateDocumentNumber = false
                },
                qrCodeStringStack,
                errors
        ]);

        Assert.HasCount(0, errors);
        Assert.HasCount(1, qrCodeStringStack);
        Assert.AreEqual($"{fields[fieldIndex]}:{expected}", qrCodeStringStack[0]);
    }

    [TestMethod]
    [DataRow("A", 0)]
    [DataRow("AAA", 2)]
    public void TestValueLengthThrowsException(string value, int maxLength)
    {

        var mockPropertyInfo = new Mock<PropertyInfo>();
        mockPropertyInfo.Setup(p => p.Name).Returns("PropName");

        var exception = Assert.Throws<BuilderFieldException>(() =>
        {
            var errors = new List<ErrorField>();

            try
            {
                var method = typeof(Builder).GetMethod(
                                "TestValueLength",
                                System.Reflection.BindingFlags.NonPublic |
                                System.Reflection.BindingFlags.IgnoreCase |
                                System.Reflection.BindingFlags.Instance
                            );

                Assert.IsNotNull(method);

                var builder = new Builder(
                        NullLogger<Builder>.Instance,
                        new Generics.Validator(NullLogger<Validator>.Instance)
                );

                method.Invoke(builder, [
                    value,
                new Properties{
                        PropertyInfo = mockPropertyInfo.Object,
                        Field = new FieldAttribute{
                            Fields = ["X"],
                            IsMandatory = true,
                            MaximLength = maxLength,
                            Position = 0
                        }
                },
                0,
                new BuilderOptions{
                     ThrowExceptionOnError = true,
                     ValidateBuyerPortugueseTin = TinValidation.NONE,
                     ValidateIssuerTin = TinValidation.NONE,
                    ValidateDocumentNumber = false
                },
                errors
                ]);
            }
            catch (Exception e)
            {
                if (e.InnerException is null) Assert.Fail($"Should throw {nameof(BuilderFieldException)}");

                Assert.AreEqual("X", errors[0].Field.Fields[0]);
                Assert.AreEqual(Error.FAIL_LENGTH, errors[0].Error);

                throw e.InnerException;
            }
        });

        Assert.AreEqual("X", exception.Field.Fields[0]);
        Assert.AreEqual(Error.FAIL_LENGTH, exception.Error);
    }

    [TestMethod]
    [DataRow("A", 0)]
    [DataRow("AAA", 2)]
    public void TestValueLengthNoThrowsException(string value, int maxLength)
    {
        var mockPropertyInfo = new Mock<PropertyInfo>();
        mockPropertyInfo.Setup(p => p.Name).Returns("PropName");

        var method = typeof(Builder).GetMethod(
                        "TestValueLength",
                        System.Reflection.BindingFlags.NonPublic |
                        System.Reflection.BindingFlags.IgnoreCase |
                        System.Reflection.BindingFlags.Instance
                    );

        Assert.IsNotNull(method);

        var builder = new Builder(
            NullLogger<Builder>.Instance,
            new Generics.Validator(NullLogger<Validator>.Instance)
         );

        var errors = new List<ErrorField>();

        method.Invoke(builder, [
            value,
                new Properties{
                        PropertyInfo = mockPropertyInfo.Object,
                        Field = new FieldAttribute{
                            Fields = ["X"],
                            IsMandatory = true,
                            MaximLength = maxLength,
                            Position = 0
                        }
                },
                0,
                new BuilderOptions{
                     ThrowExceptionOnError = false,
                     ValidateBuyerPortugueseTin = TinValidation.NONE,
                     ValidateIssuerTin = TinValidation.NONE,
                    ValidateDocumentNumber = false
                },
                errors
        ]);

        Assert.AreEqual("X", errors[0].Field.Fields[0]);
        Assert.AreEqual(Error.FAIL_LENGTH, errors[0].Error);
    }

    [TestMethod]
    [DataRow("AAA", 3)]
    [DataRow("AAA", 4)]
    public void TestValueLength(string value, int maxLength)
    {
        var mockPropertyInfo = new Mock<PropertyInfo>();
        mockPropertyInfo.Setup(p => p.Name).Returns("PropName");

        var method = typeof(Builder).GetMethod(
                        "TestValueLength",
                        System.Reflection.BindingFlags.NonPublic |
                        System.Reflection.BindingFlags.IgnoreCase |
                        System.Reflection.BindingFlags.Instance
                    );

        Assert.IsNotNull(method);

        var builder = new Builder(
            NullLogger<Builder>.Instance,
            new Generics.Validator(NullLogger<Validator>.Instance)
         );

        var errors = new List<ErrorField>();

        method.Invoke(builder, [
            value,
                new Properties{
                        PropertyInfo = mockPropertyInfo.Object,
                        Field = new FieldAttribute{
                            Fields = ["X"],
                            IsMandatory = true,
                            MaximLength = maxLength,
                            Position = 0
                        }
                },
                0,
                new BuilderOptions{
                     ThrowExceptionOnError = false,
                     ValidateBuyerPortugueseTin = TinValidation.NONE,
                     ValidateIssuerTin = TinValidation.NONE,
                    ValidateDocumentNumber = false
                },
                errors
        ]);

        Assert.HasCount(0, errors);
    }

    [TestMethod]
    public void TestNullOrEmptyMandatoryNoThrowException()
    {
        var mockPropertyInfo = new Mock<PropertyInfo>();
        mockPropertyInfo.Setup(p => p.Name).Returns("PropName");

        var method = typeof(Builder).GetMethod(
                        "TestNullOrEmptyMandatory",
                        System.Reflection.BindingFlags.NonPublic |
                        System.Reflection.BindingFlags.IgnoreCase |
                        System.Reflection.BindingFlags.Instance
                    );

        Assert.IsNotNull(method);

        var builder = new Builder(
            NullLogger<Builder>.Instance,
            new Generics.Validator(NullLogger<Validator>.Instance)
         );

        var errors = new List<ErrorField>();

        method.Invoke(builder, [
                new Properties{
                        PropertyInfo = mockPropertyInfo.Object,
                        Field = new FieldAttribute{
                            Fields = ["X"],
                            IsMandatory = true,
                            MaximLength = 0,
                            Position = 0,
                            IfEmptyValue = ""
                        }
                },
                0,
                new BuilderOptions{
                     ThrowExceptionOnError = false,
                     ValidateBuyerPortugueseTin = TinValidation.NONE,
                     ValidateIssuerTin = TinValidation.NONE,
                    ValidateDocumentNumber = false
                },
                errors
        ]);

        Assert.HasCount(1, errors);
        Assert.AreEqual(Error.MANDATORY_FIELD_WITHOUT_VALUE, errors[0].Error);
        Assert.AreEqual("X", errors[0].Field.Fields[0]);
    }

    [TestMethod]
    public void TestNullOrEmptyMandatoryThrowException()
    {
        var exception = Assert.Throws<BuilderFieldException>(() =>
        {
            var errors = new List<ErrorField>();

            try
            {

                var mockPropertyInfo = new Mock<PropertyInfo>();
                mockPropertyInfo.Setup(p => p.Name).Returns("PropName");

                var method = typeof(Builder).GetMethod(
                                "TestNullOrEmptyMandatory",
                                System.Reflection.BindingFlags.NonPublic |
                                System.Reflection.BindingFlags.IgnoreCase |
                                System.Reflection.BindingFlags.Instance
                            );

                Assert.IsNotNull(method);

                var builder = new Builder(
                         NullLogger<Builder>.Instance,
                         new Generics.Validator(NullLogger<Validator>.Instance)
                );

                method.Invoke(builder, [
                        new Properties{
                        PropertyInfo = mockPropertyInfo.Object,
                        Field = new FieldAttribute{
                            Fields = ["X"],
                            IsMandatory = true,
                            MaximLength = 0,
                            Position = 0,
                            IfEmptyValue = ""
                        }
                    },
                0,
                new BuilderOptions{
                     ThrowExceptionOnError = true,
                     ValidateBuyerPortugueseTin = TinValidation.NONE,
                     ValidateIssuerTin = TinValidation.NONE,
                    ValidateDocumentNumber = false
                },
                errors
                ]);
            }
            catch (Exception e)
            {
                if (e.InnerException is null) Assert.Fail($"Should throw {nameof(BuilderFieldException)}");

                Assert.HasCount(1, errors);
                Assert.AreEqual(Error.MANDATORY_FIELD_WITHOUT_VALUE, errors[0].Error);
                Assert.AreEqual("X", errors[0].Field.Fields[0]);

                throw e.InnerException;
            }
        });

        Assert.AreEqual(Error.MANDATORY_FIELD_WITHOUT_VALUE, exception.Error);
        Assert.AreEqual("X", exception.Field.Fields[0]);
    }

    [TestMethod]
    public void TestTinValidationsNone()
    {
        var mockDocument = new Mock<Document<OtherInfo>>();
        mockDocument.Setup(p => p.IssuerTin).Returns("1234");
        mockDocument.Setup(p => p.BuyerTin).Returns("1234");
        mockDocument.Setup(p => p.CountryCode).Returns(Country.PT);

        var method = typeof(Builder).GetMethod(
                        "TinValidations",
                        System.Reflection.BindingFlags.NonPublic |
                        System.Reflection.BindingFlags.IgnoreCase |
                        System.Reflection.BindingFlags.Instance
                    );

        Assert.IsNotNull(method);

        var builder = new Builder(
            NullLogger<Builder>.Instance,
            new Generics.Validator(NullLogger<Validator>.Instance)
         );

        var errors = new List<ErrorField>();

        method.Invoke(builder, [
                mockDocument.Object,
                new BuilderOptions{
                     ThrowExceptionOnError = false,
                     ValidateBuyerPortugueseTin = TinValidation.NONE,
                     ValidateIssuerTin = TinValidation.NONE,
                    ValidateDocumentNumber = false
                },
                errors
        ]);

        Assert.HasCount(0, errors);
    }


    [TestMethod]
    public void TestTinValidationsIssuerNoThrow()
    {
        var mockFieldAttribute = new Mock<FieldAttribute>().Object;

        var mockPropertyInfo = new Mock<PropertyInfo>();
        mockPropertyInfo.Setup(p => p.GetCustomAttributes(typeof(FieldAttribute), It.IsAny<bool>())).Returns(
            [mockFieldAttribute]
        );

        var mockDocumentType = new Mock<Type>();
        mockDocumentType.Setup(t => t.GetProperties(It.IsAny<BindingFlags>())).Returns([mockPropertyInfo.Object]);

        var mockDocument = new Mock<Document<OtherInfo>>();
        mockDocument.Setup(p => p.IssuerTin).Returns("1234");
        mockDocument.Setup(p => p.BuyerTin).Returns("1234");
        mockDocument.Setup(p => p.CountryCode).Returns(Country.PT);

        var method = typeof(Builder).GetMethod(
                        "TinValidations",
                        System.Reflection.BindingFlags.NonPublic |
                        System.Reflection.BindingFlags.IgnoreCase |
                        System.Reflection.BindingFlags.Instance
                    );

        Assert.IsNotNull(method);

        var builder = new Builder(
            NullLogger<Builder>.Instance,
            new Generics.Validator(NullLogger<Validator>.Instance)
         );

        var errors = new List<ErrorField>();

        method.Invoke(builder, [
                mockDocument.Object,
                new BuilderOptions{
                     ThrowExceptionOnError = false,
                     ValidateBuyerPortugueseTin = TinValidation.NONE,
                     ValidateIssuerTin = TinValidation.STRUCTURAL,
                    ValidateDocumentNumber = false
                },
                errors
        ]);

        Assert.HasCount(1, errors);
        Assert.AreEqual(Error.ISSUER_TIN_VALIDATION, errors[0].Error);
    }

    [TestMethod]
    public void TestTinValidationsIssuerThrowException()
    {

        var exception = Assert.ThrowsExactly<BuilderException>(() =>
        {

            var errors = new List<ErrorField>();

            try
            {

                var mockFieldAttribute = new Mock<FieldAttribute>().Object;

                var mockPropertyInfo = new Mock<PropertyInfo>();
                mockPropertyInfo.Setup(p => p.GetCustomAttributes(typeof(FieldAttribute), It.IsAny<bool>())).Returns(
                    [mockFieldAttribute]
                );

                var mockDocumentType = new Mock<Type>();
                mockDocumentType.Setup(t => t.GetProperties(It.IsAny<BindingFlags>())).Returns([mockPropertyInfo.Object]);

                var mockDocument = new Mock<Document<OtherInfo>>();
                mockDocument.Setup(p => p.IssuerTin).Returns("1234");
                mockDocument.Setup(p => p.BuyerTin).Returns("1234");
                mockDocument.Setup(p => p.CountryCode).Returns(Country.PT);

                var method = typeof(Builder).GetMethod(
                                "TinValidations",
                                System.Reflection.BindingFlags.NonPublic |
                                System.Reflection.BindingFlags.IgnoreCase |
                                System.Reflection.BindingFlags.Instance
                            );

                Assert.IsNotNull(method);

                var builder = new Builder(
                    NullLogger<Builder>.Instance,
                    new Generics.Validator(NullLogger<Validator>.Instance)
                );

                method.Invoke(builder, [
                        mockDocument.Object,
                new BuilderOptions{
                     ThrowExceptionOnError = true,
                     ValidateBuyerPortugueseTin = TinValidation.NONE,
                     ValidateIssuerTin = TinValidation.STRUCTURAL,
                    ValidateDocumentNumber = false
                },
                errors
                ]);

            }
            catch (Exception e)
            {
                if (e.InnerException is null) Assert.Fail($"Should throw {nameof(BuilderFieldException)}");

                Assert.HasCount(1, errors);
                Assert.AreEqual(Error.ISSUER_TIN_VALIDATION, errors[0].Error);

                throw e.InnerException;
            }
        });

        Assert.AreEqual(Error.ISSUER_TIN_VALIDATION, exception.Error);
    }


    [TestMethod]
    public void TestTinValidationsBuyerNoThrow()
    {
        var mockFieldAttribute = new Mock<FieldAttribute>().Object;

        var mockPropertyInfo = new Mock<PropertyInfo>();
        mockPropertyInfo.Setup(p => p.GetCustomAttributes(typeof(FieldAttribute), It.IsAny<bool>())).Returns(
            [mockFieldAttribute]
        );

        var mockDocumentType = new Mock<Type>();
        mockDocumentType.Setup(t => t.GetProperties(It.IsAny<BindingFlags>())).Returns([mockPropertyInfo.Object]);

        var mockDocument = new Mock<Document<OtherInfo>>();
        mockDocument.Setup(p => p.IssuerTin).Returns("1234");
        mockDocument.Setup(p => p.BuyerTin).Returns("1234");
        mockDocument.Setup(p => p.CountryCode).Returns(Country.PT);

        var method = typeof(Builder).GetMethod(
                        "TinValidations",
                        System.Reflection.BindingFlags.NonPublic |
                        System.Reflection.BindingFlags.IgnoreCase |
                        System.Reflection.BindingFlags.Instance
                    );

        Assert.IsNotNull(method);

        var builder = new Builder(
            NullLogger<Builder>.Instance,
            new Generics.Validator(NullLogger<Validator>.Instance)
         );

        var errors = new List<ErrorField>();

        method.Invoke(builder, [
                mockDocument.Object,
                new BuilderOptions{
                     ThrowExceptionOnError = false,
                     ValidateBuyerPortugueseTin = TinValidation.STRUCTURAL,
                     ValidateIssuerTin = TinValidation.NONE,
                     ValidateDocumentNumber = false
                },
                errors
        ]);

        Assert.HasCount(1, errors);
        Assert.AreEqual(Error.BUYER_TIN_VALIDATION, errors[0].Error);
    }

    [TestMethod]
    public void TestTinValidationsBuyerThrowException()
    {
        var exception = Assert.ThrowsExactly<BuilderException>(() =>
        {

            var errors = new List<ErrorField>();

            try
            {
                var mockFieldAttribute = new Mock<FieldAttribute>().Object;

                var mockPropertyInfo = new Mock<PropertyInfo>();
                mockPropertyInfo.Setup(p => p.GetCustomAttributes(typeof(FieldAttribute), It.IsAny<bool>())).Returns(
                    [mockFieldAttribute]
                );

                var mockDocumentType = new Mock<Type>();
                mockDocumentType.Setup(t => t.GetProperties(It.IsAny<BindingFlags>())).Returns([mockPropertyInfo.Object]);

                var mockDocument = new Mock<Document<OtherInfo>>();
                mockDocument.Setup(p => p.IssuerTin).Returns("1234");
                mockDocument.Setup(p => p.BuyerTin).Returns("1234");
                mockDocument.Setup(p => p.CountryCode).Returns(Country.PT);

                var method = typeof(Builder).GetMethod(
                                "TinValidations",
                                System.Reflection.BindingFlags.NonPublic |
                                System.Reflection.BindingFlags.IgnoreCase |
                                System.Reflection.BindingFlags.Instance
                            );

                Assert.IsNotNull(method);

                var builder = new Builder(
                        NullLogger<Builder>.Instance,
                        new Generics.Validator(NullLogger<Validator>.Instance)
                );

                method.Invoke(builder, [
                        mockDocument.Object,
                new BuilderOptions{
                     ThrowExceptionOnError = true,
                     ValidateBuyerPortugueseTin = TinValidation.STRUCTURAL,
                     ValidateIssuerTin = TinValidation.NONE,
                    ValidateDocumentNumber = false
                },
                errors
                ]);

            }
            catch (Exception e)
            {
                if (e.InnerException is null) Assert.Fail($"Should throw {nameof(BuilderFieldException)}");

                Assert.HasCount(1, errors);
                Assert.AreEqual(Error.BUYER_TIN_VALIDATION, errors[0].Error);

                throw e.InnerException;
            }
        });

        Assert.AreEqual(Error.BUYER_TIN_VALIDATION, exception.Error);
    }

    [TestMethod]
    public void TestValidationsDocumentNumberNoThrow()
    {
        var builder = new Builder(
            NullLogger<Builder>.Instance,
            new Generics.Validator(NullLogger<Validator>.Instance)
         );

        var options = new BuilderOptions
        {
            ThrowExceptionOnError = false,
            ValidateBuyerPortugueseTin = TinValidation.NONE,
            ValidateIssuerTin = TinValidation.NONE,
            ValidateDocumentNumber = true
        };

        var document = new Document
        {
            IssuerTin = "5555550",
            BuyerTin = "999999990",
            CountryCode = Country.PT,
            DocumentType = DocumentType.FT,
            DocumentStatus = DocumentStatus.N,
            DocumentDate = new DateOnly(),
            DocumentNumber = "FT .9/999",
            ATCUD = "0",
            TaxPayable = 999.99m,
            GrossTotal = 9999.99m,
            Hash = "9999",
            ProgramCertificateNumber = 9999
        };

        builder.BuildString(document, options, out var errors);

        Assert.HasCount(1, errors);
        Assert.AreEqual(Error.DOCUMENT_NUMBER, errors[0].Error);
    }

    [TestMethod]
    public void TestValidationsDocumentNumberThrowException()
    {

        var exception = Assert.Throws<BuilderFieldException>(() =>
        {
            var builder = new Builder(
                NullLogger<Builder>.Instance,
                new Generics.Validator(NullLogger<Validator>.Instance)
            );

            var options = new BuilderOptions
            {
                ThrowExceptionOnError = true,
                ValidateBuyerPortugueseTin = TinValidation.NONE,
                ValidateIssuerTin = TinValidation.NONE,
                ValidateDocumentNumber = true
            };

            var document = new Document
            {
                IssuerTin = "5555550",
                BuyerTin = "999999990",
                CountryCode = Country.PT,
                DocumentType = DocumentType.FT,
                DocumentStatus = DocumentStatus.N,
                DocumentDate = new DateOnly(),
                DocumentNumber = "FT .9/999",
                ATCUD = "0",
                TaxPayable = 999.99m,
                GrossTotal = 9999.99m,
                Hash = "9999",
                ProgramCertificateNumber = 9999
            };

            builder.BuildString(document, options, out var errors);

        });

        Assert.AreEqual(Error.DOCUMENT_NUMBER, exception.Error);
        Assert.AreEqual("G", exception.Field.Fields[0]);

    }

    [TestMethod]
    [DynamicData(nameof(DataProvider.BuilderStringData), typeof(DataProvider))]
    public void TestBuildString(ADocument document, string expectedString)
    {

        var builder = new Builder(
            NullLogger<Builder>.Instance,
            new Generics.Validator(NullLogger<Validator>.Instance)
         );

        var options = new BuilderOptions
        {
            ThrowExceptionOnError = true,
            ValidateBuyerPortugueseTin = TinValidation.NONE,
            ValidateIssuerTin = TinValidation.NONE,
            ValidateDocumentNumber = true
        };

        var qrCodeString = builder.BuildString(document, options, out var errors);

        Assert.AreEqual(expectedString, qrCodeString);
        Assert.HasCount(0, errors);

    }

    [TestMethod]
    [DynamicData(nameof(DataProvider.BuilderStringData), typeof(DataProvider))]
    public void TestBuildStringAsync(ADocument document, string expectedString)
    {

        var builder = new Builder(
            NullLogger<Builder>.Instance,
            new Generics.Validator(NullLogger<Validator>.Instance)
         );

        var options = new BuilderOptions
        {
            ThrowExceptionOnError = true,
            ValidateBuyerPortugueseTin = TinValidation.NONE,
            ValidateIssuerTin = TinValidation.NONE,
            ValidateDocumentNumber = true
        };

        var builded = builder.BuildStringAsync(document, options).Result;

        Assert.AreEqual(expectedString, builded.QrCodeString);
        Assert.HasCount(0, builded.Errors);
    }

    [TestMethod]
    public void TestValidationsNotThrowException()
    {
        var document = new Document
        {
            IssuerTin = "123456789",
            BuyerTin = "999999950",
            CountryCode = Generics.Country.PT,
            DocumentType = DocumentType.FT,
            DocumentStatus = DocumentStatus.N,
            DocumentDate = new DateOnly(2019, 12, 31),
            DocumentNumber = "FT .AB2019/0035",
            ATCUD = "CSDF7T5H-0035",
            Vats = [new Vat { TaxCountryRegion = Generics.TaxCountryRegion.PT }],
            TotalNonVat = 100.00m,
            TotalStampTax = 25.00m,
            TaxPayable = 64000.02m,
            GrossTotal = 513600.58m,
            WithholdingTaxAmount = 100.00m,
            Hash = "kLp0",
            ProgramCertificateNumber = 9999,
            OtherInfo = "TB;PT00000000000000000000000;513500.58"
        };

        var options = new BuilderOptions
        {
            ThrowExceptionOnError = false,
            ValidateBuyerPortugueseTin = TinValidation.STRUCTURAL,
            ValidateIssuerTin = TinValidation.STRUCTURAL,
            ValidateDocumentNumber = true
        };

        var builder = new Builder(
            NullLogger<Builder>.Instance,
            new Generics.Validator(NullLogger<Validator>.Instance)
         );

        builder.BuildString(document, options, out var errors);

        Assert.HasCount(3, errors);
    }

    [TestMethod]
    public void TestValidationsThrowException()
    {

        var exception = Assert.Throws<BuilderException>(() =>
        {
            var document = new Document
            {
                IssuerTin = "123456789",
                BuyerTin = "999999950",
                CountryCode = Generics.Country.PT,
                DocumentType = DocumentType.FT,
                DocumentStatus = DocumentStatus.N,
                DocumentDate = new DateOnly(2019, 12, 31),
                DocumentNumber = "FT .AB2019/0035",
                ATCUD = "CSDF7T5H-0035",
                Vats = [new Vat { TaxCountryRegion = Generics.TaxCountryRegion.PT }],
                TotalNonVat = 100.00m,
                TotalStampTax = 25.00m,
                TaxPayable = 64000.02m,
                GrossTotal = 513600.58m,
                WithholdingTaxAmount = 100.00m,
                Hash = "kLp0",
                ProgramCertificateNumber = 9999,
                OtherInfo = "TB;PT00000000000000000000000;513500.58"
            };

            var options = new BuilderOptions
            {
                ThrowExceptionOnError = true,
                ValidateBuyerPortugueseTin = TinValidation.STRUCTURAL,
                ValidateIssuerTin = TinValidation.STRUCTURAL,
                ValidateDocumentNumber = true
            };

            var builder = new Builder(
                NullLogger<Builder>.Instance,
                new Generics.Validator(NullLogger<Validator>.Instance)
            );

            var qrCodeString = builder.BuildString(document, options, out var errors);

        });
    }

}

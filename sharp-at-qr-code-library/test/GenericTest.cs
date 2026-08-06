using System.Reflection;
using System.Text.Json.Serialization;

namespace PChouse.AT.QRCode.Library.Test;

[TestClass]
public sealed class GenericTest
{

    [TestMethod]
    [DataRow(typeof(Document<>), nameof(Document<>.IssuerTin), 0, new string[] { "A" }, 9, true, "", true)]
    [DataRow(typeof(Document<>), nameof(Document<>.BuyerTin), 1, new string[] { "B" }, 30, true, "999999990", true)]
    [DataRow(typeof(Document<>), nameof(Document<>.CountryCode), 2, new string[] { "C" }, 12, true, "Desconhecido", true)]
    [DataRow(typeof(Document<>), nameof(Document<>.DocumentType), 3, new string[] { "D" }, 2, true, "", true)]
    [DataRow(typeof(Document<>), nameof(Document<>.DocumentStatus), 4, new string[] { "E" }, 1, true, "", true)]
    [DataRow(typeof(Document<>), nameof(Document<>.DocumentDate), 5, new string[] { "F" }, 8, true, "", true)]
    [DataRow(typeof(Document<>), nameof(Document<>.DocumentNumber), 6, new string[] { "G" }, 60, true, "", true)]
    [DataRow(typeof(Document<>), nameof(Document<>.ATCUD), 7, new string[] { "H" }, 70, true, "0", true)]
    [DataRow(typeof(Document<>), nameof(Document<>.Vats), 8, new string[] { "I1" }, 2, true, "0", true)]
    [DataRow(typeof(Document<>), nameof(Document<>.TotalNonVat), 9, new string[] { "L" }, 16, false, "", true)]
    [DataRow(typeof(Document<>), nameof(Document<>.TotalStampTax), 10, new string[] { "M" }, 16, false, "", true)]
    [DataRow(typeof(Document<>), nameof(Document<>.TaxPayable), 11, new string[] { "N" }, 16, true, "", false)]
    [DataRow(typeof(Document<>), nameof(Document<>.GrossTotal), 12, new string[] { "O" }, 16, true, "", false)]
    [DataRow(typeof(Document<>), nameof(Document<>.WithholdingTaxAmount), 13, new string[] { "P" }, 16, false, "", true)]
    [DataRow(typeof(Document<>), nameof(Document<>.Hash), 14, new string[] { "Q" }, 4, true, "", true)]
    [DataRow(typeof(Document<>), nameof(Document<>.ProgramCertificateNumber), 15, new string[] { "R" }, 4, true, "", true)]
    [DataRow(typeof(Document<>), nameof(Document<>.OtherInfo), 16, new string[] { "S" }, 65, false, "", true)]
    [DataRow(typeof(Vat), nameof(Vat.TaxCountryRegion), 0, new string[] { "I1", "J1", "K1" }, 4, true, "", true)]
    [DataRow(typeof(Vat), nameof(Vat.ExemptedBaseVat), 1, new string[] { "I2", "J2", "K2" }, 16, false, "", true)]
    [DataRow(typeof(Vat), nameof(Vat.ReducedBaseVat), 2, new string[] { "I3", "J3", "K3" }, 16, false, "", true)]
    [DataRow(typeof(Vat), nameof(Vat.ReducedTotalVat), 3, new string[] { "I4", "J4", "K4" }, 16, false, "", true)]
    [DataRow(typeof(Vat), nameof(Vat.IntermediateBaseVat), 4, new string[] { "I5", "J5", "K5" }, 16, false, "", true)]
    [DataRow(typeof(Vat), nameof(Vat.IntermediateTotalVat), 5, new string[] { "I6", "J6", "K6" }, 16, false, "", true)]
    [DataRow(typeof(Vat), nameof(Vat.NormalBaseVat), 6, new string[] { "I7", "J7", "K7" }, 16, false, "", true)]
    [DataRow(typeof(Vat), nameof(Vat.NormalTotalVat), 7, new string[] { "I8", "J8", "K8" }, 16, false, "", true)]
    public void TestProperties(
          Type type,
          string property,
          int position,
          string[] fields,
          int maximLength,
          bool mandatory,
          string ifEmptyValue,
          bool zeroIsEmpty
    )
    {
        var propertyInfo = type.GetProperties().First(p => p.Name == property);

        var jsonAttribute = propertyInfo.GetCustomAttribute<JsonPropertyNameAttribute>();
        var fieldAttribute = propertyInfo.GetCustomAttribute<FieldAttribute>();

        Assert.IsNotNull(jsonAttribute);
        Assert.IsNotNull(fieldAttribute);

        Assert.AreEqual(property, jsonAttribute.Name, $"Property: '{property}' fail json attribute name");
        Assert.AreEqual(position, fieldAttribute.Position, $"Property: '{property}' fail field position");
        Assert.HasCount(fields.Length, fieldAttribute.Fields, $"Property: '{property}' fail fields identifier length");

        foreach (var field in fields)
        {
            Assert.IsTrue(fieldAttribute.Fields.Contains(field), $"Property: '{property}' does not contains {field}");
        }

        Assert.AreEqual(maximLength, fieldAttribute.MaximLength, $"Property: '{property}' fail field maxim length");
        Assert.AreEqual(mandatory, fieldAttribute.IsMandatory, $"Property: '{property}' fail is field mandatory");
        Assert.AreEqual(ifEmptyValue, fieldAttribute.IfEmptyValue, $"Property: '{property}' fail if field empty");
        Assert.AreEqual(zeroIsEmpty, fieldAttribute.ZeroIsEmpty);

    }
}

using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace PChouse.AT.Generics.Test;

[TestClass]
public class EnumTest
{
    [TestMethod]
    [DataRow(typeof(CurrencyCode))]
    [DataRow(typeof(InvoiceStatus))]
    [DataRow(typeof(InvoiceType))]
    [DataRow(typeof(MovementStatus))]
    [DataRow(typeof(MovementType))]
    [DataRow(typeof(PaymentMechanism))]
    [DataRow(typeof(PaymentStatus))]
    [DataRow(typeof(ProductType))]
    [DataRow(typeof(TaxAccountingBasis))]
    [DataRow(typeof(TaxType))]
    [DataRow(typeof(WithholdingTaxType))]
    [DataRow(typeof(WorkStatus))]
    [DataRow(typeof(WorkType))]
    [DataRow(typeof(MovementTaxType))]
    [DataRow(typeof(PaymentType))]
    [DataRow(typeof(SourceBilling))]
    [DataRow(typeof(SourcePayment))]
    [DataRow(typeof(SupplierCountry))]
    public void TestXmlAttribute(Type type)
    {
        var fields = type.GetFields().Where(f => !f.Name.StartsWith("value")).ToList();

        Assert.IsNotEmpty(fields, $"{type.Name} does not have fields");

        foreach (var fieldInfo in fields)
        {
            var attributes = fieldInfo.GetCustomAttributes<XmlEnumAttribute>();

            Assert.AreEqual(1, attributes?.Count() ?? 0, $"Enum: '{type.Name}', field: '{fieldInfo.Name}'");

            var attribute = attributes!.First();

            Assert.AreEqual(fieldInfo.Name, attribute.Name, $"Enum: '{type.Name}', field: '{fieldInfo.Name}'");
        }
    }

    [TestMethod]
    [DataRow(typeof(CurrencyCode))]
    [DataRow(typeof(InvoiceStatus))]
    [DataRow(typeof(InvoiceType))]
    [DataRow(typeof(MovementStatus))]
    [DataRow(typeof(MovementType))]
    [DataRow(typeof(PaymentMechanism))]
    [DataRow(typeof(PaymentStatus))]
    [DataRow(typeof(ProductType))]
    [DataRow(typeof(TaxAccountingBasis))]
    [DataRow(typeof(TaxType))]
    [DataRow(typeof(WithholdingTaxType))]
    [DataRow(typeof(WorkStatus))]
    [DataRow(typeof(WorkType))]
    [DataRow(typeof(MovementTaxType))]
    [DataRow(typeof(PaymentType))]
    [DataRow(typeof(SourceBilling))]
    [DataRow(typeof(SourcePayment))]
    [DataRow(typeof(SupplierCountry))]
    public void TestJsonAttribute(Type type)
    {
        var fields = type.GetFields().Where(f => !f.Name.StartsWith("value")).ToList();

        Assert.IsNotEmpty(fields, $"{type.Name} does not have fields");

        foreach (var fieldInfo in fields)
        {
            var attributes = fieldInfo.GetCustomAttributes<JsonStringEnumMemberNameAttribute>();

            Assert.AreEqual(1, attributes?.Count() ?? 0, $"Enum: '{type.Name}', field: '{fieldInfo.Name}'");

            var attribute = attributes!.First();

            Assert.AreEqual(fieldInfo.Name, attribute.Name, $"Enum: '{type.Name}', field: '{fieldInfo.Name}'");
        }
    }

    [TestMethod]
    [DataRow(typeof(CurrencyCode))]
    [DataRow(typeof(InvoiceStatus))]
    [DataRow(typeof(InvoiceType))]
    [DataRow(typeof(MovementStatus))]
    [DataRow(typeof(MovementType))]
    [DataRow(typeof(PaymentMechanism))]
    [DataRow(typeof(PaymentStatus))]
    [DataRow(typeof(ProductType))]
    [DataRow(typeof(TaxAccountingBasis))]
    [DataRow(typeof(TaxType))]
    [DataRow(typeof(WithholdingTaxType))]
    [DataRow(typeof(WorkStatus))]
    [DataRow(typeof(WorkType))]
    [DataRow(typeof(MovementTaxType))]
    [DataRow(typeof(PaymentType))]
    [DataRow(typeof(SourceBilling))]
    [DataRow(typeof(SourcePayment))]
    [DataRow(typeof(SupplierCountry))]
    public void TestXml(Type type)
    {
        Assert.IsTrue(type.IsEnum, $"Type '{type.Name}' is not a enum");

        var fields = type.GetFields().Where(f => !f.Name.StartsWith("value")).ToList();

        Assert.IsNotEmpty(fields, $"{type.Name} does not have fields");

        foreach (var field in fields)
        {
            var enumeration = Enum.Parse(type, field.Name);

            using var memoryStream = new MemoryStream();
            using var xmlWriter = XmlWriter.Create(memoryStream);

            /**
            using var xmlWriter = XmlWriter.Create(memoryStream, new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
            });
            **/

            var skeletonType = typeof(Skeleton<>).MakeGenericType(type);

            var skeleton = Activator.CreateInstance(skeletonType);

            var property = skeletonType.GetProperty(nameof(Skeleton<>.Property));

            Assert.IsNotNull(property, $"Skeleton of type'{type.Name}' property {nameof(Skeleton<>.Property)}");

            property.SetValue(skeleton, enumeration);

            var serialize = new XmlSerializer(skeletonType);
            serialize.Serialize(xmlWriter, skeleton);

            xmlWriter.Flush();
            xmlWriter.Close();

            memoryStream.Flush();
            memoryStream.Position = 0;

            using var streamReader = new StreamReader(memoryStream);
            var xml = streamReader.ReadToEnd();

            memoryStream.Close();
            streamReader.Close();

            var regexp = new Regex($"<Property>{field.Name}</Property>");

            Assert.IsTrue(regexp.IsMatch(xml), $"Enum: '{type.Name}', field: '{field.Name}'");
        }
    }

    [TestMethod]
    [DataRow(typeof(CurrencyCode))]
    [DataRow(typeof(InvoiceStatus))]
    [DataRow(typeof(InvoiceType))]
    [DataRow(typeof(MovementStatus))]
    [DataRow(typeof(MovementType))]
    [DataRow(typeof(PaymentMechanism))]
    [DataRow(typeof(PaymentStatus))]
    [DataRow(typeof(ProductType))]
    [DataRow(typeof(TaxAccountingBasis))]
    [DataRow(typeof(TaxType))]
    [DataRow(typeof(WithholdingTaxType))]
    [DataRow(typeof(WorkStatus))]
    [DataRow(typeof(WorkType))]
    [DataRow(typeof(MovementTaxType))]
    [DataRow(typeof(PaymentType))]
    [DataRow(typeof(SourceBilling))]
    [DataRow(typeof(SourcePayment))]
    [DataRow(typeof(SupplierCountry))]
    public void TestJson(Type type)
    {

        Assert.IsTrue(type.IsEnum, $"Type '{type.Name}' is not a enum");

        var fields = type.GetFields().Where(f => !f.Name.StartsWith("value")).ToList();

        Assert.IsNotEmpty(fields, $"{type.Name} does not have fields");

        foreach (var field in fields)
        {
            var enumeration = Enum.Parse(type, field.Name);

            using var memoryStream = new MemoryStream();
            using var jsonWriter = new System.Text.Json.Utf8JsonWriter(memoryStream);

            var skeletonType = typeof(Skeleton<>).MakeGenericType(type);

            var skeleton = Activator.CreateInstance(skeletonType);

            var property = skeletonType.GetProperty(nameof(Skeleton<>.Property));

            Assert.IsNotNull(property, $"Skeleton of type'{type.Name}' property {nameof(Skeleton<>.Property)}");

            property.SetValue(skeleton, enumeration);

            jsonWriter.WriteStartObject();
            jsonWriter.WritePropertyName("Skeleton");

            System.Text.Json.JsonSerializer.Serialize(jsonWriter, skeleton);

            jsonWriter.WriteEndObject();

            jsonWriter.Flush();

            var json = Encoding.UTF8.GetString(memoryStream.ToArray());
            memoryStream.Close();

            var regexp = new Regex($"\"Property\": ?\"{field.Name}\"");

            Assert.IsTrue(regexp.IsMatch(json), $"Enum: '{type.Name}', field: '{field.Name}'");
        }
    }
}
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace PChouse.AT.Generics.Test;


[TestClass]
public sealed class TaxCountryRegionTest
{

    [TestMethod]
    public void TestXmlAttribute()
    {
        var fields = typeof(TaxCountryRegion).GetFields().Where(f => !f.Name.StartsWith("value")).ToList();

        Assert.IsNotEmpty(fields);

        foreach (var fieldInfo in fields)
        {
            var attributes = fieldInfo.GetCustomAttributes<XmlEnumAttribute>();

            Assert.AreEqual(1, attributes?.Count() ?? 0);

            var attribute = attributes!.First();

            if (Enum.Parse<TaxCountryRegion>(fieldInfo.Name) == TaxCountryRegion.PT_AC)
            {
                Assert.AreEqual("PT-AC", attribute.Name);
            }
            else if (Enum.Parse<TaxCountryRegion>(fieldInfo.Name) == TaxCountryRegion.PT_MA)
            {
                Assert.AreEqual("PT-MA", attribute.Name);
            }
            else
            {
                Assert.AreEqual(fieldInfo.Name, attributes?.First().Name ?? "");
            }
        }
    }

    [TestMethod]
    public void TestJsonAttribute()
    {
        var fields = typeof(TaxCountryRegion).GetFields().Where(f => !f.Name.StartsWith("value")).ToList();

        Assert.IsNotEmpty(fields);

        foreach (var fieldInfo in fields)
        {
            var attributes = fieldInfo.GetCustomAttributes<JsonStringEnumMemberNameAttribute>();

            Assert.AreEqual(1, attributes?.Count() ?? 0);

            var attribute = attributes!.First();

            if (Enum.Parse<TaxCountryRegion>(fieldInfo.Name) == TaxCountryRegion.PT_AC)
            {
                Assert.AreEqual("PT-AC", attribute.Name);
            }
            else if (Enum.Parse<TaxCountryRegion>(fieldInfo.Name) == TaxCountryRegion.PT_MA)
            {
                Assert.AreEqual("PT-MA", attribute.Name);
            }
            else
            {
                Assert.AreEqual(fieldInfo.Name, attributes?.First().Name ?? "");
            }
        }
    }

    [TestMethod]
    public void TestXml()
    {

        var fields = typeof(TaxCountryRegion).GetFields().Where(f => !f.Name.StartsWith("value")).ToList();

        foreach (var field in fields)
        {

            var taxCountryRegion = Enum.Parse<TaxCountryRegion>(field.Name);

            using var memoryStream = new MemoryStream();
            using var xmlWriter = XmlWriter.Create(memoryStream);

            var skeleton = new Skeleton<TaxCountryRegion>
            {
                Property = taxCountryRegion
            };

            xmlWriter.WriteStartDocument();

            var serialize = new XmlSerializer(typeof(Skeleton<TaxCountryRegion>));
            serialize.Serialize(xmlWriter, skeleton);

            xmlWriter.WriteEndDocument();
            xmlWriter.Flush();
            xmlWriter.Close();

            memoryStream.Flush();
            memoryStream.Position = 0;

            using var streamReader = new StreamReader(memoryStream);
            var xml = streamReader.ReadToEnd();

            memoryStream.Close();
            streamReader.Close();

            Regex regex;

            if (taxCountryRegion == TaxCountryRegion.PT_AC)
            {
                regex = new Regex(@"<Property>PT-AC</Property>");
            }
            else if (taxCountryRegion == TaxCountryRegion.PT_MA)
            {
                regex = new Regex(@"<Property>PT-MA</Property>");
            }
            else
            {
                regex = new Regex($"<Property>{field.Name}</Property>");
            }

            Assert.IsTrue(regex.IsMatch(xml));

        }
    }

    [TestMethod]
    public void TestJson()
    {

        var fields = typeof(TaxCountryRegion).GetFields().Where(f => !f.Name.StartsWith("value")).ToList();

        foreach (var field in fields)
        {

            var taxCountryRegion = Enum.Parse<TaxCountryRegion>(field.Name);

            using var memoryStream = new MemoryStream();
            using var jsonWriter = new System.Text.Json.Utf8JsonWriter(memoryStream);

            var skeleton = new Skeleton<TaxCountryRegion>
            {
                Property = taxCountryRegion
            };

            jsonWriter.WriteStartObject();
            jsonWriter.WritePropertyName("Skeleton");

            System.Text.Json.JsonSerializer.Serialize(jsonWriter, skeleton);

            jsonWriter.WriteEndObject();

            jsonWriter.Flush();

            var json = Encoding.UTF8.GetString(memoryStream.ToArray());

            memoryStream.Close();

            Regex regex;

            if (taxCountryRegion == TaxCountryRegion.PT_AC)
            {
                regex = new Regex("\"Property\": ?\"PT-AC\"");
            }
            else if (taxCountryRegion == TaxCountryRegion.PT_MA)
            {
                regex = new Regex("\"Property\": ?\"PT-MA\"");
            }
            else
            {
                regex = new Regex($"\"Property\": ?\"{field.Name}\"");
            }

            Assert.IsTrue(regex.IsMatch(json));
        }
    }

}

using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace PChouse.AT.Generics.Test;


[TestClass]
public sealed class CountryTest
{

    [TestMethod]
    public void TestXmlAttribute()
    {
        var fields = typeof(Country).GetFields().Where(f => !f.Name.StartsWith("value")).ToList();

        Assert.IsNotEmpty(fields);

        foreach (var fieldInfo in fields)
        {
            var attributes = fieldInfo.GetCustomAttributes<XmlEnumAttribute>();

            Assert.AreEqual(1, attributes?.Count() ?? 0);

            var attribute = attributes!.First();

            if (Enum.Parse<Country>(fieldInfo.Name) == Country.Unknown)
            {
                Assert.AreEqual("Desconhecido", attribute.Name);
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
        var fields = typeof(Country).GetFields().Where(f => !f.Name.StartsWith("value")).ToList();

        Assert.IsNotEmpty(fields);

        foreach (var fieldInfo in fields)
        {
            var attributes = fieldInfo.GetCustomAttributes<JsonStringEnumMemberNameAttribute>();

            Assert.AreEqual(1, attributes?.Count() ?? 0);

            var attribute = attributes!.First();

            if (Enum.Parse<Country>(fieldInfo.Name) == Country.Unknown)
            {
                Assert.AreEqual("Desconhecido", attribute.Name);
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

        var fields = typeof(Country).GetFields().Where(f => !f.Name.StartsWith("value")).ToList();

        foreach (var field in fields)
        {

            var country = Enum.Parse<Country>(field.Name);

            using var memoryStream = new MemoryStream();
            using var xmlWriter = XmlWriter.Create(memoryStream);

            var skeletonCountry = new SkeletonCountry
            {
                Country = country
            };

            xmlWriter.WriteStartDocument();

            var serialize = new XmlSerializer(typeof(SkeletonCountry));
            serialize.Serialize(xmlWriter, skeletonCountry);

            xmlWriter.WriteEndDocument();
            xmlWriter.Flush();
            xmlWriter.Close();

            memoryStream.Flush();
            memoryStream.Position = 0;

            using var streamReader = new StreamReader(memoryStream);
            var xml = streamReader.ReadToEnd();

            memoryStream.Close();
            streamReader.Close();

            var regexp = country == Country.Unknown
                ? new Regex($"<Country>Desconhecido</Country>")
                : new Regex($"<Country>{field.Name}</Country>");

            Assert.IsTrue(regexp.IsMatch(xml));

        }
    }

    [TestMethod]
    public void TestJson()
    {

        var fields = typeof(Country).GetFields().Where(f => !f.Name.StartsWith("value")).ToList();

        foreach (var field in fields)
        {

            var country = Enum.Parse<Country>(field.Name);

            using var memoryStream = new MemoryStream();
            using var jsonWriter = new System.Text.Json.Utf8JsonWriter(memoryStream);

            var skeletonCountry = new SkeletonCountry
            {
                Country = country
            };

            jsonWriter.WriteStartObject();
            jsonWriter.WritePropertyName("SkeletonCountry");

            System.Text.Json.JsonSerializer.Serialize<SkeletonCountry>(jsonWriter, skeletonCountry);

            jsonWriter.WriteEndObject();

            jsonWriter.Flush();

            var json = Encoding.UTF8.GetString(memoryStream.ToArray());

            memoryStream.Close();

            var regexp = country == Country.Unknown
                ? new Regex($"\"Country\": ?\"Desconhecido\"")
                : new Regex($"\"Country\": ?\"{field.Name}\"");

            Assert.IsTrue(regexp.IsMatch(json));

        }
    }

}

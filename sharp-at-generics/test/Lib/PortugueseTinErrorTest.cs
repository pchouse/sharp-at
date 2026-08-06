using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace PChouse.AT.Generics.Test;


[TestClass]
public class PortugueseTinErrorTest
{

    [TestMethod]
    public void TestXmlAttribute()
    {
        var fields = typeof(PortugueseTinError).GetFields().Where(f => !f.Name.StartsWith("value")).ToList();

        Assert.IsNotEmpty(fields);

        foreach (var fieldInfo in fields)
        {
            var attributes = fieldInfo.GetCustomAttributes<XmlEnumAttribute>();

            Assert.AreEqual(1, attributes?.Count() ?? 0);

            var attribute = attributes!.First();
        }
    }

    [TestMethod]
    public void TestJsonAttribute()
    {
        var fields = typeof(PortugueseTinError).GetFields().Where(f => !f.Name.StartsWith("value")).ToList();

        Assert.IsNotEmpty(fields);

        foreach (var fieldInfo in fields)
        {
            var attributes = fieldInfo.GetCustomAttributes<JsonStringEnumMemberNameAttribute>();

            Assert.AreEqual(1, attributes?.Count() ?? 0);

            var attribute = attributes!.First();
            Assert.AreEqual(fieldInfo.Name, attributes?.First().Name ?? "");

        }
    }

    [TestMethod]
    public void TestXml()
    {

        var fields = typeof(PortugueseTinError).GetFields().Where(f => !f.Name.StartsWith("value")).ToList();

        foreach (var field in fields)
        {

            var error = Enum.Parse<PortugueseTinError>(field.Name);

            using var memoryStream = new MemoryStream();
            using var xmlWriter = XmlWriter.Create(memoryStream);

            var skeletonError = new SkeletonError<PortugueseTinError>
            {
                Error = error
            };

            xmlWriter.WriteStartDocument();

            var serialize = new XmlSerializer(typeof(SkeletonError<PortugueseTinError>));
            serialize.Serialize(xmlWriter, skeletonError);

            xmlWriter.WriteEndDocument();
            xmlWriter.Flush();
            xmlWriter.Close();

            memoryStream.Flush();
            memoryStream.Position = 0;

            using var streamReader = new StreamReader(memoryStream);
            var xml = streamReader.ReadToEnd();

            memoryStream.Close();
            streamReader.Close();

            var regexp = new Regex($"<Error>{field.Name}</Error>");

            Assert.IsTrue(regexp.IsMatch(xml));
        }
    }

    [TestMethod]
    public void TestJson()
    {

        var fields = typeof(PortugueseTinError).GetFields().Where(f => !f.Name.StartsWith("value")).ToList();

        foreach (var field in fields)
        {

            var error = Enum.Parse<PortugueseTinError>(field.Name);

            using var memoryStream = new MemoryStream();
            using var jsonWriter = new System.Text.Json.Utf8JsonWriter(memoryStream);

            var skeletonError = new SkeletonError<PortugueseTinError>
            {
                Error = error
            };

            jsonWriter.WriteStartObject();
            jsonWriter.WritePropertyName("PortugueseTinErrorCode");

            System.Text.Json.JsonSerializer.Serialize(jsonWriter, skeletonError);

            jsonWriter.WriteEndObject();

            jsonWriter.Flush();

            var json = Encoding.UTF8.GetString(memoryStream.ToArray());
            memoryStream.Close();

            var regexp = new Regex($"\"Error\": ?\"{field.Name}\"");

            Assert.IsTrue(regexp.IsMatch(json));
        }
    }

}
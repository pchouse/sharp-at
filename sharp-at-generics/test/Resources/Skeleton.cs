using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace PChouse.AT.Generics.Test;

[XmlRoot(elementName: "Skeleton")]
public class Skeleton<T>
{

    [JsonPropertyName("Property")]
    [XmlElement(elementName: "Property")]
    public T? Property { get; set; }
}
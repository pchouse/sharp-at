
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace PChouse.AT.Generics.Test;

public class SkeletonError<T> where T : Enum
{
    [JsonPropertyName("Error")]
    [XmlElement(elementName: "Error")]
    public T? Error { get; set; }
}
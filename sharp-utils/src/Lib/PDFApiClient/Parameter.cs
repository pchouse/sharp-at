/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Text.Json.Serialization;

namespace PChouse.Utils.PDFApiClient;

public partial class Parameter
{
    [JsonRequired]
    [JsonPropertyName("type")]
    public virtual required ParameterType Type { get; set; }

    [JsonRequired]
    [JsonPropertyName("name")]
    public virtual required string Name { get; set; }

    [JsonRequired]
    [JsonPropertyName("value")]
    public virtual required string Value { get; set; }

    [JsonPropertyName("format")]
    public virtual string Format { get; set; } = string.Empty;


    [JsonExtensionData]
    public virtual IDictionary<string, object> AdditionalProperties { get; set; } = new Dictionary<string, object>();

    /// <summary>
    /// Default constructor for the Parameter class.
    /// </summary>
    public Parameter() { }

    /// <summary>
    /// Constructor for the Parameter class with parameters.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="name"></param>
    /// <param name="value"></param>
    public static Parameter New(ParameterType type, string name, string value)
    {
        return new Parameter
        {
            Type = type,
            Name = name,
            Value = value
        };
    }

    public static Parameter New(ParameterType type, string name, string value, string format)
    {
        return new Parameter
        {
            Type = type,
            Name = name,
            Value = value,
            Format = format
        };
    }

}


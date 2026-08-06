
using System.Text.Json.Serialization;


/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/
namespace PChouse.AT.QRCode.Library;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public class FieldAttribute : Attribute
{
    [JsonPropertyName(nameof(Position))]
    public virtual required int Position { get; set; } = 0;

    [JsonPropertyName(nameof(Fields))]
    public virtual required string[] Fields { get; set; } = [];

    [JsonPropertyName(nameof(MaximLength))]
    public virtual required int MaximLength { get; set; } = 0;

    [JsonPropertyName(nameof(IsMandatory))]
    public virtual required bool IsMandatory { get; set; }

    [JsonPropertyName(nameof(IfEmptyValue))]
    public virtual string IfEmptyValue { get; set; } = string.Empty;

    [JsonPropertyName(nameof(ZeroIsEmpty))]
    public virtual bool ZeroIsEmpty { get; set; } = true;

}
/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Text.Json.Serialization;

namespace PChouse.AT.QRCode.Library;

public class ErrorField
{
    /// <summary>
    /// The error find
    /// </summary>
    [JsonPropertyName(nameof(Error))]
    public virtual required Error Error { get; set; }

    /// <summary>
    /// The field with error
    /// </summary>
    [JsonPropertyName(nameof(FieldAttribute))]
    public virtual required FieldAttribute Field { get; set; }

}
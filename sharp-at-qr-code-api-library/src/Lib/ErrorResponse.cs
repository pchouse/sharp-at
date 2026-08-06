/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Text.Json.Serialization;
using PChouse.AT.QRCode.Library;


namespace PChouse.AT.QRCode.API.Library;

public record ErrorResponse
{
    /// <summary>
    /// The error
    /// </summary>
    [JsonPropertyName(nameof(Error))]
    public Error? Error { get; set; }

    /// <summary>
    /// Field
    /// </summary>
    [JsonPropertyName(nameof(Field))]
    public string? Field { get; set; }

    /// <summary>
    /// Error message
    /// </summary>
    [JsonPropertyName(nameof(Message))]
    public string? Message { get; set; }

}
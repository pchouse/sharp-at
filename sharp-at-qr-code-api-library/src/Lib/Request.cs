/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Text.Json.Serialization;
using PChouse.AT.QRCode.Library;


namespace PChouse.AT.QRCode.API.Library;

/// <summary>
/// Request the QRCode string
/// </summary>
public record Request
{

    /// <summary>
    /// The Document
    /// </summary>
    [JsonPropertyName(nameof(Document))]
    public required Document Document { get; set; }

    /// <summary>
    /// Request options
    /// </summary>
    [JsonPropertyName(nameof(BuilderOptions))]
    public required BuilderOptions BuilderOptions { get; set; }

}

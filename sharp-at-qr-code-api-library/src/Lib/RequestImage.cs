/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Text.Json.Serialization;
using PChouse.AT.QRCode.Library;


namespace PChouse.AT.QRCode.API.Library;

/// <summary>
/// Request QRCode image from Document
/// </summary>
public record RequestImage
{
    /// <summary>
    /// The Document
    /// </summary>
    [JsonPropertyName(nameof(Document))]
    public required Document Document { get; set; }

    /// <summary>
    /// String builder options
    /// </summary>
    [JsonPropertyName(nameof(BuilderOptions))]
    public required BuilderOptions BuilderOptions { get; set; }

    /// <summary>
    /// QRCode image options
    /// </summary>
    [JsonPropertyName(nameof(ImageOptions))]
    public required ImageOptions ImageOptions { get; set; }

}

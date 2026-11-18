/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Text.Json.Serialization;
using PChouse.AT.QRCode.Library;


namespace PChouse.AT.QRCode.API.Library;

/// <summary>
/// Request the QRCode image from the string
/// </summary>
public record RequestImageFromString
{

    /// <summary>
    /// The QRCode string
    /// </summary>
    [JsonPropertyName(nameof(QRCode))]
    public required string QRCode { get; set; }

    /// <summary>
    /// Image options
    /// </summary>
    [JsonPropertyName(nameof(ImageOptions))]
    public required ImageOptions ImageOptions { get; set; }

}

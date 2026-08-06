/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Text.Json.Serialization;
using PChouse.AT.QRCode.Library;


namespace PChouse.AT.QRCode.API.Library;

/// <summary>
/// Request string from a qr code image
/// </summary>
public record RequestFromImage
{
    /// <summary>
    /// The image has base64 string encoded
    /// </summary>
    [JsonPropertyName(nameof(Image))]
    public required string Image { get; set; }

    /// <summary>
    /// The type image to parse
    /// </summary>
    [JsonPropertyName(nameof(ImageParseType))]
    public ImageParseType ImageParseType { get; set; } = ImageParseType.Auto;

}

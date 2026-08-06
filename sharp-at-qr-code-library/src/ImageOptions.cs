/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Text.Json.Serialization;

namespace PChouse.AT.QRCode.Library;

/// <summary>
/// The options to generate the image
/// </summary>
public record ImageOptions
{
    /// <summary>
    /// Image margin
    /// </summary>
    [JsonPropertyName(nameof(Margin))]
    public int? Margin { get; set; } = 4;

    /// <summary>
    /// Character set
    /// </summary>
    [JsonPropertyName(nameof(CharacterSet))]
    public string CharacterSet { get; set; } = "UTF-8";

    /// <summary>
    /// Image width
    /// </summary>
    [JsonPropertyName(nameof(Width))]
    public int? Width { get; set; } = 109;

    /// <summary>
    /// Image height
    /// </summary>
    [JsonPropertyName(nameof(Height))]
    public int? Height { get; set; } = 109;

    /// <summary>
    /// Image quality
    /// </summary>
    [JsonPropertyName(nameof(Quality))]
    public int Quality { get; set; } = 100;

    /// <summary>
    /// The image type to be generated
    /// </summary>
    [JsonPropertyName(nameof(ImageType))]
    public required ImageType ImageType { get; set; }

}

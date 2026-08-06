/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Text.Json.Serialization;

namespace PChouse.AT.QRCode.Library;

/// <summary>
/// The type of image to be generated
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ImageType
{
    /// <summary>
    /// Jeg
    /// </summary>
    [JsonStringEnumMemberName(nameof(Jpeg))]
    Jpeg,

    /// <summary>
    /// Png
    /// </summary>
    [JsonStringEnumMemberName(nameof(Png))]
    Png,

    /// <summary>
    /// Svg
    /// </summary>
    [JsonStringEnumMemberName(nameof(Svg))]
    Svg,

}


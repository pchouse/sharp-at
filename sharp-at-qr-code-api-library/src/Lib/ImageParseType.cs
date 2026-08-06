/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Text.Json.Serialization;


namespace PChouse.AT.QRCode.API.Library;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ImageParseType
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

    /// <summary>
    /// Auto detection of the image type.
    /// Image must be one of the other types in this enumeration
    /// </summary>
    [JsonStringEnumMemberName(nameof(Auto))]
    Auto
}
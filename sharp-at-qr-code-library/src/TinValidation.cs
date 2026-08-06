/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/
using System.Text.Json.Serialization;

namespace PChouse.AT.QRCode.Library;

/// <summary>
/// TIN validations type
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TinValidation
{
    [JsonStringEnumMemberName("NONE")]
    NONE,

    [JsonStringEnumMemberName("STRUCTURAL")]
    STRUCTURAL,

    [JsonStringEnumMemberName("STRICT")]
    STRICT
}
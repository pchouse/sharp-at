/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Text.Json.Serialization;

namespace PChouse.Utils.PDFApiClient.Multicert;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum CanonicalTransform
{
    [JsonStringEnumMemberName(nameof(EXCLUSIVE))]
    EXCLUSIVE,

    [JsonStringEnumMemberName(nameof(EXCLUSIVE_WITH_COMMENTS))]
    EXCLUSIVE_WITH_COMMENTS
}


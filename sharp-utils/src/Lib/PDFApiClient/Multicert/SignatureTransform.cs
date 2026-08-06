/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Text.Json.Serialization;

namespace PChouse.Utils.PDFApiClient.Multicert;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SignatureTransform
{
    [JsonStringEnumMemberName(nameof(ENVELOPED))]
    ENVELOPED,

    [JsonStringEnumMemberName(nameof(XPATH))]
    XPATH,

    [JsonStringEnumMemberName(nameof(XPATH2_FILTER))]
    XPATH2_FILTER
}


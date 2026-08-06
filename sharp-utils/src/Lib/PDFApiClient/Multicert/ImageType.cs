/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Text.Json.Serialization;

namespace PChouse.Utils.PDFApiClient.Multicert;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ImageType
{
    [JsonStringEnumMemberName(nameof(PNG))]
    PNG,

    [JsonStringEnumMemberName(nameof(JPG))]
    JPG
}


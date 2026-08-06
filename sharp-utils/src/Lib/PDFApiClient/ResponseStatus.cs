/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Text.Json.Serialization;

namespace PChouse.Utils.PDFApiClient;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ResponseStatus
{

    [JsonStringEnumMemberName(nameof(OK))]
    OK = 0,

    [JsonStringEnumMemberName(nameof(ERROR))]
    ERROR = 1,

}


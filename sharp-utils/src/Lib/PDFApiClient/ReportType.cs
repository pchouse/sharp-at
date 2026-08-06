/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Text.Json.Serialization;

namespace PChouse.Utils.PDFApiClient;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ReportType
{

    [JsonStringEnumMemberName(nameof(PDF))]
    PDF = 0,

    [JsonStringEnumMemberName(nameof(PRINT))]
    PRINT = 1,

}


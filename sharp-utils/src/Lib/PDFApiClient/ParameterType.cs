/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Text.Json.Serialization;

namespace PChouse.Utils.PDFApiClient;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ParameterType
{

    [JsonStringEnumMemberName(nameof(P_STRING))]
    P_STRING = 0,

    [JsonStringEnumMemberName(nameof(P_BOOLEAN))]
    P_BOOLEAN = 1,

    [JsonStringEnumMemberName(nameof(P_BOOL))]
    P_BOOL = 2,

    [JsonStringEnumMemberName(nameof(P_DOUBLE))]
    P_DOUBLE = 3,

    [JsonStringEnumMemberName(nameof(P_FLOAT))]
    P_FLOAT = 4,

    [JsonStringEnumMemberName(nameof(P_INTEGER))]
    P_INTEGER = 5,

    [JsonStringEnumMemberName(nameof(P_LONG))]
    P_LONG = 6,

    [JsonStringEnumMemberName(nameof(P_SHORT))]
    P_SHORT = 7,

    [JsonStringEnumMemberName(nameof(P_BIGDECIMAL))]
    P_BIGDECIMAL = 8,

    [JsonStringEnumMemberName(nameof(P_DATE))]
    P_DATE = 9,

    [JsonStringEnumMemberName(nameof(P_TIME))]
    P_TIME = 10,

    [JsonStringEnumMemberName(nameof(P_SQL_TIME))]
    P_SQL_TIME = 11,

    [JsonStringEnumMemberName(nameof(P_SQL_DATE))]
    P_SQL_DATE = 12,

    [JsonStringEnumMemberName(nameof(P_TIMESTAMP))]
    P_TIMESTAMP = 13,

}


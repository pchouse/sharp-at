/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Text.Json.Serialization;

namespace PChouse.Utils.PDFApiClient;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Provider
{
    [JsonStringEnumMemberName(nameof(ORACLE))]
    ORACLE,

    [JsonStringEnumMemberName(nameof(SQLSERVER))]
    SQLSERVER,

    [JsonStringEnumMemberName(nameof(MYSQL))]
    MYSQL,

    [JsonStringEnumMemberName(nameof(POSTGRESQL))]
    POSTGRESQL,

    [JsonStringEnumMemberName(nameof(H2))]
    H2,

    [JsonStringEnumMemberName(nameof(DB2))]
    DB2,

    [JsonStringEnumMemberName(nameof(MARIADB))]
    MARIADB,

    [JsonStringEnumMemberName(nameof(DERBY))]
    DERBY,

    [JsonStringEnumMemberName(nameof(HSQLDB))]
    HSQLDB,

    [JsonStringEnumMemberName(nameof(SQLITE))]
    SQLITE,

    [JsonStringEnumMemberName(nameof(INFORMIX))]
    INFORMIX,
}


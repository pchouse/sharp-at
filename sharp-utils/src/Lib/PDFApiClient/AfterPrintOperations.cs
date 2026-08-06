/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Text.Json.Serialization;

namespace PChouse.Utils.PDFApiClient;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum AfterPrintOperations
{

    [JsonStringEnumMemberName(nameof(AFTER_PRINT_CUT_PAPER))]
    AFTER_PRINT_CUT_PAPER = 0,

    [JsonStringEnumMemberName(nameof(AFTER_PRINT_OPEN_CASH_DRAWER))]
    AFTER_PRINT_OPEN_CASH_DRAWER = 1,

}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum afterPrintOperations
{

    [JsonStringEnumMemberName(nameof(AFTER_PRINT_CUT_PAPER))]
    AFTER_PRINT_CUT_PAPER = 0,

    [JsonStringEnumMemberName(nameof(AFTER_PRINT_OPEN_CASH_DRAWER))]
    AFTER_PRINT_OPEN_CASH_DRAWER = 1,

}

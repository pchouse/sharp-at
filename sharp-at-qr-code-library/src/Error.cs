/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/


using System.Text.Json.Serialization;

namespace PChouse.AT.QRCode.Library;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Error
{
    [JsonStringEnumMemberName(nameof(MANDATORY_FIELD_WITHOUT_VALUE))]
    MANDATORY_FIELD_WITHOUT_VALUE,

    [JsonStringEnumMemberName(nameof(NEGATIVE_DECIMAL))]
    NEGATIVE_DECIMAL,

    [JsonStringEnumMemberName(nameof(NEGATIVE_INTEGER))]
    NEGATIVE_INTEGER,

    [JsonStringEnumMemberName(nameof(FAIL_LENGTH))]
    FAIL_LENGTH,

    [JsonStringEnumMemberName(nameof(VAT_VALUES_ALL_EMPTY))]
    VAT_VALUES_ALL_EMPTY,

    [JsonStringEnumMemberName(nameof(ISSUER_TIN_VALIDATION))]
    ISSUER_TIN_VALIDATION,

    [JsonStringEnumMemberName(nameof(BUYER_TIN_VALIDATION))]
    BUYER_TIN_VALIDATION,

    [JsonStringEnumMemberName(nameof(DOCUMENT_NUMBER))]
    DOCUMENT_NUMBER
}
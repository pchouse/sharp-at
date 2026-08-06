/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace PChouse.AT.Generics;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum PortugueseTinError
{
    [XmlEnum("IS_NOT_ONLY_NUMBERS")]
    [JsonStringEnumMemberName("IS_NOT_ONLY_NUMBERS")]
    IS_NOT_ONLY_NUMBERS,

    [XmlEnum("WRONG_LENGTH")]
    [JsonStringEnumMemberName("WRONG_LENGTH")]
    WRONG_LENGTH,

    [XmlEnum("TIN_STARTS_WITH_ZERO")]
    [JsonStringEnumMemberName("TIN_STARTS_WITH_ZERO")]
    TIN_STARTS_WITH_ZERO,

    [XmlEnum("WRONG_START_DIGITS")]
    [JsonStringEnumMemberName("WRONG_START_DIGITS")]
    WRONG_START_DIGITS,

    [XmlEnum("CONTROL_DIGIT_NOT_MATCH")]
    [JsonStringEnumMemberName("CONTROL_DIGIT_NOT_MATCH")]
    CONTROL_DIGIT_NOT_MATCH

}
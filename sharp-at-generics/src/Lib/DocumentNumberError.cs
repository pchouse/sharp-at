/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace PChouse.AT.Generics;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum DocumentNumberError
{
    [XmlEnum("WRONG_LENGTH")]
    [JsonStringEnumMemberName("WRONG_LENGTH")]
    WRONG_LENGTH,

    [XmlEnum("REG_EXP_NOT_MATCH")]
    [JsonStringEnumMemberName("REG_EXP_NOT_MATCH")]
    REG_EXP_NOT_MATCH,

    [XmlEnum("SERIAL_STARTS_OR_ENDS_WRONG_CHARACTER")]
    [JsonStringEnumMemberName("SERIAL_STARTS_OR_ENDS_WRONG_CHARACTER")]
    SERIAL_STARTS_OR_ENDS_WRONG_CHARACTER
}
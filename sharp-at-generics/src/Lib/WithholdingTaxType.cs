/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace PChouse.AT.Generics;

/// <summary>
/// SAF-T(PT) Withholding Tax Type
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum WithholdingTaxType
{
    /// <summary>
    /// IRS stands for Personal Income Tax.
    /// </summary>
    [XmlEnum("IRS")]
    [JsonStringEnumMemberName("IRS")]
    IRS,

    /// <summary>
    /// IRC – Corporate Income Tax
    /// </summary>
    [XmlEnum("IRC")]
    [JsonStringEnumMemberName("IRC")]
    IRC,

    /// <summary>
    /// IS for Stamp Duty
    /// </summary>
    [XmlEnum("IS")]
    [JsonStringEnumMemberName("IS")]
    IS
}
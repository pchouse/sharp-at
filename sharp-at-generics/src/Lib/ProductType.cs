/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace PChouse.AT.Generics;

/// <summary>
/// SAF-T(PT) Product Type
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ProductType
{
    /// <summary>
    /// “P” - Products
    /// </summary>
    [XmlEnum("P")]
    [JsonStringEnumMemberName("P")]
    P,

    /// <summary>
    /// “S” - Services 
    /// </summary>
    [XmlEnum("S")]
    [JsonStringEnumMemberName("S")]
    S,

    /// <summary>
    /// “O” - Others 
    /// </summary>
    [XmlEnum("O")]
    [JsonStringEnumMemberName("O")]
    O,

    /// <summary>
    /// “E” - Excise duties - (e.g. IABA, ISP, IT)
    /// </summary>
    [XmlEnum("E")]
    [JsonStringEnumMemberName("E")]
    E,

    /// <summary>
    /// “I” - Taxes, tax rates and parafiscal charges except VAT and Stamp Duty
    /// </summary>
    [XmlEnum("I")]
    [JsonStringEnumMemberName("I")]
    I
}
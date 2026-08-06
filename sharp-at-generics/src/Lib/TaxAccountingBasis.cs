/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace PChouse.AT.Generics;

/// <summary>
/// SAF-T(PT) Tax Accounting Basis
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TaxAccountingBasis
{
    /// <summary>
    /// “C” - Accounting
    /// </summary>
    [XmlEnum("C")]
    [JsonStringEnumMemberName("C")]
    C,

    /// <summary>
    /// “E” - Invoices issued by third parties
    /// </summary>
    [XmlEnum("E")]
    [JsonStringEnumMemberName("E")]
    E,

    /// <summary>
    /// “F” - Invoicing 
    /// </summary>
    [XmlEnum("F")]
    [JsonStringEnumMemberName("F")]
    F,

    /// <summary>
    /// “I” - Invoicing and accounting integrated data;<br> 
    /// </summary>
    [XmlEnum("I")]
    [JsonStringEnumMemberName("I")]
    I,

    /// <summary>
    /// “P” - Invoicing partial data 
    /// </summary>
    [XmlEnum("P")]
    [JsonStringEnumMemberName("P")]
    P,

    /// <summary>
    /// “R” - Receipts
    /// </summary>
    [XmlEnum("R")]
    [JsonStringEnumMemberName("R")]
    R,

    /// <summary>
    /// “S” - Self-billing 
    /// </summary>
    [XmlEnum("S")]
    [JsonStringEnumMemberName("S")]
    S,

    /// <summary>
    /// “T” - Transport documents 
    /// </summary>
    [XmlEnum("T")]
    [JsonStringEnumMemberName("T")]
    T
}
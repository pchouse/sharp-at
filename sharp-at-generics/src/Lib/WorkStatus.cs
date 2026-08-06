/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace PChouse.AT.Generics;

/// <summary>
/// SAF-T(PT) Work Status 
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum WorkStatus
{

    /// <summary>
    /// “N” - Normal
    /// </summary>
    [XmlEnum("N")]
    [JsonStringEnumMemberName("N")]
    N,

    /// <summary>
    /// “A” - Cancelled document
    /// </summary>
    [XmlEnum("A")]
    [JsonStringEnumMemberName("A")]
    A,

    /// <summary>
    /// “F” - Billed document, even if partially,
    //  when for the same document there is also on table 4.1. – SalesInvoices,
    /// the corresponding invoice or simplified invoice.
    /// </summary>
    [XmlEnum("F")]
    [JsonStringEnumMemberName("F")]
    F
}
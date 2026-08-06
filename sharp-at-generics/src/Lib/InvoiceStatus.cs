/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace PChouse.AT.Generics;

/// <summary>
/// SAF-T(PT) Invoice Status 
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum InvoiceStatus
{

    /// <summary>
    /// “N” - Normal
    /// </summary>
    [XmlEnum("N")]
    [JsonStringEnumMemberName("N")]
    N,

    /// <summary>
    /// S for self-invoicing "Autofaturacao"
    /// </summary>
    [XmlEnum("S")]
    [JsonStringEnumMemberName("S")]
    S,

    /// <summary>
    /// “A” - Cancelled document
    /// </summary>
    [XmlEnum("A")]
    [JsonStringEnumMemberName("A")]
    A,

    /// <summary>
    /// “R” - Summary document for other documents created in other applications and generated in this application
    /// </summary>
    [XmlEnum("R")]
    [JsonStringEnumMemberName("R")]
    R,

    /// <summary>
    /// “F” - Invoiced document
    /// </summary>
    [XmlEnum("F")]
    [JsonStringEnumMemberName("F")]
    F
}
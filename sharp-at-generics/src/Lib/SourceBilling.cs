/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace PChouse.AT.Generics;

/// <summary>
/// SAF-T(PT) Source Billing
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SourceBilling
{
    /// <summary>
    /// “P” – Document created in the invoicing program 
    /// </summary>
    [XmlEnum("P")]
    [JsonStringEnumMemberName("P")]
    P,

    /// <summary>
    /// I -> “I” – Document integrated and produced in a different invoicing program;<br>
    /// </summary>
    [XmlEnum("I")]
    [JsonStringEnumMemberName("I")]
    I,

    /// <summary>
    /// “M” – Recovered or manually issued document.<br>
    /// </summary>
    [XmlEnum("M")]
    [JsonStringEnumMemberName("M")]
    M
}
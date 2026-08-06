/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace PChouse.AT.Generics;

/// <summary>
/// SAF-T(PT) Payment Status
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum PaymentStatus
{
    /// <summary>
    /// "N" - Normal ;
    /// </summary>
    [XmlEnum("N")]
    [JsonStringEnumMemberName("N")]
    N,

    /// <summary>
    /// “A” - Canceled 
    /// </summary>
    [XmlEnum("A")]
    [JsonStringEnumMemberName("A")]
    A,

}
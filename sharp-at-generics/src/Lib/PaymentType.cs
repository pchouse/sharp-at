/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace PChouse.AT.Generics;

/// <summary>
/// SAF-T(PT) Payment Type
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum PaymentType
{

    /// <summary>
    /// RC for receipts issued under the Cash-Basis VAT scheme
    /// (including those relating to prepayments under that scheme)
    /// </summary>
    [XmlEnum("RC")]
    [JsonStringEnumMemberName("RC")]
    RC,

    /// <summary>
    /// RG for other receipts issued
    /// </summary>
    [XmlEnum("RG")]
    [JsonStringEnumMemberName("RG")]
    RG
}
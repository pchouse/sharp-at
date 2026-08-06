/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace PChouse.AT.Generics;

/// <summary>
/// SAF-T(PT) Movement Tax Type
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum MovementTaxType
{

    /// <summary>
    /// “IVA” – Value Added Tax; 
    /// </summary>
    [XmlEnum("IVA")]
    [JsonStringEnumMemberName("IVA")]
    IVA,

    /// <summary
    /// “NS” – Not subject to VAT or Stamp Duty
    /// </summary>
    [XmlEnum("NS")]
    [JsonStringEnumMemberName("NS")]
    NS
}
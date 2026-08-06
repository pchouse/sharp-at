/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace PChouse.AT.Generics;

/// <summary>
/// SAF-T(PT) Invoice Type
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum InvoiceType
{

    /// <summary>
    /// “FT” - Invoice issued according to article 36 of the VAT code;
    /// </summary>
    [XmlEnum("FT")]
    [JsonStringEnumMemberName("FT")]
    FT,

    /// <summary>
    /// “FS” - Simplified Invoice issued according to article 40 of the VAT code; 
    /// </summary>
    [XmlEnum("FS")]
    [JsonStringEnumMemberName("FS")]
    FS,

    /// <summary>
    /// “FR” – Invoice-receipt
    /// </summary>
    [XmlEnum("FR")]
    [JsonStringEnumMemberName("FR")]
    FR,

    /// <summary>
    /// “ND” - Debit note;
    /// </summary>
    [XmlEnum("ND")]
    [JsonStringEnumMemberName("ND")]
    ND,

    /// <summary>
    /// “NC” - Credit note
    /// </summary>
    [XmlEnum("NC")]
    [JsonStringEnumMemberName("NC")]
    NC,

    /// <summary>
    /// “RP” – Premium or premium receipt;
    /// </summary>
    [XmlEnum("RP")]
    [JsonStringEnumMemberName("RP")]
    RP,

    /// <summary>
    /// “RE” - Return insurance or receipt of return insurance;
    /// </summary>
    [XmlEnum("RE")]
    [JsonStringEnumMemberName("RE")]
    RE,

    /// <summary>
    /// “CS” - Imputation to co-insurance companies;
    /// </summary>
    [XmlEnum("CS")]
    [JsonStringEnumMemberName("CS")]
    CS,

    /// <summary>
    /// “LD” - Imputation to a leader co-insurance company
    /// </summary>
    [XmlEnum("LD")]
    [JsonStringEnumMemberName("LD")]
    LD,

    /// <summary>
    /// “RA” - Accepted reinsurance. (a) For data up to 2012-12-31.
    /// </summary>
    [XmlEnum("RA")]
    [JsonStringEnumMemberName("RA")]
    RA
}
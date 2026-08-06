/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace PChouse.AT.Generics;

/// <summary>
/// SAF-T(PT) Work Type 
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum WorkType
{

    /// <summary>
    /// “CM” – Table checks;
    /// </summary>
    [XmlEnum("CM")]
    [JsonStringEnumMemberName("CM")]
    CM,

    /// <summary>
    /// “CC” – Consignment credit note;
    /// </summary>
    [XmlEnum("CC")]
    [JsonStringEnumMemberName("CC")]
    CC,

    /// <summary>
    /// “FC” – Consignment invoice according to art. 38 of the Portuguese VAT Code;
    /// </summary>
    [XmlEnum("FC")]
    [JsonStringEnumMemberName("FC")]
    FC,

    /// <summary>
    /// “FO” – Worksheets [to record the service rendered or the work performed];
    /// </summary>
    [XmlEnum("FO")]
    [JsonStringEnumMemberName("FO")]
    FO,

    /// <summary>
    /// “NE” – Purchase order;
    /// </summary>
    [XmlEnum("NE")]
    [JsonStringEnumMemberName("NE")]
    NE,

    /// <summary>
    /// “OU” – Others [not specified in the remaining WorkTypes];
    /// </summary>
    [XmlEnum("OU")]
    [JsonStringEnumMemberName("OU")]
    OU,

    /// <summary>
    /// “OR” – Budgets;
    /// </summary>
    [XmlEnum("OR")]
    [JsonStringEnumMemberName("OR")]
    OR,

    /// <summary>
    /// “PF” – Pro forma invoice;
    /// </summary>
    [XmlEnum("PF")]
    [JsonStringEnumMemberName("PF")]
    PF,

    /// <summary>
    /// “DC” - Issued documents likely to be presented to the customer for the
    /// purpose of checking goods or provision of services (for data until 2017-06-30).
    /// </summary>
    [XmlEnum("DC")]
    [JsonStringEnumMemberName("DC")]
    DC,

    /// <summary>
    /// “RP” – Insurance - Premium or Premium receipt;
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
    /// rance company
    /// </summary>
    [XmlEnum("LD")]
    [JsonStringEnumMemberName("LD")]
    LD,

    /// <summary>
    /// “RA” - Accepted reinsurance.
    /// </summary>
    [XmlEnum("RA")]
    [JsonStringEnumMemberName("RA")]
    RA,
}
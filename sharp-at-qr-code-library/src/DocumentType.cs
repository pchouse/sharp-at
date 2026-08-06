/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Text.Json.Serialization;

namespace PChouse.AT.QRCode.Library;

/// <summary>
/// QR code documents type
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum DocumentType
{
    /// <summary>
    /// “FT” - Invoice issued according to article 36 of the VAT code;
    /// </summary>
    [JsonStringEnumMemberName("FT")]
    FT,

    /// <summary>
    /// “FS” - Simplified Invoice issued according to article 40 of the VAT code; 
    /// </summary>
    [JsonStringEnumMemberName("FS")]
    FS,

    /// <summary>
    /// “FR” – Invoice-receipt
    /// </summary>
    [JsonStringEnumMemberName("FR")]
    FR,

    /// <summary>
    /// “ND” - Debit note;
    /// </summary>
    [JsonStringEnumMemberName("ND")]
    ND,

    /// <summary>
    /// “NC” - Credit note
    /// </summary>
    [JsonStringEnumMemberName("NC")]
    NC,

    /// <summary>
    /// “RP” – Premium or premium receipt;
    /// </summary>
    [JsonStringEnumMemberName("RP")]
    RP,

    /// <summary>
    /// “RE” - Return insurance or receipt of return insurance;
    /// </summary>
    [JsonStringEnumMemberName("RE")]
    RE,

    /// <summary>
    /// “CS” - Imputation to co-insurance companies;
    /// </summary>
    [JsonStringEnumMemberName("CS")]
    CS,

    /// <summary>
    /// “LD” - Imputation to a leader co-insurance company
    /// </summary>
    [JsonStringEnumMemberName("LD")]
    LD,

    /// <summary>
    /// “RA” - Accepted reinsurance. (a) For data up to 2012-12-31.
    /// </summary>
    [JsonStringEnumMemberName("RA")]
    RA,

    /// <summary>
    /// "GR" - Delivery note
    /// </summary>
    [JsonStringEnumMemberName("GR")]
    GR,

    /// <summary>
    /// "GT" - Transport guide (include here the global transport documents) 
    /// </summary>
    [JsonStringEnumMemberName("GT")]
    GT,

    /// <summary>
    /// “GA” – Transport document for own fixed assets 
    /// </summary>
    [JsonStringEnumMemberName("GA")]
    GA,

    /// <summary>
    /// “GC” - Consignment note
    /// </summary>
    [JsonStringEnumMemberName("GC")]
    GC,

    /// <summary>
    /// “GD” – Return note
    /// </summary>
    [JsonStringEnumMemberName("GD")]
    GD,

    /// <summary>
    /// RC for receipts issued under the Cash-Basis VAT scheme
    /// (including those relating to prepayments under that scheme)
    /// </summary>
    [JsonStringEnumMemberName("RC")]
    RC,

    /// <summary>
    /// RG for other receipts issued
    /// </summary>
    [JsonStringEnumMemberName("RG")]
    RG,

    /// <summary>
    /// “CM” – Table checks;
    /// </summary>
    [JsonStringEnumMemberName("CM")]
    CM,

    /// <summary>
    /// “CC” – Consignment credit note;
    /// </summary>
    [JsonStringEnumMemberName("CC")]
    CC,

    /// <summary>
    /// “FC” – Consignment invoice according to art. 38 of the Portuguese VAT Code;
    /// </summary>
    [JsonStringEnumMemberName("FC")]
    FC,

    /// <summary>
    /// “FO” – Worksheets [to record the service rendered or the work performed];
    /// </summary>
    [JsonStringEnumMemberName("FO")]
    FO,

    /// <summary>
    /// “NE” – Purchase order;
    /// </summary>
    [JsonStringEnumMemberName("NE")]
    NE,

    /// <summary>
    /// “OU” – Others [not specified in the remaining WorkTypes];
    /// </summary>
    [JsonStringEnumMemberName("OU")]
    OU,

    /// <summary>
    /// “OR” – Budgets;
    /// </summary>
    [JsonStringEnumMemberName("OR")]
    OR,

    /// <summary>
    /// “PF” – Pro forma invoice;
    /// </summary>
    [JsonStringEnumMemberName("PF")]
    PF,

    /// <summary>
    /// “DC” - Issued documents likely to be presented to the customer for the
    /// purpose of checking goods or provision of services (for data until 2017-06-30).
    /// </summary>
    [JsonStringEnumMemberName("DC")]
    DC,

}

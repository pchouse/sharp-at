/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace PChouse.AT.Generics;

/// <summary>
/// SAF-T(PT) Currency code enumeration, norm ISO 4217
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum CurrencyCode
{
    [XmlEnum("AED")]
    [JsonStringEnumMemberName("AED")]
    AED,

    [XmlEnum("AFN")]
    [JsonStringEnumMemberName("AFN")]
    AFN,

    [XmlEnum("ALL")]
    [JsonStringEnumMemberName("ALL")]
    ALL,

    [XmlEnum("AMD")]
    [JsonStringEnumMemberName("AMD")]
    AMD,

    [XmlEnum("ANG")]
    [JsonStringEnumMemberName("ANG")]
    ANG,

    [XmlEnum("AOA")]
    [JsonStringEnumMemberName("AOA")]
    AOA,

    [XmlEnum("ARS")]
    [JsonStringEnumMemberName("ARS")]
    ARS,

    [XmlEnum("AUD")]
    [JsonStringEnumMemberName("AUD")]
    AUD,

    [XmlEnum("AWG")]
    [JsonStringEnumMemberName("AWG")]
    AWG,

    [XmlEnum("AZN")]
    [JsonStringEnumMemberName("AZN")]
    AZN,

    [XmlEnum("BAM")]
    [JsonStringEnumMemberName("BAM")]
    BAM,

    [XmlEnum("BBD")]
    [JsonStringEnumMemberName("BBD")]
    BBD,

    [XmlEnum("BDT")]
    [JsonStringEnumMemberName("BDT")]
    BDT,

    [XmlEnum("BGN")]
    [JsonStringEnumMemberName("BGN")]
    BGN,

    [XmlEnum("BHD")]
    [JsonStringEnumMemberName("BHD")]
    BHD,

    [XmlEnum("BIF")]
    [JsonStringEnumMemberName("BIF")]
    BIF,

    [XmlEnum("BMD")]
    [JsonStringEnumMemberName("BMD")]
    BMD,

    [XmlEnum("BND")]
    [JsonStringEnumMemberName("BND")]
    BND,

    [XmlEnum("BOB")]
    [JsonStringEnumMemberName("BOB")]
    BOB,

    [XmlEnum("BOV")]
    [JsonStringEnumMemberName("BOV")]
    BOV,

    [XmlEnum("BRL")]
    [JsonStringEnumMemberName("BRL")]
    BRL,

    [XmlEnum("BSD")]
    [JsonStringEnumMemberName("BSD")]
    BSD,

    [XmlEnum("BTN")]
    [JsonStringEnumMemberName("BTN")]
    BTN,

    [XmlEnum("BWP")]
    [JsonStringEnumMemberName("BWP")]
    BWP,

    [XmlEnum("BYN")]
    [JsonStringEnumMemberName("BYN")]
    BYN,

    [XmlEnum("BYR")]
    [JsonStringEnumMemberName("BYR")]
    BYR,

    [XmlEnum("BZD")]
    [JsonStringEnumMemberName("BZD")]
    BZD,

    [XmlEnum("CAD")]
    [JsonStringEnumMemberName("CAD")]
    CAD,

    [XmlEnum("CDF")]
    [JsonStringEnumMemberName("CDF")]
    CDF,

    [XmlEnum("CHE")]
    [JsonStringEnumMemberName("CHE")]
    CHE,

    [XmlEnum("CHF")]
    [JsonStringEnumMemberName("CHF")]
    CHF,

    [XmlEnum("CHW")]
    [JsonStringEnumMemberName("CHW")]
    CHW,

    [XmlEnum("CLF")]
    [JsonStringEnumMemberName("CLF")]
    CLF,

    [XmlEnum("CLP")]
    [JsonStringEnumMemberName("CLP")]
    CLP,

    [XmlEnum("CNY")]
    [JsonStringEnumMemberName("CNY")]
    CNY,

    [XmlEnum("COP")]
    [JsonStringEnumMemberName("COP")]
    COP,

    [XmlEnum("COU")]
    [JsonStringEnumMemberName("COU")]
    COU,

    [XmlEnum("CRC")]
    [JsonStringEnumMemberName("CRC")]
    CRC,

    [XmlEnum("CUC")]
    [JsonStringEnumMemberName("CUC")]
    CUC,

    [XmlEnum("CUP")]
    [JsonStringEnumMemberName("CUP")]
    CUP,

    [XmlEnum("CVE")]
    [JsonStringEnumMemberName("CVE")]
    CVE,

    [XmlEnum("CZK")]
    [JsonStringEnumMemberName("CZK")]
    CZK,

    [XmlEnum("DJF")]
    [JsonStringEnumMemberName("DJF")]
    DJF,

    [XmlEnum("DKK")]
    [JsonStringEnumMemberName("DKK")]
    DKK,

    [XmlEnum("DOP")]
    [JsonStringEnumMemberName("DOP")]
    DOP,

    [XmlEnum("DZD")]
    [JsonStringEnumMemberName("DZD")]
    DZD,

    [XmlEnum("EGP")]
    [JsonStringEnumMemberName("EGP")]
    EGP,

    [XmlEnum("ERN")]
    [JsonStringEnumMemberName("ERN")]
    ERN,

    [XmlEnum("ETB")]
    [JsonStringEnumMemberName("ETB")]
    ETB,

    [XmlEnum("FJD")]
    [JsonStringEnumMemberName("FJD")]
    FJD,

    [XmlEnum("FKP")]
    [JsonStringEnumMemberName("FKP")]
    FKP,

    [XmlEnum("GBP")]
    [JsonStringEnumMemberName("GBP")]
    GBP,

    [XmlEnum("GEL")]
    [JsonStringEnumMemberName("GEL")]
    GEL,

    [XmlEnum("GHS")]
    [JsonStringEnumMemberName("GHS")]
    GHS,

    [XmlEnum("GIP")]
    [JsonStringEnumMemberName("GIP")]
    GIP,

    [XmlEnum("GMD")]
    [JsonStringEnumMemberName("GMD")]
    GMD,

    [XmlEnum("GNF")]
    [JsonStringEnumMemberName("GNF")]
    GNF,

    [XmlEnum("GTQ")]
    [JsonStringEnumMemberName("GTQ")]
    GTQ,

    [XmlEnum("GYD")]
    [JsonStringEnumMemberName("GYD")]
    GYD,

    [XmlEnum("HKD")]
    [JsonStringEnumMemberName("HKD")]
    HKD,

    [XmlEnum("HNL")]
    [JsonStringEnumMemberName("HNL")]
    HNL,

    [XmlEnum("HRK")]
    [JsonStringEnumMemberName("HRK")]
    HRK,

    [XmlEnum("HTG")]
    [JsonStringEnumMemberName("HTG")]
    HTG,

    [XmlEnum("HUF")]
    [JsonStringEnumMemberName("HUF")]
    HUF,

    [XmlEnum("IDR")]
    [JsonStringEnumMemberName("IDR")]
    IDR,

    [XmlEnum("ILS")]
    [JsonStringEnumMemberName("ILS")]
    ILS,

    [XmlEnum("INR")]
    [JsonStringEnumMemberName("INR")]
    INR,

    [XmlEnum("IQD")]
    [JsonStringEnumMemberName("IQD")]
    IQD,

    [XmlEnum("IRR")]
    [JsonStringEnumMemberName("IRR")]
    IRR,

    [XmlEnum("ISK")]
    [JsonStringEnumMemberName("ISK")]
    ISK,

    [XmlEnum("JMD")]
    [JsonStringEnumMemberName("JMD")]
    JMD,

    [XmlEnum("JOD")]
    [JsonStringEnumMemberName("JOD")]
    JOD,

    [XmlEnum("JPY")]
    [JsonStringEnumMemberName("JPY")]
    JPY,

    [XmlEnum("KES")]
    [JsonStringEnumMemberName("KES")]
    KES,

    [XmlEnum("KGS")]
    [JsonStringEnumMemberName("KGS")]
    KGS,

    [XmlEnum("KHR")]
    [JsonStringEnumMemberName("KHR")]
    KHR,

    [XmlEnum("KMF")]
    [JsonStringEnumMemberName("KMF")]
    KMF,

    [XmlEnum("KPW")]
    [JsonStringEnumMemberName("KPW")]
    KPW,

    [XmlEnum("KRW")]
    [JsonStringEnumMemberName("KRW")]
    KRW,

    [XmlEnum("KWD")]
    [JsonStringEnumMemberName("KWD")]
    KWD,

    [XmlEnum("KYD")]
    [JsonStringEnumMemberName("KYD")]
    KYD,

    [XmlEnum("KZT")]
    [JsonStringEnumMemberName("KZT")]
    KZT,

    [XmlEnum("LAK")]
    [JsonStringEnumMemberName("LAK")]
    LAK,

    [XmlEnum("LBP")]
    [JsonStringEnumMemberName("LBP")]
    LBP,

    [XmlEnum("LKR")]
    [JsonStringEnumMemberName("LKR")]
    LKR,

    [XmlEnum("LRD")]
    [JsonStringEnumMemberName("LRD")]
    LRD,

    [XmlEnum("LSL")]
    [JsonStringEnumMemberName("LSL")]
    LSL,

    [XmlEnum("LTL")]
    [JsonStringEnumMemberName("LTL")]
    LTL,

    [XmlEnum("LVL")]
    [JsonStringEnumMemberName("LVL")]
    LVL,

    [XmlEnum("LYD")]
    [JsonStringEnumMemberName("LYD")]
    LYD,

    [XmlEnum("MAD")]
    [JsonStringEnumMemberName("MAD")]
    MAD,

    [XmlEnum("MDL")]
    [JsonStringEnumMemberName("MDL")]
    MDL,

    [XmlEnum("MGA")]
    [JsonStringEnumMemberName("MGA")]
    MGA,

    [XmlEnum("MKD")]
    [JsonStringEnumMemberName("MKD")]
    MKD,

    [XmlEnum("MMK")]
    [JsonStringEnumMemberName("MMK")]
    MMK,

    [XmlEnum("MNT")]
    [JsonStringEnumMemberName("MNT")]
    MNT,

    [XmlEnum("MOP")]
    [JsonStringEnumMemberName("MOP")]
    MOP,

    [XmlEnum("MRO")]
    [JsonStringEnumMemberName("MRO")]
    MRO,

    [XmlEnum("MRU")]
    [JsonStringEnumMemberName("MRU")]
    MRU,

    [XmlEnum("MUR")]
    [JsonStringEnumMemberName("MUR")]
    MUR,

    [XmlEnum("MVR")]
    [JsonStringEnumMemberName("MVR")]
    MVR,

    [XmlEnum("MWK")]
    [JsonStringEnumMemberName("MWK")]
    MWK,

    [XmlEnum("MXN")]
    [JsonStringEnumMemberName("MXN")]
    MXN,

    [XmlEnum("MXV")]
    [JsonStringEnumMemberName("MXV")]
    MXV,

    [XmlEnum("MYR")]
    [JsonStringEnumMemberName("MYR")]
    MYR,

    [XmlEnum("MZN")]
    [JsonStringEnumMemberName("MZN")]
    MZN,

    [XmlEnum("NAD")]
    [JsonStringEnumMemberName("NAD")]
    NAD,

    [XmlEnum("NGN")]
    [JsonStringEnumMemberName("NGN")]
    NGN,

    [XmlEnum("NIO")]
    [JsonStringEnumMemberName("NIO")]
    NIO,

    [XmlEnum("NOK")]
    [JsonStringEnumMemberName("NOK")]
    NOK,

    [XmlEnum("NPR")]
    [JsonStringEnumMemberName("NPR")]
    NPR,

    [XmlEnum("NZD")]
    [JsonStringEnumMemberName("NZD")]
    NZD,

    [XmlEnum("OMR")]
    [JsonStringEnumMemberName("OMR")]
    OMR,

    [XmlEnum("PAB")]
    [JsonStringEnumMemberName("PAB")]
    PAB,

    [XmlEnum("PEN")]
    [JsonStringEnumMemberName("PEN")]
    PEN,

    [XmlEnum("PGK")]
    [JsonStringEnumMemberName("PGK")]
    PGK,

    [XmlEnum("PHP")]
    [JsonStringEnumMemberName("PHP")]
    PHP,

    [XmlEnum("PKR")]
    [JsonStringEnumMemberName("PKR")]
    PKR,

    [XmlEnum("PLN")]
    [JsonStringEnumMemberName("PLN")]
    PLN,

    [XmlEnum("PYG")]
    [JsonStringEnumMemberName("PYG")]
    PYG,

    [XmlEnum("QAR")]
    [JsonStringEnumMemberName("QAR")]
    QAR,

    [XmlEnum("RON")]
    [JsonStringEnumMemberName("RON")]
    RON,

    [XmlEnum("RSD")]
    [JsonStringEnumMemberName("RSD")]
    RSD,

    [XmlEnum("RUB")]
    [JsonStringEnumMemberName("RUB")]
    RUB,

    [XmlEnum("RWF")]
    [JsonStringEnumMemberName("RWF")]
    RWF,

    [XmlEnum("SAR")]
    [JsonStringEnumMemberName("SAR")]
    SAR,

    [XmlEnum("SBD")]
    [JsonStringEnumMemberName("SBD")]
    SBD,

    [XmlEnum("SCR")]
    [JsonStringEnumMemberName("SCR")]
    SCR,

    [XmlEnum("SDG")]
    [JsonStringEnumMemberName("SDG")]
    SDG,

    [XmlEnum("SEK")]
    [JsonStringEnumMemberName("SEK")]
    SEK,

    [XmlEnum("SGD")]
    [JsonStringEnumMemberName("SGD")]
    SGD,

    [XmlEnum("SHP")]
    [JsonStringEnumMemberName("SHP")]
    SHP,

    [XmlEnum("SLL")]
    [JsonStringEnumMemberName("SLL")]
    SLL,

    [XmlEnum("SOS")]
    [JsonStringEnumMemberName("SOS")]
    SOS,

    [XmlEnum("SRD")]
    [JsonStringEnumMemberName("SRD")]
    SRD,

    [XmlEnum("SSP")]
    [JsonStringEnumMemberName("SSP")]
    SSP,

    [XmlEnum("STD")]
    [JsonStringEnumMemberName("STD")]
    STD,

    [XmlEnum("STN")]
    [JsonStringEnumMemberName("STN")]
    STN,

    [XmlEnum("SVC")]
    [JsonStringEnumMemberName("SVC")]
    SVC,

    [XmlEnum("SYP")]
    [JsonStringEnumMemberName("SYP")]
    SYP,

    [XmlEnum("SZL")]
    [JsonStringEnumMemberName("SZL")]
    SZL,

    [XmlEnum("THB")]
    [JsonStringEnumMemberName("THB")]
    THB,

    [XmlEnum("TJS")]
    [JsonStringEnumMemberName("TJS")]
    TJS,

    [XmlEnum("TMT")]
    [JsonStringEnumMemberName("TMT")]
    TMT,

    [XmlEnum("TND")]
    [JsonStringEnumMemberName("TND")]
    TND,

    [XmlEnum("TOP")]
    [JsonStringEnumMemberName("TOP")]
    TOP,

    [XmlEnum("TRY")]
    [JsonStringEnumMemberName("TRY")]
    TRY,

    [XmlEnum("TTD")]
    [JsonStringEnumMemberName("TTD")]
    TTD,

    [XmlEnum("TWD")]
    [JsonStringEnumMemberName("TWD")]
    TWD,

    [XmlEnum("TZS")]
    [JsonStringEnumMemberName("TZS")]
    TZS,

    [XmlEnum("UAH")]
    [JsonStringEnumMemberName("UAH")]
    UAH,

    [XmlEnum("UGX")]
    [JsonStringEnumMemberName("UGX")]
    UGX,

    [XmlEnum("USD")]
    [JsonStringEnumMemberName("USD")]
    USD,

    [XmlEnum("USN")]
    [JsonStringEnumMemberName("USN")]
    USN,

    [XmlEnum("USS")]
    [JsonStringEnumMemberName("USS")]
    USS,

    [XmlEnum("UYI")]
    [JsonStringEnumMemberName("UYI")]
    UYI,

    [XmlEnum("UYU")]
    [JsonStringEnumMemberName("UYU")]
    UYU,

    [XmlEnum("UZS")]
    [JsonStringEnumMemberName("UZS")]
    UZS,

    [XmlEnum("VEF")]
    [JsonStringEnumMemberName("VEF")]
    VEF,

    [XmlEnum("VES")]
    [JsonStringEnumMemberName("VES")]
    VES,

    [XmlEnum("VND")]
    [JsonStringEnumMemberName("VND")]
    VND,

    [XmlEnum("VUV")]
    [JsonStringEnumMemberName("VUV")]
    VUV,

    [XmlEnum("WST")]
    [JsonStringEnumMemberName("WST")]
    WST,

    [XmlEnum("XAF")]
    [JsonStringEnumMemberName("XAF")]
    XAF,

    [XmlEnum("XAG")]
    [JsonStringEnumMemberName("XAG")]
    XAG,

    [XmlEnum("XAU")]
    [JsonStringEnumMemberName("XAU")]
    XAU,

    [XmlEnum("XBA")]
    [JsonStringEnumMemberName("XBA")]
    XBA,

    [XmlEnum("XBB")]
    [JsonStringEnumMemberName("XBB")]
    XBB,

    [XmlEnum("XBC")]
    [JsonStringEnumMemberName("XBC")]
    XBC,

    [XmlEnum("XBD")]
    [JsonStringEnumMemberName("XBD")]
    XBD,

    [XmlEnum("XCD")]
    [JsonStringEnumMemberName("XCD")]
    XCD,

    [XmlEnum("XDR")]
    [JsonStringEnumMemberName("XDR")]
    XDR,

    [XmlEnum("XFU")]
    [JsonStringEnumMemberName("XFU")]
    XFU,

    [XmlEnum("XOF")]
    [JsonStringEnumMemberName("XOF")]
    XOF,

    [XmlEnum("XPD")]
    [JsonStringEnumMemberName("XPD")]
    XPD,

    [XmlEnum("XPF")]
    [JsonStringEnumMemberName("XPF")]
    XPF,

    [XmlEnum("XPT")]
    [JsonStringEnumMemberName("XPT")]
    XPT,

    [XmlEnum("XSU")]
    [JsonStringEnumMemberName("XSU")]
    XSU,

    [XmlEnum("XUA")]
    [JsonStringEnumMemberName("XUA")]
    XUA,

    [XmlEnum("YER")]
    [JsonStringEnumMemberName("YER")]
    YER,

    [XmlEnum("ZAR")]
    [JsonStringEnumMemberName("ZAR")]
    ZAR,

    [XmlEnum("ZMW")]
    [JsonStringEnumMemberName("ZMW")]
    ZMW,

    [XmlEnum("ZWL")]
    [JsonStringEnumMemberName("ZWL")]
    ZWL,

    [XmlEnum("EEK")]
    [JsonStringEnumMemberName("EEK")]
    EEK,

    [XmlEnum("SKK")]
    [JsonStringEnumMemberName("SKK")]
    SKK,

    [XmlEnum("TMM")]
    [JsonStringEnumMemberName("TMM")]
    TMM,

    [XmlEnum("ZMK")]
    [JsonStringEnumMemberName("ZMK")]
    ZMK,

    [XmlEnum("ZWD")]
    [JsonStringEnumMemberName("ZWD")]
    ZWD,

    [XmlEnum("ZWR")]
    [JsonStringEnumMemberName("ZWR")]
    ZWR
}
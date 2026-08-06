/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace PChouse.AT.Generics;

/// <summary>
/// SAF-T(PT) Country enumeration alfa 2 characters, norm ISO 3166-1
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Country
{
    [XmlEnum("AD")]
    [JsonStringEnumMemberName("AD")]
    AD,

    [XmlEnum("AE")]
    [JsonStringEnumMemberName("AE")]
    AE,

    [XmlEnum("AF")]
    [JsonStringEnumMemberName("AF")]
    AF,

    [XmlEnum("AG")]
    [JsonStringEnumMemberName("AG")]
    AG,

    [XmlEnum("AI")]
    [JsonStringEnumMemberName("AI")]
    AI,

    [XmlEnum("AL")]
    [JsonStringEnumMemberName("AL")]
    AL,

    [XmlEnum("AM")]
    [JsonStringEnumMemberName("AM")]
    AM,

    [XmlEnum("AO")]
    [JsonStringEnumMemberName("AO")]
    AO,

    [XmlEnum("AQ")]
    [JsonStringEnumMemberName("AQ")]
    AQ,

    [XmlEnum("AR")]
    [JsonStringEnumMemberName("AR")]
    AR,

    [XmlEnum("AS")]
    [JsonStringEnumMemberName("AS")]
    AS,

    [XmlEnum("AT")]
    [JsonStringEnumMemberName("AT")]
    AT,

    [XmlEnum("AU")]
    [JsonStringEnumMemberName("AU")]
    AU,

    [XmlEnum("AW")]
    [JsonStringEnumMemberName("AW")]
    AW,

    [XmlEnum("AX")]
    [JsonStringEnumMemberName("AX")]
    AX,

    [XmlEnum("AZ")]
    [JsonStringEnumMemberName("AZ")]
    AZ,

    [XmlEnum("BA")]

    [JsonStringEnumMemberName("BA")]
    BA,

    [XmlEnum("BB")]
    [JsonStringEnumMemberName("BB")]
    BB,

    [XmlEnum("BD")]
    [JsonStringEnumMemberName("BD")]
    BD,

    [XmlEnum("BE")]
    [JsonStringEnumMemberName("BE")]
    BE,

    [XmlEnum("BF")]
    [JsonStringEnumMemberName("BF")]
    BF,

    [XmlEnum("BG")]
    [JsonStringEnumMemberName("BG")]
    BG,

    [XmlEnum("BH")]
    [JsonStringEnumMemberName("BH")]
    BH,

    [XmlEnum("BI")]
    [JsonStringEnumMemberName("BI")]
    BI,

    [XmlEnum("BJ")]
    [JsonStringEnumMemberName("BJ")]
    BJ,

    [XmlEnum("BL")]
    [JsonStringEnumMemberName("BL")]
    BL,

    [XmlEnum("BM")]
    [JsonStringEnumMemberName("BM")]
    BM,

    [XmlEnum("BN")]
    [JsonStringEnumMemberName("BN")]
    BN,

    [XmlEnum("BO")]
    [JsonStringEnumMemberName("BO")]
    BO,

    [XmlEnum("BQ")]
    [JsonStringEnumMemberName("BQ")]
    BQ,

    [XmlEnum("BR")]
    [JsonStringEnumMemberName("BR")]
    BR,

    [XmlEnum("BS")]
    [JsonStringEnumMemberName("BS")]
    BS,

    [XmlEnum("BT")]
    [JsonStringEnumMemberName("BT")]
    BT,

    [XmlEnum("BV")]
    [JsonStringEnumMemberName("BV")]
    BV,

    [XmlEnum("BW")]
    [JsonStringEnumMemberName("BW")]
    BW,

    [XmlEnum("BY")]
    [JsonStringEnumMemberName("BY")]
    BY,

    [XmlEnum("BZ")]
    [JsonStringEnumMemberName("BZ")]
    BZ,

    [XmlEnum("CA")]
    [JsonStringEnumMemberName("CA")]
    CA,

    [XmlEnum("CC")]
    [JsonStringEnumMemberName("CC")]
    CC,

    [XmlEnum("CD")]
    [JsonStringEnumMemberName("CD")]
    CD,

    [XmlEnum("CF")]
    [JsonStringEnumMemberName("CF")]
    CF,

    [XmlEnum("CG")]
    [JsonStringEnumMemberName("CG")]
    CG,

    [XmlEnum("CH")]
    [JsonStringEnumMemberName("CH")]
    CH,

    [XmlEnum("CI")]
    [JsonStringEnumMemberName("CI")]
    CI,

    [XmlEnum("CK")]
    [JsonStringEnumMemberName("CK")]
    CK,

    [XmlEnum("CL")]
    [JsonStringEnumMemberName("CL")]
    CL,

    [XmlEnum("CM")]
    [JsonStringEnumMemberName("CM")]
    CM,

    [XmlEnum("CN")]
    [JsonStringEnumMemberName("CN")]
    CN,

    [XmlEnum("CO")]
    [JsonStringEnumMemberName("CO")]
    CO,

    [XmlEnum("CR")]
    [JsonStringEnumMemberName("CR")]
    CR,

    [XmlEnum("CU")]
    [JsonStringEnumMemberName("CU")]
    CU,

    [XmlEnum("CV")]
    [JsonStringEnumMemberName("CV")]
    CV,

    [XmlEnum("CW")]
    [JsonStringEnumMemberName("CW")]
    CW,

    [XmlEnum("CX")]
    [JsonStringEnumMemberName("CX")]
    CX,

    [XmlEnum("CY")]
    [JsonStringEnumMemberName("CY")]
    CY,

    [XmlEnum("CZ")]
    [JsonStringEnumMemberName("CZ")]
    CZ,

    [XmlEnum("DE")]
    [JsonStringEnumMemberName("DE")]
    DE,

    [XmlEnum("DJ")]
    [JsonStringEnumMemberName("DJ")]
    DJ,

    [XmlEnum("DK")]
    [JsonStringEnumMemberName("DK")]
    DK,

    [XmlEnum("DM")]
    [JsonStringEnumMemberName("DM")]
    DM,

    [XmlEnum("DO")]
    [JsonStringEnumMemberName("DO")]
    DO,

    [XmlEnum("DZ")]
    [JsonStringEnumMemberName("DZ")]
    DZ,

    [XmlEnum("EC")]
    [JsonStringEnumMemberName("EC")]
    EC,

    [XmlEnum("EE")]
    [JsonStringEnumMemberName("EE")]
    EE,

    [XmlEnum("EG")]
    [JsonStringEnumMemberName("EG")]
    EG,

    [XmlEnum("EH")]
    [JsonStringEnumMemberName("EH")]
    EH,

    [XmlEnum("ER")]
    [JsonStringEnumMemberName("ER")]
    ER,

    [XmlEnum("ES")]
    [JsonStringEnumMemberName("ES")]
    ES,

    [XmlEnum("ET")]
    [JsonStringEnumMemberName("ET")]
    ET,

    [XmlEnum("FI")]
    [JsonStringEnumMemberName("FI")]
    FI,

    [XmlEnum("FJ")]
    [JsonStringEnumMemberName("FJ")]
    FJ,

    [XmlEnum("FK")]
    [JsonStringEnumMemberName("FK")]
    FK,

    [XmlEnum("FM")]
    [JsonStringEnumMemberName("FM")]
    FM,

    [XmlEnum("FO")]
    [JsonStringEnumMemberName("FO")]
    FO,

    [XmlEnum("FR")]
    [JsonStringEnumMemberName("FR")]
    FR,

    [XmlEnum("GA")]
    [JsonStringEnumMemberName("GA")]
    GA,

    [XmlEnum("GB")]
    [JsonStringEnumMemberName("GB")]
    GB,

    [XmlEnum("GD")]
    [JsonStringEnumMemberName("GD")]
    GD,

    [XmlEnum("GE")]
    [JsonStringEnumMemberName("GE")]
    GE,

    [XmlEnum("GF")]
    [JsonStringEnumMemberName("GF")]
    GF,

    [XmlEnum("GG")]
    [JsonStringEnumMemberName("GG")]
    GG,

    [XmlEnum("GH")]
    [JsonStringEnumMemberName("GH")]
    GH,

    [XmlEnum("GI")]
    [JsonStringEnumMemberName("GI")]
    GI,

    [XmlEnum("GL")]
    [JsonStringEnumMemberName("GL")]
    GL,

    [XmlEnum("GM")]
    [JsonStringEnumMemberName("GM")]
    GM,

    [XmlEnum("GN")]
    [JsonStringEnumMemberName("GN")]
    GN,

    [XmlEnum("GP")]
    [JsonStringEnumMemberName("GP")]
    GP,

    [XmlEnum("GQ")]
    [JsonStringEnumMemberName("GQ")]
    GQ,

    [XmlEnum("GR")]
    [JsonStringEnumMemberName("GR")]
    GR,

    [XmlEnum("GS")]

    [JsonStringEnumMemberName("GS")]
    GS,

    [XmlEnum("GT")]
    [JsonStringEnumMemberName("GT")]
    GT,

    [XmlEnum("GU")]
    [JsonStringEnumMemberName("GU")]
    GU,

    [XmlEnum("GW")]

    [JsonStringEnumMemberName("GW")]
    GW,

    [XmlEnum("GY")]
    [JsonStringEnumMemberName("GY")]
    GY,

    [XmlEnum("HK")]
    [JsonStringEnumMemberName("HK")]
    HK,

    [XmlEnum("HM")]
    [JsonStringEnumMemberName("HM")]
    HM,

    [XmlEnum("HN")]
    [JsonStringEnumMemberName("HN")]
    HN,

    [XmlEnum("HR")]
    [JsonStringEnumMemberName("HR")]
    HR,

    [XmlEnum("HT")]
    [JsonStringEnumMemberName("HT")]
    HT,

    [XmlEnum("HU")]
    [JsonStringEnumMemberName("HU")]
    HU,

    [XmlEnum("ID")]
    [JsonStringEnumMemberName("ID")]
    ID,

    [XmlEnum("IE")]
    [JsonStringEnumMemberName("IE")]
    IE,

    [XmlEnum("IL")]
    [JsonStringEnumMemberName("IL")]
    IL,

    [XmlEnum("IM")]
    [JsonStringEnumMemberName("IM")]
    IM,

    [XmlEnum("IN")]
    [JsonStringEnumMemberName("IN")]
    IN,

    [XmlEnum("IO")]
    [JsonStringEnumMemberName("IO")]
    IO,

    [XmlEnum("IQ")]
    [JsonStringEnumMemberName("IQ")]
    IQ,

    [XmlEnum("IR")]
    [JsonStringEnumMemberName("IR")]
    IR,

    [XmlEnum("IS")]
    [JsonStringEnumMemberName("IS")]
    IS,

    [XmlEnum("IT")]
    [JsonStringEnumMemberName("IT")]
    IT,

    [XmlEnum("JE")]
    [JsonStringEnumMemberName("JE")]
    JE,

    [XmlEnum("JM")]
    [JsonStringEnumMemberName("JM")]
    JM,

    [XmlEnum("JO")]
    [JsonStringEnumMemberName("JO")]
    JO,

    [XmlEnum("JP")]
    [JsonStringEnumMemberName("JP")]
    JP,

    [XmlEnum("KE")]
    [JsonStringEnumMemberName("KE")]
    KE,

    [XmlEnum("KG")]
    [JsonStringEnumMemberName("KG")]
    KG,

    [XmlEnum("KH")]
    [JsonStringEnumMemberName("KH")]
    KH,

    [XmlEnum("KI")]
    [JsonStringEnumMemberName("KI")]
    KI,

    [XmlEnum("KM")]
    [JsonStringEnumMemberName("KM")]
    KM,

    [XmlEnum("KN")]
    [JsonStringEnumMemberName("KN")]
    KN,

    [XmlEnum("KP")]
    [JsonStringEnumMemberName("KP")]
    KP,

    [XmlEnum("KR")]
    [JsonStringEnumMemberName("KR")]
    KR,

    [XmlEnum("KW")]
    [JsonStringEnumMemberName("KW")]
    KW,

    [XmlEnum("KY")]
    [JsonStringEnumMemberName("KY")]
    KY,

    [XmlEnum("KZ")]
    [JsonStringEnumMemberName("KZ")]
    KZ,

    [XmlEnum("LA")]

    [JsonStringEnumMemberName("LA")]
    LA,

    [XmlEnum("LB")]
    [JsonStringEnumMemberName("LB")]
    LB,

    [XmlEnum("LC")]

    [JsonStringEnumMemberName("LC")]
    LC,

    [XmlEnum("LI")]
    [JsonStringEnumMemberName("LI")]
    LI,

    [XmlEnum("LK")]
    [JsonStringEnumMemberName("LK")]
    LK,

    [XmlEnum("LR")]

    [JsonStringEnumMemberName("LR")]
    LR,

    [XmlEnum("LS")]
    [JsonStringEnumMemberName("LS")]
    LS,

    [XmlEnum("LT")]
    [JsonStringEnumMemberName("LT")]
    LT,

    [XmlEnum("LU")]
    [JsonStringEnumMemberName("LU")]
    LU,

    [XmlEnum("LV")]
    [JsonStringEnumMemberName("LV")]
    LV,

    [XmlEnum("LY")]
    [JsonStringEnumMemberName("LY")]
    LY,

    [XmlEnum("MA")]
    [JsonStringEnumMemberName("MA")]
    MA,

    [XmlEnum("MC")]
    [JsonStringEnumMemberName("MC")]
    MC,

    [XmlEnum("MD")]
    [JsonStringEnumMemberName("MD")]
    MD,

    [XmlEnum("ME")]
    [JsonStringEnumMemberName("ME")]
    ME,

    [XmlEnum("MF")]
    [JsonStringEnumMemberName("MF")]
    MF,

    [XmlEnum("MG")]
    [JsonStringEnumMemberName("MG")]
    MG,

    [XmlEnum("MH")]
    [JsonStringEnumMemberName("MH")]
    MH,

    [XmlEnum("MK")]
    [JsonStringEnumMemberName("MK")]
    MK,

    [XmlEnum("ML")]
    [JsonStringEnumMemberName("ML")]
    ML,

    [XmlEnum("MM")]
    [JsonStringEnumMemberName("MM")]
    MM,

    [XmlEnum("MN")]
    [JsonStringEnumMemberName("MN")]
    MN,

    [XmlEnum("MO")]
    [JsonStringEnumMemberName("MO")]
    MO,

    [XmlEnum("MP")]
    [JsonStringEnumMemberName("MP")]
    MP,

    [XmlEnum("MQ")]
    [JsonStringEnumMemberName("MQ")]
    MQ,

    [XmlEnum("MR")]
    [JsonStringEnumMemberName("MR")]
    MR,

    [XmlEnum("MS")]
    [JsonStringEnumMemberName("MS")]
    MS,

    [XmlEnum("MT")]
    [JsonStringEnumMemberName("MT")]
    MT,

    [XmlEnum("MU")]
    [JsonStringEnumMemberName("MU")]
    MU,

    [XmlEnum("MV")]
    [JsonStringEnumMemberName("MV")]
    MV,

    [XmlEnum("MW")]
    [JsonStringEnumMemberName("MW")]
    MW,

    [XmlEnum("MX")]
    [JsonStringEnumMemberName("MX")]
    MX,

    [XmlEnum("MY")]
    [JsonStringEnumMemberName("MY")]
    MY,

    [XmlEnum("MZ")]
    [JsonStringEnumMemberName("MZ")]
    MZ,

    [XmlEnum("NA")]
    [JsonStringEnumMemberName("NA")]
    NA,

    [XmlEnum("NC")]
    [JsonStringEnumMemberName("NC")]
    NC,

    [XmlEnum("NE")]
    [JsonStringEnumMemberName("NE")]
    NE,

    [XmlEnum("NF")]
    [JsonStringEnumMemberName("NF")]
    NF,

    [XmlEnum("NG")]
    [JsonStringEnumMemberName("NG")]
    NG,

    [XmlEnum("NI")]
    [JsonStringEnumMemberName("NI")]
    NI,

    [XmlEnum("NL")]
    [JsonStringEnumMemberName("NL")]
    NL,

    [XmlEnum("NO")]
    [JsonStringEnumMemberName("NO")]
    NO,

    [XmlEnum("NP")]
    [JsonStringEnumMemberName("NP")]
    NP,

    [XmlEnum("NR")]
    [JsonStringEnumMemberName("NR")]
    NR,

    [XmlEnum("NU")]
    [JsonStringEnumMemberName("NU")]
    NU,

    [XmlEnum("NZ")]
    [JsonStringEnumMemberName("NZ")]
    NZ,

    [XmlEnum("OM")]
    [JsonStringEnumMemberName("OM")]
    OM,

    [XmlEnum("PA")]

    [JsonStringEnumMemberName("PA")]
    PA,

    [XmlEnum("PE")]
    [JsonStringEnumMemberName("PE")]
    PE,

    [XmlEnum("PF")]
    [JsonStringEnumMemberName("PF")]
    PF,

    [XmlEnum("PG")]
    [JsonStringEnumMemberName("PG")]
    PG,

    [XmlEnum("PH")]
    [JsonStringEnumMemberName("PH")]
    PH,

    [XmlEnum("PK")]
    [JsonStringEnumMemberName("PK")]
    PK,

    [XmlEnum("PL")]
    [JsonStringEnumMemberName("PL")]
    PL,

    [XmlEnum("PM")]
    [JsonStringEnumMemberName("PM")]
    PM,

    [XmlEnum("PN")]
    [JsonStringEnumMemberName("PN")]
    PN,

    [XmlEnum("PR")]
    [JsonStringEnumMemberName("PR")]
    PR,

    [XmlEnum("PS")]
    [JsonStringEnumMemberName("PS")]
    PS,

    [XmlEnum("PT")]
    [JsonStringEnumMemberName("PT")]
    PT,

    [XmlEnum("PW")]
    [JsonStringEnumMemberName("PW")]
    PW,

    [XmlEnum("PY")]
    [JsonStringEnumMemberName("PY")]
    PY,

    [XmlEnum("QA")]
    [JsonStringEnumMemberName("QA")]
    QA,

    [XmlEnum("RE")]
    [JsonStringEnumMemberName("RE")]
    RE,

    [XmlEnum("RO")]
    [JsonStringEnumMemberName("RO")]
    RO,

    [XmlEnum("RS")]
    [JsonStringEnumMemberName("RS")]
    RS,

    [XmlEnum("RU")]
    [JsonStringEnumMemberName("RU")]
    RU,

    [XmlEnum("RW")]
    [JsonStringEnumMemberName("RW")]
    RW,

    [XmlEnum("SA")]
    [JsonStringEnumMemberName("SA")]
    SA,

    [XmlEnum("SB")]
    [JsonStringEnumMemberName("SB")]
    SB,

    [XmlEnum("SC")]
    [JsonStringEnumMemberName("SC")]
    SC,

    [XmlEnum("SD")]
    [JsonStringEnumMemberName("SD")]
    SD,

    [XmlEnum("SE")]
    [JsonStringEnumMemberName("SE")]
    SE,

    [XmlEnum("SG")]
    [JsonStringEnumMemberName("SG")]
    SG,

    [XmlEnum("SH")]
    [JsonStringEnumMemberName("SH")]
    SH,

    [XmlEnum("SI")]
    [JsonStringEnumMemberName("SI")]
    SI,

    [XmlEnum("SJ")]
    [JsonStringEnumMemberName("SJ")]
    SJ,

    [XmlEnum("SK")]
    [JsonStringEnumMemberName("SK")]
    SK,

    [XmlEnum("SL")]
    [JsonStringEnumMemberName("SL")]
    SL,

    [XmlEnum("SM")]
    [JsonStringEnumMemberName("SM")]
    SM,

    [XmlEnum("SN")]
    [JsonStringEnumMemberName("SN")]
    SN,

    [XmlEnum("SO")]
    [JsonStringEnumMemberName("SO")]
    SO,

    [XmlEnum("SR")]
    [JsonStringEnumMemberName("SR")]
    SR,

    [XmlEnum("SS")]
    [JsonStringEnumMemberName("SS")]
    SS,

    [XmlEnum("ST")]
    [JsonStringEnumMemberName("ST")]
    ST,

    [XmlEnum("SV")]
    [JsonStringEnumMemberName("SV")]
    SV,

    [XmlEnum("SX")]
    [JsonStringEnumMemberName("SX")]
    SX,

    [XmlEnum("SY")]
    [JsonStringEnumMemberName("SY")]
    SY,

    [XmlEnum("SZ")]
    [JsonStringEnumMemberName("SZ")]
    SZ,

    [XmlEnum("TC")]
    [JsonStringEnumMemberName("TC")]
    TC,

    [XmlEnum("TD")]
    [JsonStringEnumMemberName("TD")]
    TD,

    [XmlEnum("TF")]
    [JsonStringEnumMemberName("TF")]
    TF,

    [XmlEnum("TG")]
    [JsonStringEnumMemberName("TG")]
    TG,

    [XmlEnum("TH")]
    [JsonStringEnumMemberName("TH")]
    TH,

    [XmlEnum("TJ")]
    [JsonStringEnumMemberName("TJ")]
    TJ,

    [XmlEnum("TK")]
    [JsonStringEnumMemberName("TK")]
    TK,

    [XmlEnum("TL")]
    [JsonStringEnumMemberName("TL")]
    TL,

    [XmlEnum("TM")]
    [JsonStringEnumMemberName("TM")]
    TM,

    [XmlEnum("TN")]
    [JsonStringEnumMemberName("TN")]
    TN,

    [XmlEnum("TO")]
    [JsonStringEnumMemberName("TO")]
    TO,

    [XmlEnum("TR")]
    [JsonStringEnumMemberName("TR")]
    TR,

    [XmlEnum("TT")]
    [JsonStringEnumMemberName("TT")]
    TT,

    [XmlEnum("TV")]
    [JsonStringEnumMemberName("TV")]
    TV,

    [XmlEnum("TW")]
    [JsonStringEnumMemberName("TW")]
    TW,

    [XmlEnum("TZ")]
    [JsonStringEnumMemberName("TZ")]
    TZ,

    [XmlEnum("UA")]
    [JsonStringEnumMemberName("UA")]
    UA,

    [XmlEnum("UG")]
    [JsonStringEnumMemberName("UG")]
    UG,

    [XmlEnum("UM")]
    [JsonStringEnumMemberName("UM")]
    UM,

    [XmlEnum("US")]
    [JsonStringEnumMemberName("US")]
    US,

    [XmlEnum("UY")]
    [JsonStringEnumMemberName("UY")]
    UY,

    [XmlEnum("UZ")]
    [JsonStringEnumMemberName("UZ")]
    UZ,

    [XmlEnum("VA")]
    [JsonStringEnumMemberName("VA")]
    VA,

    [XmlEnum("VC")]
    [JsonStringEnumMemberName("VC")]
    VC,

    [XmlEnum("VE")]
    [JsonStringEnumMemberName("VE")]
    VE,

    [XmlEnum("VG")]
    [JsonStringEnumMemberName("VG")]
    VG,

    [XmlEnum("VI")]
    [JsonStringEnumMemberName("VI")]
    VI,

    [XmlEnum("VN")]
    [JsonStringEnumMemberName("VN")]
    VN,

    [XmlEnum("VU")]
    [JsonStringEnumMemberName("VU")]
    VU,

    [XmlEnum("WF")]
    [JsonStringEnumMemberName("WF")]
    WF,

    [XmlEnum("WS")]
    [JsonStringEnumMemberName("WS")]
    WS,

    [XmlEnum("XK")]
    [JsonStringEnumMemberName("XK")]
    XK,

    [XmlEnum("YE")]
    [JsonStringEnumMemberName("YE")]
    YE,

    [XmlEnum("YT")]
    [JsonStringEnumMemberName("YT")]
    YT,

    [XmlEnum("ZA")]
    [JsonStringEnumMemberName("ZA")]
    ZA,

    [XmlEnum("ZM")]
    [JsonStringEnumMemberName("ZM")]
    ZM,

    [XmlEnum("ZW")]
    [JsonStringEnumMemberName("ZW")]
    ZW,

    [XmlEnum("Desconhecido")]
    [JsonStringEnumMemberName("Desconhecido")]
    Unknown
}

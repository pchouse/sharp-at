/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace PChouse.AT.Generics;

/// <summary>
/// SAF-T(PT) Payment Mechanism
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum PaymentMechanism
{

    /// <summary>
    /// “CC” - Credit card;
    /// </summary>
    [XmlEnum("CC")]
    [JsonStringEnumMemberName("CC")]
    CC,

    /// <summary>
    /// “CD” - Debit card;
    /// </summary>
    [XmlEnum("CD")]
    [JsonStringEnumMemberName("CD")]
    CD,

    /// <summary>
    /// “CH” - Bank cheque
    /// </summary>
    [XmlEnum("CH")]
    [JsonStringEnumMemberName("CH")]
    CH,

    /// <summary>
    /// “CI” – International Letter of Credit 
    /// </summary>
    [XmlEnum("CI")]
    [JsonStringEnumMemberName("CI")]
    CI,

    /// <summary>
    /// “CO” - Gift cheque or gift card 
    /// </summary>
    [XmlEnum("CO")]
    [JsonStringEnumMemberName("CO")]
    CO,

    /// <summary>
    /// “CS” - Balance compensation in current account 
    /// </summary>
    [XmlEnum("CS")]
    [JsonStringEnumMemberName("CS")]
    CS,

    /// <summary>
    /// “DE” - Electronic Money, for example, on fidelity or points cards<br> 
    /// </summary>
    [XmlEnum("DE")]
    [JsonStringEnumMemberName("DE")]
    DE,

    /// <summary>
    /// “LC” - Commercial Bill 
    /// </summary>
    [XmlEnum("LC")]
    [JsonStringEnumMemberName("LC")]
    LC,

    /// <summary>
    /// “MB” - Payment references for ATM
    /// </summary>
    [XmlEnum("MB")]
    [JsonStringEnumMemberName("MB")]
    MB,

    /// <summary>
    /// NU “NU” – Cash 
    /// </summary>
    [XmlEnum("NU")]
    [JsonStringEnumMemberName("NU")]
    NU,

    /// <summary>
    /// “OU” – Other means not mentioned 
    /// </summary>
    [XmlEnum("OU")]
    [JsonStringEnumMemberName("OU")]
    OU,

    /**
     * “PR” – Exchange of goods<br>
     * &lt;xs:enumeration value="PR"/&gt;
     * @since 3.0.0
     */
    /// <summary>
    /// 
    /// </summary>
    [XmlEnum("PR")]
    [JsonStringEnumMemberName("PR")]
    PR,

    /// <summary>
    /// “TB” – Banking transfer or authorized direct debit<br> 
    /// </summary>
    [XmlEnum("TB")]
    [JsonStringEnumMemberName("TB")]
    TB,

    /// <summary>
    /// “TR” - Non-wage compensation titles regardless of their support 
    /// </summary>
    [XmlEnum("TR")]
    [JsonStringEnumMemberName("TR")]
    TR

}
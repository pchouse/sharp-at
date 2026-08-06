/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace PChouse.AT.Generics;

/// <summary>
/// SAF-T(PT) Movement Type
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum MovementType
{

    /// <summary>
    /// "GR" - Delivery note
    /// </summary>
    [XmlEnum("GR")]
    [JsonStringEnumMemberName("GR")]
    GR,

    /// <summary>
    /// "GT" - Transport guide (include here the global transport documents) 
    /// </summary>
    [XmlEnum("GT")]
    [JsonStringEnumMemberName("GT")]
    GT,

    /// <summary>
    /// “GA” – Transport document for own fixed assets 
    /// </summary>
    [XmlEnum("GA")]
    [JsonStringEnumMemberName("GA")]
    GA,

    /// <summary>
    /// “GC” - Consignment note
    /// </summary>
    [XmlEnum("GC")]
    [JsonStringEnumMemberName("GC")]
    GC,

    /// <summary>
    /// “GD” – Return note
    /// </summary>
    [XmlEnum("GD")]
    [JsonStringEnumMemberName("GD")]
    GD

}
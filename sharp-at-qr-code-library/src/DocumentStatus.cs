/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Text.Json.Serialization;

namespace PChouse.AT.QRCode.Library;

/// <summary>
/// QR code documents status
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum DocumentStatus
{
    /// <summary>
    /// “N” - Normal
    /// </summary>
    [JsonStringEnumMemberName("N")]
    N,

    /// <summary>
    /// S for self-invoicing "Autofaturacao"
    /// </summary>
    [JsonStringEnumMemberName("S")]
    S,

    /// <summary>
    /// “A” - Cancelled document
    /// </summary>
    [JsonStringEnumMemberName("A")]
    A,

    /// <summary>
    /// “R” - Summary document for other documents created in other applications and generated in this application
    /// </summary>
    [JsonStringEnumMemberName("R")]
    R,

    /// <summary>
    /// “F” - Invoiced document
    /// “F” – Billed document, even if partially, when for the same document there is
    /// also on table 4.1. – SalesInvoices the corresponding invoice or simplified invoice
    /// </summary>
    [JsonStringEnumMemberName("F")]
    F,

    /// <summary>
    /// “T” - On behalf of third parties
    /// </summary>
    [JsonStringEnumMemberName("T")]
    T,

}

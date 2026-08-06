/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Text.Json.Serialization;

namespace PChouse.Utils.PDFApiClient.Multicert;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SignatureType
{
    /// <summary>
    /// PAdES: Usually applied to PDF files.
    /// </summary>
    [JsonStringEnumMemberName(nameof(PADES))]
    PADES,

    /// <summary>
    /// XAdES: Usually applied to XML text files.
    /// </summary>
    [JsonStringEnumMemberName(nameof(XADES))]
    XADES,

    /// <summary>
    /// CAdES: Usually applied to binary files.
    /// </summary>
    [JsonStringEnumMemberName(nameof(CADES))]
    CADES
}


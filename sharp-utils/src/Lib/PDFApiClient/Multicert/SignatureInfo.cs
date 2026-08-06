/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Text.Json.Serialization;

namespace PChouse.Utils.PDFApiClient.Multicert;

/// <summary>
/// Signature information to be applied to a document.
/// </summary>
public class SignatureInfo
{

    /// <summary>
    /// PAdES: Usually applied to PDF files.
    /// XAdES: Usually applied to XML text files.
    /// CAdES: Usually applied to binary files.
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("signatureType")]
    public virtual SignatureType SignatureType { get; set; } = SignatureType.PADES;

    /// <summary>
    /// Visible signature information attributes to be applied to a document.Restricted support for PDF documents only.
    /// </summary>
    [JsonPropertyName("visibleSignatureInfo")]
    public virtual VisibleSignatureInfo? VisibleSignatureInfo { get; set; } = default;

    /// <summary>
    /// Signature reason (free text) to be set on document metadata.Support is restricted to PAdES based signature types.
    /// </summary>
    [JsonPropertyName("reason")]
    public virtual string? Reason { get; set; } = default;

    /// <summary>
    /// Signature location (free text) to be set on document metadata.
    /// Support is restricted to PAdES based signature types.
    /// </summary>
    [JsonPropertyName("location")]
    public virtual string? Location { get; set; } = default;

    /// <summary>
    /// Will apply timestamp to signature. Will use the default system timestamp server.
    /// </summary>    
    [JsonPropertyName("applyTimestamp")]
    public virtual bool ApplyTimestamp { get; set; } = false;

    /// <summary>
    /// Advanced signature information attributes to be applied to a document. Restricted support for XML documents only.
    /// </summary>
    [JsonPropertyName("advancedSignatureInfo")]
    public virtual AdvancedSignatureInfo? AdvancedSignatureInfo { get; set; } = default;

    /// <summary>
    /// Default constructor for SignatureInfo.
    /// </summary>
    public SignatureInfo()
    {

    }

    /// <summary>
    /// Constructor for SignatureInfo with parameters.
    /// </summary>
    /// <param name="reason"></param>
    /// <param name="location"></param>
    public SignatureInfo(string reason, string location)
    {
        Reason = reason;
        Location = location;
    }

    /// <summary>
    /// Constructor for SignatureInfo with parameters.
    /// </summary>
    /// <param name="visibleSignatureInfo"></param>
    /// <param name="reason"></param>
    /// <param name="location"></param>
    public SignatureInfo(VisibleSignatureInfo visibleSignatureInfo, string reason, string location) :
        this(reason, location)
    {
        VisibleSignatureInfo = visibleSignatureInfo;
    }

}


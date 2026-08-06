/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Text.Json.Serialization;

namespace PChouse.Utils.PDFApiClient;

public partial class RequestSign<T> where T : ISignPDF
{
    /// <summary>
    /// String with the document content in base64 format.
    /// </summary>
    [JsonPropertyName("pdfBase64")]
    public virtual required string PdfBase64 { get; set; }

    /// <summary>
    /// The signature provider to be used for signing the document.
    /// </summary>
    [JsonPropertyName("signPDF")]
    public virtual required T SignPDF { get; set; }

    [JsonExtensionData]
    public IDictionary<string, object> AdditionalProperties { get; set; } = new Dictionary<string, object>();
    
    /// <summary>
    /// Constructor for the RequestSign class.
    /// </summary>
    public RequestSign()
    {

    }

    /// <summary>
    /// Constructor for the RequestSign class with parameters.
    /// </summary>
    /// <param name="pdfBase64"></param>
    /// <param name="signPDF"></param>
    public RequestSign(string pdfBase64, T signPDF)
    {
        PdfBase64 = pdfBase64;
        SignPDF = signPDF;
    }

}

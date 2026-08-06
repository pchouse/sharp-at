/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Text.Json.Serialization;

namespace PChouse.Utils.PDFApiClient;

public class RequestPdfToImage
{

    [JsonPropertyName("pdf")]
    public virtual required string Pdf { get; set; }

    [JsonPropertyName("dpi")]
    public virtual float? Dpi { get; set; } = default;
}


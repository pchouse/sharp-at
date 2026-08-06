/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Text.Json.Serialization;

namespace PChouse.Utils.PDFApiClient;

public class PdfSignature
{
    [JsonPropertyName("location")]
    public virtual string? Location { get; set; } = default;

    [JsonPropertyName("reason")]
    public virtual string? Reason { get; set; } = default;

    [JsonPropertyName("contactInfo")]
    public virtual string? ContactInfo { get; set; } = default;

    [JsonPropertyName("name")]
    public virtual string? Name { get; set; } = default;

    [JsonPropertyName("signDate")]
    public virtual string? SignDate { get; set; } = default;
}


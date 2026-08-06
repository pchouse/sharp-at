/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Text.Json.Serialization;

namespace PChouse.Utils.PDFApiClient;

public class PdfToImageResponse
{
    [JsonPropertyName("status")]
    public virtual ResponseStatus? Status { get; set; } = default;

    [JsonPropertyName("message")]
    public virtual string Message { get; set; } = string.Empty;

    [JsonPropertyName("duration")]
    public virtual string Duration { get; set; } = string.Empty;

    [JsonPropertyName("images")]
    public virtual System.Collections.Generic.ICollection<string> Images { get; set; } = new List<string>();

    [JsonPropertyName("signature")]
    public virtual PdfSignature? Signature { get; set; } = default;
}


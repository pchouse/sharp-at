/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Text.Json.Serialization;

namespace PChouse.Utils.PDFApiClient.Multicert;

public class AdvancedSignatureInfo
{

    [JsonPropertyName("signatureLocation")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public virtual string? SignatureLocation { get; set; } = default;

    [JsonPropertyName("signatureTransform")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public virtual SignatureTransform? SignatureTransform { get; set; } = default;

    [JsonPropertyName("canonicalTransform")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public virtual CanonicalTransform? CanonicalTransform { get; set; } = default;

}


/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Text.Json.Serialization;

namespace PChouse.Utils.PDFApiClient;

public partial class PdfProperties
{
    [JsonPropertyName("userPassword")]
    public virtual string UserPassword { get; set; } = string.Empty;

    [JsonPropertyName("ownerPassword")]
    public virtual string OwnerPassword { get; set; } = string.Empty;

    [JsonPropertyName("javascript")]
    public virtual string Javascript { get; set; } = string.Empty;

    [JsonExtensionData]
    public virtual IDictionary<string, object> AdditionalProperties { get; set; } = new Dictionary<string, object>();

}


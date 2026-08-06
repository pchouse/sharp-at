/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Text.Json.Serialization;

namespace PChouse.Utils.PDFApiClient;

public partial class Metadata
{
    [JsonPropertyName("title")]
    public virtual string Title { get; set; } = string.Empty;

    [JsonPropertyName("author")]
    public virtual string Author { get; set; } = string.Empty;

    [JsonPropertyName("subject")]
    public virtual string Subject { get; set; } = string.Empty;

    [JsonPropertyName("keywords")]
    public virtual string Keywords { get; set; } = string.Empty;

    [JsonPropertyName("application")]
    public virtual string Application { get; set; } = string.Empty;

    [JsonPropertyName("creator")]
    public virtual string Creator { get; set; } = string.Empty;

    [JsonPropertyName("displayMetadataTitle")]
    public virtual bool DisplayMetadataTitle { get; set; } = false;

    [JsonPropertyName("javascript")]
    public virtual string Javascript { get; set; } = string.Empty;

    [JsonPropertyName("ownerPassword")]
    public virtual string OwnerPassword { get; set; } = string.Empty;

    [JsonPropertyName("userPassword")]
    public virtual string UserPassword { get; set; } = string.Empty;

    [JsonPropertyName("allowPrinting")]
    public virtual bool AllowPrinting { get; set; } = true;

    [JsonPropertyName("allowModifying")]
    public virtual bool AllowModifying { get; set; } = false;

    [JsonPropertyName("allowCopying")]
    public virtual bool AllowCopying { get; set; } = true;

    [JsonPropertyName("allowFillIn")]
    public virtual bool AllowFillIn { get; set; } = false;

    [JsonPropertyName("allowScreenReaders")]
    public virtual bool AllowScreenReaders { get; set; } = true;

    public virtual bool AllowAssembly { get; set; } = true;

    [JsonPropertyName("allowDegradedPrinting")]
    public virtual bool AllowDegradedPrinting { get; set; } = true;

    [JsonPropertyName("allowModifyAnnotation")]
    public virtual bool AllowModifyAnnotation { get; set; } = true;

    [JsonExtensionData]
    public virtual IDictionary<string, object> AdditionalProperties { get; set; } = new Dictionary<string, object>();

}


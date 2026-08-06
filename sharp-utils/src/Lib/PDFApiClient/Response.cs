/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Text.Json.Serialization;

namespace PChouse.Utils.PDFApiClient;

public partial class Response
{
    [JsonPropertyName("status")]
    public virtual ResponseStatus Status { get; set; }

    [JsonPropertyName("message")]
    public virtual string Message { get; set; } = string.Empty;

    [JsonPropertyName("duration")]
    public virtual string Duration { get; set; } = string.Empty;

    [JsonPropertyName("report")]
    public virtual string Report { get; set; } = string.Empty;

    [JsonExtensionData]
    public virtual IDictionary<string, object> AdditionalProperties { get; set; } = new Dictionary<string, object>();
    /// <summary>
    /// Returns a string representation of the Response object.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return $"Response: {{Status: {this.Status}, Message: {this.Message}, Duration: {this.Duration}, Report: {(this.Report == null ? "null" : "...")} }}";
    }

}


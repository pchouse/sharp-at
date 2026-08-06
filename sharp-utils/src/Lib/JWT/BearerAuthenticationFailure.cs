

using System.Text.Json.Serialization;


/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/
namespace PChouse.Utils.JWT;

public class BearerAuthenticationFailure
{
    /// <summary>
    /// Reason failure
    /// </summary>
    [JsonPropertyName(nameof(Reason))]
    public required string Reason { get; set; }
}
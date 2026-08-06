

using System.Text.Json.Serialization;


/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/
namespace PChouse.AT.QRCode.Library;

/// <summary>
/// Other Info
/// </summary>
/// <typeparam name="T"></typeparam>
public record OtherInfo : IOtherInfo
{
    /// <summary>
    /// Other info
    /// </summary>
    [JsonPropertyName(nameof(Information))]
    public virtual required string Information { get; set; }

    public virtual string? Info()
    {
        if (string.IsNullOrEmpty(Information)) return null;
        return Information;
    }
}

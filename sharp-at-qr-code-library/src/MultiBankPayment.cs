/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Globalization;
using System.Text.Json.Serialization;

namespace PChouse.AT.QRCode.Library;

public record MultiBankPayment : IOtherInfo
{

    /// <summary>
    /// Entity code
    /// </summary>
    [JsonPropertyName(nameof(Entity))]
    public virtual required string Entity { get; set; }

    /// <summary>
    /// Payment Reference
    /// </summary>
    [JsonPropertyName(nameof(Reference))]
    public virtual required string Reference { get; set; }

    /// <summary>
    /// Amount Reference
    /// </summary>
    [JsonPropertyName(nameof(Amount))]
    public virtual required decimal Amount { get; set; }

    /// <summary>
    /// Get the Other info text information for the QR code other info field
    /// </summary>
    /// <returns></returns>
    public virtual string? Info()
    {
        return $"MB;{Entity};{Reference};{Amount.ToString("F2", CultureInfo.InvariantCulture)}";        
    }
}
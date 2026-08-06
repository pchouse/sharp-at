/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Globalization;
using System.Text;
using System.Text.Json.Serialization;
using PChouse.AT.Generics;

namespace PChouse.AT.QRCode.Library;

/// <summary>
/// Iban information for QR code other info field 
/// </summary>
public record BankTransfer : IOtherInfo
{

    /// <summary>
    /// The Iban
    /// </summary>
    [JsonPropertyName(nameof(Iban))]
    public virtual required string Iban { get; set; }

    /// <summary>
    /// The transfer amount
    /// </summary>
    [JsonPropertyName(nameof(Amount))]
    public virtual required decimal? Amount { get; set; }

    /// <summary>
    /// The currency to transfer, null for EUR
    /// </summary>
    [JsonPropertyName(nameof(CurrencyCode))]
    public virtual required CurrencyCode? CurrencyCode { get; set; }

    /// <summary>
    /// Get the Other info text information
    /// </summary>
    /// <returns></returns>
    public virtual string? Info()
    {
        if (string.IsNullOrEmpty(Iban)){
            return null;
        }

        var stringBuilder = new StringBuilder(
            $"TB;{Iban}"
        );

        if (Amount is not null)
        {
            if (CurrencyCode is null)
            {
                stringBuilder.Append($";{Amount?.ToString("F2", CultureInfo.InvariantCulture)}");
            }
            else
            {
                stringBuilder.Append($";{Amount?.ToString(CultureInfo.InvariantCulture)}");
            }
        }

        if (CurrencyCode is not null)
        {
            stringBuilder.Append($";{CurrencyCode.ToString()}");
        }

        return stringBuilder.ToString();

    }
}

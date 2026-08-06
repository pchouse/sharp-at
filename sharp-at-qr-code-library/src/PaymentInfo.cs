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
public record PaymentInfo : IOtherInfo
{

    /// <summary>
    /// The payment mechanism
    /// </summary>
    [JsonPropertyName(nameof(PaymentMechanism))]
    public virtual required PaymentMechanism PaymentMechanism { get; set; }

    /// <summary>
    /// The payment cash amount
    /// </summary>
    [JsonPropertyName(nameof(Amount))]
    public virtual required decimal Amount { get; set; }

    /// <summary>
    /// The currency of the payment, null for EUR
    /// </summary>
    [JsonPropertyName(nameof(CurrencyCode))]
    public virtual required CurrencyCode? CurrencyCode { get; set; }

    /// <summary>
    /// Get the Other info text information
    /// </summary>
    /// <returns></returns>
    public virtual string? Info()
    {
        var stringBuilder = new StringBuilder(
            $"{PaymentMechanism}"
        );

        if (CurrencyCode is null)
        {
            stringBuilder.Append($";{Amount.ToString("F2", CultureInfo.InvariantCulture)}");
        }
        else
        {
            stringBuilder.Append($";{Amount.ToString(CultureInfo.InvariantCulture)}");
            stringBuilder.Append($";{CurrencyCode}");
        }

        return stringBuilder.ToString();
    }
}



using System.Text.Json.Serialization;


/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/
namespace PChouse.AT.QRCode.Library;

/// <summary>
/// The builded string for the qr code
/// </summary>
public record BuildStringAsyncResult
{
    /// <summary>
    /// The QR code builded string
    /// </summary>
    [JsonPropertyName(nameof(QrCodeString))]
    public virtual required string QrCodeString { get; set; }

    /// <summary>
    /// The build errors
    /// </summary>
    [JsonPropertyName(nameof(Errors))]
    public virtual required List<ErrorField> Errors { get; set; }

}

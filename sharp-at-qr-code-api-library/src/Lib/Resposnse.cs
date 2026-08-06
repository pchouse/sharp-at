/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Text.Json.Serialization;
using PChouse.AT.QRCode.Library;


namespace PChouse.AT.QRCode.API.Library;

public record Response
{
    /// <summary>
    /// The Errors
    /// </summary>
    [JsonPropertyName(nameof(Error))]
    public virtual List<Error>? Error { get; set; }

    /// <summary>
    /// The Qr code string or the Image as Base64 string
    /// </summary> 
    [JsonPropertyName(nameof(QrCode))]
    public virtual required string QrCode { get; set; }

}
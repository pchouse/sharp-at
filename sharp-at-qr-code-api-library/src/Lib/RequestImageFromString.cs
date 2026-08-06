/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Text.Json.Serialization;
using PChouse.AT.QRCode.Library;


namespace PChouse.AT.QRCode.API.Library;

public record RequestImageFromString
{

    [JsonPropertyName(nameof(QRCode))]
    public required string QRCode { get; set; }

    [JsonPropertyName(nameof(ImageOptions))]
    public required ImageOptions ImageOptions { get; set; }

}

/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Text.Json.Serialization;

namespace PChouse.AT.QRCode.API.Library;

public record RequestFromString
{

    [JsonPropertyName(nameof(QRCode))]
    public required string QRCode { get; set; }

}

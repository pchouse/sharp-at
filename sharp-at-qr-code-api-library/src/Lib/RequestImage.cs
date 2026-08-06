/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Text.Json.Serialization;
using PChouse.AT.QRCode.Library;


namespace PChouse.AT.QRCode.API.Library;

public record RequestImage
{

    [JsonPropertyName(nameof(Document))]
    public required Document Document { get; set; }

    [JsonPropertyName(nameof(BuilderOptions))]
    public required BuilderOptions BuilderOptions { get; set; }

    [JsonPropertyName(nameof(ImageOptions))]
    public required ImageOptions ImageOptions { get; set; }

}

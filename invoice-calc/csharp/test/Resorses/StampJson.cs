using System.Text.Json.Serialization;

namespace InvoiceCalcTest.Resources;

public class StampJson
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("tax")]
    public decimal Tax { get; set; } = 0m;  
}
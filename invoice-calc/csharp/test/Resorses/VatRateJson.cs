using System.Text.Json.Serialization;

namespace InvoiceCalcTest.Resources;

public class VatRateJson
{

    [JsonPropertyName("vatType")]
    public string VatType { get; set; } = string.Empty;

    [JsonPropertyName("vatZone")]
    public string VatZone { get; set; } = string.Empty;

    [JsonPropertyName("tax")]
    public decimal Tax { get; set; } = 0m;

    [JsonPropertyName("taxExemptionCode")]
    public string? TaxExemptionCode { get; set; }

}
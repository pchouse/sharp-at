using System.Text.Json.Serialization;

namespace InvoiceCalcTest.Resources;

public class VatJson
{
    [JsonPropertyName("VatRate")]
    public VatRateJson? VatRate { get; set; } = null;

    [JsonPropertyName("Stamp")]
    public StampJson? Stamp { get; set; } = null;
}
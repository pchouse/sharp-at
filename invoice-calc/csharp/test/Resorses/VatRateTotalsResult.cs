
using System.Text.Json.Serialization;
using PChouse.InvoiceCalc.Tax;

namespace InvoiceCalcTest.Resources;

public class VatRateTotalsResult
{
    [JsonPropertyName("type")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public VatType Type;

    [JsonPropertyName("zone")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public VatZone Zone;

    [JsonPropertyName("tax")]
    public decimal Tax;

    [JsonPropertyName("taxExemptionCode")]
    public string? TaxExemptionCode;

    [JsonPropertyName("baseTaxableAmount")]
    public decimal BaseTaxableAmount;

    [JsonPropertyName("taxValueAmount")]
    public decimal TaxValueAmount;

    [JsonPropertyName("baseTaxable")]
    public decimal BaseTaxable;

    [JsonPropertyName("taxValue")]
    public decimal TaxValue;

}
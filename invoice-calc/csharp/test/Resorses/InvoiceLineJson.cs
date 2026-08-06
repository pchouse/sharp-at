using System.Text.Json.Serialization;
using PChouse.InvoiceCalc;

namespace InvoiceCalcTest.Resources;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

public class InvoiceLineJson
{

    [JsonPropertyName("unitPrice")]
    public decimal UnitPrice { get; set; }

    [JsonPropertyName("unitPriceScale")]
    public int UnitPriceScale { get; set; }

    [JsonPropertyName("quantity")]
    public decimal Quantity { get; set; }

    [JsonPropertyName("vat")]
    public VatJson Vat { get; set; }

    [JsonPropertyName("discounts")]
    public List<DiscountJson> Discounts {get; set;} = [];

    [JsonPropertyName("totalAdjustment")]
    public decimal TotalAdjustment { get; set; }

    [JsonPropertyName("withholdingTaxRate")]
    public decimal? WithholdingTaxRate { get; set; }

}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

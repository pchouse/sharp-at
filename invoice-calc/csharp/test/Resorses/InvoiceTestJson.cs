using System.Text.Json.Serialization;
using PChouse.InvoiceCalc;

namespace InvoiceCalcTest.Resources;

public class InvoiceTestJson
{
    [JsonPropertyName("testDescribe")]
    public string TestDescribe { get; set; } = string.Empty;

    [JsonPropertyName("vatIncluded")]
    public bool VatIncluded { get; set; }

    [JsonPropertyName("totalScale")]
    public int TotalScale { get; set; }

    [JsonPropertyName("discount")]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public DiscountJson Discount { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    [JsonPropertyName("currencyRate")]
    public decimal CurrencyRate { get; set; }

    [JsonPropertyName("totalAdjustment")]
    public decimal TotalAdjustment { get; set; }

    [JsonPropertyName("lines")]
    public List<LineJson> Lines { get; set; } = [];

    [JsonPropertyName("invoiceTestResult")]
    public InvoiceTestResult? InvoiceTestResult { get; set; } = null;

}
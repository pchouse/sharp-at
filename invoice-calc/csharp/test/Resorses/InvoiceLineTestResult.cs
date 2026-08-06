using System.Text.Json.Serialization;

namespace InvoiceCalcTest.Resources;

public class InvoiceLineTestResult
{
    [JsonPropertyName("baseTotalAmount")]
    public decimal BaseTotalAmount { get; set; }

    [JsonPropertyName("totalAmount")]
    public decimal TotalAmount { get; set; }

    [JsonPropertyName("netPrice")]
    public decimal NetPrice { get; set; }

    [JsonPropertyName("settlementAmount")]
    public decimal SettlementAmount { get; set; }

    [JsonPropertyName("total")]
    public decimal Total { get; set; }

    [JsonPropertyName("withholdingTax")]
    public decimal WithholdingTax { get; set; }

    [JsonPropertyName("withholdingTaxAmount")]
    public decimal WithholdingTaxAmount { get; set; }
}
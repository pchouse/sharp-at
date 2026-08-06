using System.Text.Json.Serialization;

namespace InvoiceCalcTest.Resources;


public class InvoiceTestResult
{

    [JsonPropertyName("baseTotal")]
    public decimal BaseTotal { get; set; }

    [JsonPropertyName("totalAmount")]
    public decimal TotalAmount { get; set; }

    [JsonPropertyName("taxAmount")]
    public decimal TaxAmount { get; set; }

    [JsonPropertyName("taxableAmount")]
    public decimal TaxableAmount { get; set; }

    [JsonPropertyName("totalHeadDiscount")]
    public decimal TotalHeadDiscount { get; set; }

    [JsonPropertyName("totalLinesDiscount")]
    public decimal TotalLinesDiscount { get; set; }

    [JsonPropertyName("totalDiscount")]
    public decimal TotalDiscount { get; set; }

    [JsonPropertyName("grossTotal")]
    public decimal GrossTotal { get; set; }

    [JsonPropertyName("taxPayable")]
    public decimal TaxPayable { get; set; }

    [JsonPropertyName("netTotal")]
    public decimal NetTotal { get; set; }

    [JsonPropertyName("totalVat")]
    public decimal TotalVat { get; set; }

    [JsonPropertyName("totalStamp")]
    public decimal TotalStamp { get; set; }

    [JsonPropertyName("vatRateTotals")]
    public List<VatRateTotalsResult> VatRateTotals { get; set; } = [];

    [JsonPropertyName("withholdingTax")]
    public decimal WithholdingTax { get; set; }

    [JsonPropertyName("withholdingTaxAmount")]
    public decimal WithholdingTaxAmount { get; set; }

}
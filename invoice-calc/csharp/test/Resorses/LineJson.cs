using System.Text.Json.Serialization;
using PChouse.InvoiceCalc;

namespace InvoiceCalcTest.Resources;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

public class LineJson
{

    [JsonPropertyName("invoiceLineTest")]
    public InvoiceLineJson InvoiceLine { get; set; }

    [JsonPropertyName("invoiceLineTestResult")]

    public InvoiceLineTestResult InvoiceLineTestResult { get; set; }

}

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

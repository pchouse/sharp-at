using PChouse.InvoiceCalc;

namespace InvoiceCalcTest.Resources;

public class ParsedLine
{
    public required InvoiceLine InvoiceLine { get; set; }

    public required InvoiceLineTestResult InvoiceLineTestResult { get; set; }

}
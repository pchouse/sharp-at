using System.Text.Json.Serialization;
using Microsoft.Testing.Platform.Extensions.Messages;
using PChouse.InvoiceCalc;

namespace InvoiceCalcTest.Resources;

public class InvoiceTestParse
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public Invoice Invoice { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    public InvoiceTestResult? InvoiceTestResult { get; set; }

    public List<LineTest> Lines { get; set; } = [];
}
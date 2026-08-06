using System.Reflection;
using PChouse.InvoiceCalc;
using PChouse.InvoiceCalc.Tax;

namespace InvoiceCalcTest.Resources;

public class ABaseTest
{

    public List<InvoiceTestJson> Parser(string fileName)
    {

        try
        {
            string dllPath = Assembly.GetAssembly(this.GetType())!.Location;
            string projectDir = dllPath.Substring(0, dllPath.LastIndexOf("csharp"));
            var jsonString = File.ReadAllText(Path.Combine(projectDir, "test-resources", fileName));
            var json = System.Text.Json.JsonSerializer.Deserialize<List<InvoiceTestJson>>(
                jsonString,
                new System.Text.Json.JsonSerializerOptions
                {
                    IncludeFields = true,
                }
                )!;

            return json;

        }
        catch (Exception e)
        {
            Assert.Fail(e.Message);
            return [];
        }
    }


    public InvoiceTestParse InvoiceTestParse(InvoiceTestJson invoiceTestJson)
    {

        var headDiscount = new Discount(
            invoiceTestJson.Discount.Type, invoiceTestJson.Discount.DiscountVal
        );


        var invoice = new Invoice(
            invoiceTestJson.TotalScale,
            invoiceTestJson.VatIncluded,
            headDiscount,
            invoiceTestJson.CurrencyRate,
            invoiceTestJson.TotalAdjustment
        );

        var lineNumber = 0;

        var lines = new List<LineTest>();

        var invoiceTestParse = new InvoiceTestParse
        {
            Invoice = invoice,
            InvoiceTestResult = invoiceTestJson.InvoiceTestResult,
            Lines = lines
        };

        foreach (var line in invoiceTestJson.Lines)
        {

            var discounts = line.InvoiceLine.Discounts.Select(
                d => new Discount(d.Type, d.DiscountVal)
            ).ToList();

            IVat vat;

            if (line.InvoiceLine.Vat.VatRate != null)
            {
                vat = new VatRate(
                    Enum.Parse<VatType>(line.InvoiceLine.Vat.VatRate.VatType),
                    Enum.Parse<VatZone>(line.InvoiceLine.Vat.VatRate.VatZone),
                    line.InvoiceLine.Vat.VatRate.Tax,
                    line.InvoiceLine.Vat.VatRate.TaxExemptionCode
                );
            }
            else
            {
                vat = new Stamp(
                    Enum.Parse<CalcType>(line.InvoiceLine.Vat.Stamp!.Type),
                    line.InvoiceLine.Vat.Stamp.Tax
                );
            }

            var invoiceLine = new InvoiceLine(
                invoice,
                ++lineNumber,
                line.InvoiceLine.UnitPrice,
                line.InvoiceLine.UnitPriceScale,
                line.InvoiceLine.Quantity,
                vat,
                discounts,
                line.InvoiceLine.WithholdingTaxRate
            );

            invoice.Lines.Add(invoiceLine);

            lines.Add(
                new LineTest
                {
                    InvoiceLine = invoiceLine,
                    InvoiceLineTestResult = line.InvoiceLineTestResult
                }
            );
        }

        return invoiceTestParse;
    }

    public void PerformAssertions(InvoiceTestParse invoiceTestParse, string testDescribe)
    {
        var invoice = invoiceTestParse.Invoice;
        var invoiceTestResult = invoiceTestParse.InvoiceTestResult;

        invoice.Calculate();

        foreach (var line in invoiceTestParse.Lines)
        {
            var invoiceLine = line.InvoiceLine;
            var testResult = line.InvoiceLineTestResult;

            Assert.AreEqual(testResult.BaseTotalAmount, invoiceLine.BaseTotalAmount, testDescribe + " => Line [" + invoiceLine.LineNumber + "] base total amount");
            Assert.AreEqual(testResult.TotalAmount, invoiceLine.TotalAmount, testDescribe + "  => Line [" + invoiceLine.LineNumber + "] total amount");
            Assert.IsLessThanOrEqualTo(0.01m, Math.Abs(testResult.NetPrice - invoiceLine.NetPrice), testDescribe + "  => Line [" + invoiceLine.LineNumber + "] net price");
            Assert.IsLessThanOrEqualTo(0.01m, Math.Abs(testResult.SettlementAmount - invoiceLine.SettlementAmount), testDescribe + "  => Line [" + invoiceLine.LineNumber + "] settlement amount");
            Assert.AreEqual(testResult.Total, invoiceLine.Total, testDescribe + "  => Line [" + invoiceLine.LineNumber + "] total");
            Assert.AreEqual(testResult.WithholdingTax, invoiceLine.WithholdingTax, testDescribe + " => Line [" + invoiceLine.LineNumber + "] withholding tax");
            Assert.AreEqual(testResult.WithholdingTaxAmount, invoiceLine.WithholdingTaxAmount, testDescribe + " => Line [" + invoiceLine.LineNumber + "] withholding tax amount");
        }

        Assert.AreEqual(invoiceTestResult!.BaseTotal, invoice.BaseTotal, testDescribe + "  => Invoice base total");
        Assert.AreEqual(invoiceTestResult.TotalVat, invoice.TotalVat, testDescribe + " => Invoice total vat");
        Assert.AreEqual(invoiceTestResult.TotalAmount, invoice.TotalAmount, testDescribe + " => Invoice total amount");
        Assert.AreEqual(invoiceTestResult.TotalStamp, invoice.TotalStamp, testDescribe + " => Invoice total stamp");
        Assert.AreEqual(invoiceTestResult.TaxableAmount, invoice.TaxableAmount, testDescribe + " => Invoice taxable amount");
        Assert.AreEqual(invoiceTestResult.NetTotal, invoice.NetTotal, testDescribe + " => Invoice net total");
        Assert.AreEqual(invoiceTestResult.GrossTotal, invoice.GrossTotal, testDescribe + " => Invoice gross total");
        Assert.AreEqual(invoiceTestResult.TaxPayable, invoice.TaxPayable, testDescribe + " => Invoice tax payable");
        Assert.AreEqual(invoiceTestResult.TaxAmount, invoice.TaxAmount, testDescribe + " => Invoice tax amount");
        Assert.AreEqual(invoiceTestResult.WithholdingTax, invoice.WithholdingTax, testDescribe + " => Invoice withholding tax");
        Assert.AreEqual(invoiceTestResult.WithholdingTaxAmount, invoice.WithholdingTaxAmount, testDescribe + " => Invoice withholding tax amount");

        Assert.AreEqual(
            invoiceTestResult!.VatRateTotals?.Count ?? 0,
            invoice.VatRatesTotals()?.Count ?? 0,
            testDescribe + " => Invoice length of vat rate totals stack"
            );

        foreach (var expectedVatRateTotal in invoiceTestResult.VatRateTotals!)
        {

            var vatRateTotals = invoice.VatRatesTotals().FirstOrDefault(vrt =>
            {
                return vrt.Type == expectedVatRateTotal.Type &&
                vrt.Zone == expectedVatRateTotal.Zone &&
                vrt.TaxExemptionCode == expectedVatRateTotal.TaxExemptionCode;
            });

            if (vatRateTotals == null)
            {
                Assert.Fail("vatRateTotals is null");
            }

            var error = testDescribe + " => Tax rate total not found: " +
                        expectedVatRateTotal.Type + " - " + expectedVatRateTotal.Zone +
                        (expectedVatRateTotal.TaxExemptionCode == null ? "" : "-" + expectedVatRateTotal.TaxExemptionCode);


            var vatId = vatRateTotals.Type.ToString() + "-" + vatRateTotals.Zone.ToString() + "-" + vatRateTotals.Tax;

            Assert.AreEqual(expectedVatRateTotal.Type, vatRateTotals.Type, testDescribe + " => Vat rate total type - " + vatId);
            Assert.AreEqual(expectedVatRateTotal.Zone, vatRateTotals.Zone, testDescribe + " => Vat rate total zone - " + vatId);
            Assert.AreEqual(expectedVatRateTotal.Tax, vatRateTotals.Tax, testDescribe + " Vat rate total tax - vatId");
            Assert.AreEqual(expectedVatRateTotal.TaxExemptionCode, vatRateTotals.TaxExemptionCode, testDescribe + " => Vat rate total code - " + vatId);
            Assert.AreEqual(expectedVatRateTotal.BaseTaxableAmount, vatRateTotals.BaseTaxableAmount, testDescribe + " => Vat rate total base taxable - " + vatId);
            Assert.AreEqual(expectedVatRateTotal.TaxValueAmount, vatRateTotals.TaxValueAmount, testDescribe + " => Vat rate value tax amount - " + vatId);
            Assert.AreEqual(expectedVatRateTotal.BaseTaxable, vatRateTotals.BaseTaxable, testDescribe + " => Vat rate total base taxable - " + vatId);
            Assert.AreEqual(expectedVatRateTotal.TaxValue, vatRateTotals.TaxValue, testDescribe + " => Vat rate tax value - " + vatId);
        }

        Assert.AreEqual(invoiceTestParse.Lines?.Count ?? 0, invoice.Lines?.Count ?? 0, testDescribe + " => Invoice length of the lines stack");

        Assert.AreEqual(
            invoice.TaxAmount + invoice.TaxableAmount,
            invoice.TotalAmount - invoice.TotalAdjustment,
            testDescribe + " => Invoice total amount should be tax amount plus taxable amount"
        );

        Assert.AreEqual(
            invoiceTestResult.TotalHeadDiscount,
            invoice.DiscountHeadAmount,
            "invoiceTest.testDescribe => Invoice discount head amount"
        );

        Assert.AreEqual(
            invoiceTestResult.TotalLinesDiscount,
            invoice.DiscountLinesAmount,
            "invoiceTest.testDescribe => Invoice discount line amount"
        );

        Assert.AreEqual(
            invoiceTestResult.TotalDiscount,
            invoice.DiscountAmount,
            "invoiceTest.testDescribe => Invoice discount total"
        );

        Assert.AreEqual(
            invoiceTestResult.TotalDiscount,
            invoice.DiscountLinesAmount + invoice.DiscountHeadAmount,
            "invoiceTest.testDescribe => Invoice head discount plus lines discount should be equal to total"
        );

        if (invoice.IsVatIncluded)
        {
            Assert.AreEqual(
                invoice.BaseTotal - invoice.DiscountAmount,
                invoice.TotalAmount - invoice.TotalAdjustment,
                "invoiceTest.testDescribe => Invoice total amount should be base total subtracting discount amount"
            );
        }
        else
        {
            Assert.AreEqual(
                invoice.BaseTotal - invoice.DiscountAmount,
                invoice.TaxableAmount,
                "invoiceTest.testDescribe => Invoice taxable amount amount should be base total subtracting discount amount"
            );
        }

        var netValue = 0.0m;

        foreach (var l in invoice.Lines!)
        {
            netValue += l.NetPrice * l.Quantity;
        }

        Assert.IsLessThan(
                0.01m,
                invoice.NetTotal - netValue,
                "invoiceTest.testDescribe => The sum of product of net value per quantity of all value should be equals to invoice net total"
        );

        var netLineValue = 0.0m;

        foreach (var l in invoice.Lines)
        {
            netLineValue = netLineValue + l.Total;
        }

        Assert.AreEqual(
            netLineValue,
            invoice.NetTotal,
            "invoiceTest.testDescribe => The sum of all total lines (debit/credit) mut be equal to invoice net total"
        );
    }

}
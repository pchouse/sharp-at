using InvoiceCalcTest.Resources;

namespace InvoiceCalcTest;

[TestClass]
public sealed class InvoiceTest : ABaseTest
{
    [TestMethod]
    public void TestFileBasicInvoiceLineTestJson()
    {
        var parsed = this.Parser("basic-invoice-line-test.json");

        foreach (var invoiceTest in parsed ?? [])
        {
            PerformAssertions(
                this.InvoiceTestParse(invoiceTest), invoiceTest.TestDescribe
             );
        }
    }

    [TestMethod]
    public void TestFileInvoiceOneLineEachVatTypeTestJson()
    {
        var parsed = this.Parser("invoice-one-line-each-vat-type-test.json");

        foreach (var invoiceTest in parsed ?? [])
        {
            PerformAssertions(
                this.InvoiceTestParse(invoiceTest), invoiceTest.TestDescribe
             );
        }
    }

    [TestMethod]
    public void TestFileInvoiceLineDiscountsTestJson()
    {
        var parsed = this.Parser("invoice-line-discounts-test.json");

        foreach (var invoiceTest in parsed ?? [])
        {
            PerformAssertions(
                this.InvoiceTestParse(invoiceTest), invoiceTest.TestDescribe
             );
        }
    }

    [TestMethod]
    public void TestFileInvoiceHeadDiscountsTestJson()
    {
        var parsed = this.Parser("invoice-head-discounts-test.json");

        foreach (var invoiceTest in parsed ?? [])
        {
            PerformAssertions(
                this.InvoiceTestParse(invoiceTest), invoiceTest.TestDescribe
             );
        }
    }

    [TestMethod]
    public void TestFileInvoiceLineStampTaxTestJson()
    {
        var parsed = this.Parser("invoice-line-stamp-tax-test.json");

        foreach (var invoiceTest in parsed ?? [])
        {
            PerformAssertions(
                this.InvoiceTestParse(invoiceTest), invoiceTest.TestDescribe
             );
        }
    }

    [TestMethod]
    public void TestFileInvoiceMultipleTestJson()
    {
        var parsed = this.Parser("invoice-multiple-test.json");

        foreach (var invoiceTest in parsed ?? [])
        {
            PerformAssertions(
                this.InvoiceTestParse(invoiceTest), invoiceTest.TestDescribe
             );
        }
    }

    [TestMethod]
    public void TestFileInvoiceMultipleTestHeadDiscountJson()
    {
        var parsed = this.Parser("invoice-multiple-test-head-discount.json");

        foreach (var invoiceTest in parsed ?? [])
        {
            PerformAssertions(
                this.InvoiceTestParse(invoiceTest), invoiceTest.TestDescribe
             );
        }
    }

    [TestMethod]
    public void TestFileInvoiceMultipleTestVatIncludedJson()
    {
        var parsed = this.Parser("invoice-multiple-test-vat-included.json");

        foreach (var invoiceTest in parsed ?? [])
        {
            PerformAssertions(
                this.InvoiceTestParse(invoiceTest), invoiceTest.TestDescribe
             );
        }
    }

    [TestMethod]
    public void TestFileInvoiceMultipleTestVatIncludedHeadDiscountJson()
    {
        var parsed = this.Parser("invoice-multiple-test-vat-included-head-discount.json");

        foreach (var invoiceTest in parsed ?? [])
        {
            PerformAssertions(
                this.InvoiceTestParse(invoiceTest), invoiceTest.TestDescribe
             );
        }
    }

    [TestMethod]
    public void TestFileFileBasicInvoiceLineTestCurrencyJson()
    {
        var parsed = this.Parser("basic-invoice-line-test-currency.json");

        foreach (var invoiceTest in parsed ?? [])
        {
            PerformAssertions(
                this.InvoiceTestParse(invoiceTest), invoiceTest.TestDescribe
             );
        }
    }

    [TestMethod]
    public void TestFileFileBasicInvoiceWithholdingTestJson()
    {
        var parsed = this.Parser("basic-invoice-withholding-test.json");

        foreach (var invoiceTest in parsed ?? [])
        {
            PerformAssertions(
                this.InvoiceTestParse(invoiceTest), invoiceTest.TestDescribe
             );
        }
    }

    [TestMethod]
    public void TestFileFileInvoiceLineAdjustmentTestJson()
    {
        var parsed = this.Parser("invoice-line-adjustment-test.json");

        foreach (var invoiceTest in parsed ?? [])
        {
            PerformAssertions(
                this.InvoiceTestParse(invoiceTest), invoiceTest.TestDescribe
             );
        }
    }

}

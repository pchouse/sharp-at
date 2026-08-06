import { Discount, DiscountType, Invoice, InvoiceLine, Stamp, VatRate, VatType, VatZone } from '../../src';
import Decimal                                                                            from 'decimal.js';
import { TInvoiceTest }                                                                   from './TInvoiceTest';
import { TInvoiceTestParse }                                                              from './TInvoiceTestParse';

export function invoiceTestParse(invoiceTest: TInvoiceTest): TInvoiceTestParse {

  // @ts-ignore
  const parsed: TInvoiceTestParse = {};
  parsed.lines = [];

  const invoice = new Invoice(
    BigInt(invoiceTest.totalScale),
    invoiceTest.vatIncluded,
    new Discount(
      DiscountType[invoiceTest.discount.type], new Decimal(invoiceTest.discount.discount)
    ),
    new Decimal(invoiceTest.currencyRate),
    new Decimal(invoiceTest.totalAdjustment)
  );

  parsed.invoice = invoice;
  parsed.invoiceTestResult = invoiceTest.invoiceTestResult;

  let lineNumber = 0;
  invoiceTest.lines.forEach(l => {

    let vat: VatRate | Stamp;
    const discounts: Discount[] = [];

    if ((typeof l.invoiceLineTest.vat.VatRate) !== 'undefined') {

      vat = new VatRate(
        // @ts-ignore
        VatType[l.invoiceLineTest.vat.VatRate!.vatType],
        // @ts-ignore
        VatZone[l.invoiceLineTest.vat.VatRate!.vatZone],
        new Decimal(l.invoiceLineTest.vat.VatRate!.tax),
        l.invoiceLineTest.vat.VatRate!.taxExemptionCode
      );

    } else {
      vat = new Stamp(l.invoiceLineTest.vat.Stamp!.type, new Decimal(l.invoiceLineTest.vat.Stamp!.tax));
    }

    l.invoiceLineTest.discounts.forEach(discount => {
      discounts.push(
        new Discount(DiscountType[discount.type], new Decimal(discount.discount))
      );
    });

    const withholdingTaxRate = l.invoiceLineTest.withholdingTaxRate === null
                               ? null : new Decimal(l.invoiceLineTest.withholdingTaxRate);

    const line = new InvoiceLine(
      invoice,
      BigInt(++lineNumber),
      new Decimal(l.invoiceLineTest.unitPrice),
      BigInt(l.invoiceLineTest.unitPriceScale),
      new Decimal(l.invoiceLineTest.quantity),
      vat,
      discounts,
      withholdingTaxRate
    );

    invoice.lines.push(line);

    parsed.lines.push({
      invoiceLine          : line,
      invoiceLineTestResult: l.invoiceLineTestResult
    });

  });

  return parsed;
}

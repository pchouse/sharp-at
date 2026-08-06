import { TInvoiceTest }                    from './resourses/TInvoiceTest';
import { invoiceTestParse }                from './resourses/InvoiceTestParse';
import { VatRateTotals, VatType, VatZone } from '../src';
import 'jest-expect-message';
import Decimal                                      from 'decimal.js';
import 'jest';

export function invoiceTestPerformAssertions(invoiceTestJson: TInvoiceTest): void {

  const invoiceTest = invoiceTestParse(invoiceTestJson);

  const invoice = invoiceTest.invoice;
  const invoiceTestResult = invoiceTest.invoiceTestResult!;

  invoice.calculate();

  invoiceTest.lines.forEach(line => {

    const invoiceLine = line.invoiceLine;
    const testResult = line.invoiceLineTestResult;

    expect(invoiceLine.baseTotalAmount.toNumber(), `Line [${invoiceLine.lineNumber}] base total amount`).toEqual(testResult.baseTotalAmount);
    expect(invoiceLine.totalAmount.toNumber(), `Line [${invoiceLine.lineNumber}] total amount`).toEqual(testResult.totalAmount);
    expect(invoiceLine.netPrice.toNumber(), `Line [${invoiceLine.lineNumber}] net price`).toBeCloseTo(testResult.netPrice, 2);
    expect(invoiceLine.settlementAmount.toNumber(), `Line [${invoiceLine.lineNumber}] settlement amount`).toBeCloseTo(testResult.settlementAmount, 2);
    expect(invoiceLine.total.toNumber(), `Line [${invoiceLine.lineNumber}] total`).toEqual(testResult.total);
    expect(invoiceLine.withholdingTax?.toNumber() ?? null, `Line [${invoiceLine.lineNumber}] withholding tax`).toEqual(testResult.withholdingTax);
    expect(invoiceLine.withholdingTaxAmount?.toNumber() ?? null, `Line [${invoiceLine.lineNumber}] withholding tax amount`).toEqual(testResult.withholdingTaxAmount);
  });

  expect(invoice.baseTotal.toNumber(), 'Invoice base total').toEqual(invoiceTestResult.baseTotal);
  expect(invoice.totalAmount.toNumber(), 'Invoice total amount').toEqual(invoiceTestResult.totalAmount);
  expect(invoice.totalVat.toNumber(), 'Invoice total vat').toEqual(invoiceTestResult.totalVat);
  expect(invoice.totalStamp.toNumber(), 'Invoice total stamp').toEqual(invoiceTestResult.totalStamp);
  expect(invoice.taxableAmount.toNumber(), 'Invoice taxable amount').toEqual(invoiceTestResult.taxableAmount);
  expect(invoice.netTotal.toNumber(), 'Invoice net total').toEqual(invoiceTestResult.netTotal);
  expect(invoice.grossTotal.toNumber(), 'Invoice gross total').toEqual(invoiceTestResult.grossTotal);
  expect(invoice.taxPayable.toNumber(), 'Invoice tax payable').toEqual(invoiceTestResult.taxPayable);
  expect(invoice.taxAmount.toNumber(), 'Invoice tax amount').toEqual(invoiceTestResult.taxAmount);
  expect(invoice.withholdingTax?.toNumber() ?? null, 'Invoice withholding tax').toEqual(invoiceTestResult.withholdingTax);
  expect(invoice.withholdingTaxAmount?.toNumber() ?? null, 'Invoice withholding tax amount').toEqual(invoiceTestResult.withholdingTaxAmount);

  expect(invoice.vatRatesTotals.length, 'Invoice length of vat rate totals stack').toEqual(invoiceTestResult.vatRateTotals.length);

  invoiceTestResult.vatRateTotals.forEach(expectedVatRateTotal => {

    const vatRateTotals = invoice.vatRatesTotals.find(vrt => {
      // @ts-ignore
      return vrt.type === VatType[expectedVatRateTotal.type]
        // @ts-ignore
        && vrt.zone === VatZone[expectedVatRateTotal.zone]
        && vrt.taxExemptionCode === expectedVatRateTotal.taxExemptionCode;
    });

    const error = `Tax rate total not found: ${expectedVatRateTotal.type} - ${expectedVatRateTotal.zone}${expectedVatRateTotal.taxExemptionCode === null ? '' : ` - ${expectedVatRateTotal.taxExemptionCode}`}`;

    expect(vatRateTotals, error).toBeInstanceOf(VatRateTotals);

    const vatId = `${vatRateTotals?.type}-${vatRateTotals?.zone}-${vatRateTotals?.tax}`;

    // @ts-ignore
    expect(vatRateTotals!.type, `Vat rate total type - ${vatId}`).toEqual(VatType[expectedVatRateTotal.type]);
    // @ts-ignore
    expect(vatRateTotals!.zone, `Vat rate total zone - ${vatId}`).toEqual(VatZone[expectedVatRateTotal.zone]);
    expect(vatRateTotals!.tax.toNumber(), `Vat rate total tax - ${vatId}`).toEqual(expectedVatRateTotal.tax);
    expect(vatRateTotals!.taxExemptionCode, `Vat rate total code - ${vatId}`).toEqual(expectedVatRateTotal.taxExemptionCode);
    expect(vatRateTotals!.baseTaxableAmount.toNumber(), `Vat rate total base taxable - ${vatId}`).toEqual(expectedVatRateTotal.baseTaxableAmount);
    expect(vatRateTotals!.taxValueAmount.toNumber(), `Vat rate value tax amount - ${vatId}`).toEqual(expectedVatRateTotal.taxValueAmount);
    expect(vatRateTotals!.baseTaxable.toNumber(), `Vat rate total base taxable - ${vatId}`).toEqual(expectedVatRateTotal.baseTaxable);
    expect(vatRateTotals!.taxValue.toNumber(), `Vat rate tax value - ${vatId}`).toEqual(expectedVatRateTotal.taxValue);
  });

  expect(invoice.lines.length, 'Invoice length of the lines stack').toEqual(invoiceTest.lines.length);

  // Other assertations
  expect(invoice.totalAmount.sub(invoice.totalAdjustment).toNumber(), 'Invoice total amount should be tax amount plus taxable amount').toEqual(
    invoice.taxAmount.add(invoice.taxableAmount).toNumber()
  );

  expect(invoice.discountHeadAmount.toNumber(), 'Invoice discount head amount').toEqual(invoiceTestResult.totalHeadDiscount);
  expect(invoice.discountLinesAmount.toNumber(), 'Invoice discount line amount').toEqual(invoiceTestResult.totalLinesDiscount);
  expect(invoice.discountAmount.toNumber(), 'Invoice discount total').toEqual(invoiceTestResult.totalDiscount);

  expect(
    invoice.discountLinesAmount.add(invoice.discountHeadAmount).toNumber(),
    'Invoice head discount plus lines discount should be equal to total')
    .toEqual(invoiceTestResult.totalDiscount);

  invoice.isVatIncluded
  ? expect(invoice.totalAmount.sub(invoice.totalAdjustment).toNumber(), 'Invoice total amount should be base total subtracting discount amount (vat inc)')
    .toEqual(invoice.baseTotal.sub(invoice.discountAmount).toNumber())
  : expect(invoice.taxableAmount.toNumber(), 'Invoice taxable amount amount should be base total subtracting discount amount (vat not inc)')
    .toEqual(invoice.baseTotal.sub(invoice.discountAmount).toNumber());

  let netValue = new Decimal(0.0);
  invoice.lines.forEach(l => netValue = netValue.add(l.netPrice.mul(l.quantity)));

  expect(invoice.netTotal.sub(netValue).toNumber(), 'The sum of product of net value per quantity of all value should be equals to invoice net total')
    .toBeLessThan(0.01);

  let netLineValue = new Decimal(0.0);
  invoice.lines.forEach(l => netLineValue = netLineValue.add(l.total));

  expect(invoice.netTotal.toNumber(), 'The sum of all total lines (debit/credit) mut be equal to invoice net total')
    .toEqual(netLineValue.toNumber());

}

import Decimal                                                                     from 'decimal.js';
import { Discount, DiscountType, Invoice, InvoiceLine, VatRate, VatType, VatZone } from '../src';
import basicInvoiceLineTest
                            from '../../test-resources/basic-invoice-line-test.json';
import { TInvoiceTest }     from './resourses/TInvoiceTest';
import { invoiceTestParse } from './resourses/InvoiceTestParse';
import 'jest-expect-message';

test('Instance', () => {

  const invoice = new Invoice(
    2n,
    false,
    new Discount(DiscountType.PERCENT, new Decimal(0.0)),
    new Decimal(1),
    new Decimal(0.0)
  );

  const invoiceLine = new InvoiceLine(
    invoice,
    1n,
    new Decimal(0.0),
    2n,
    new Decimal(1),
    new VatRate(VatType.NOR, VatZone.PT, new Decimal(23.00), null),
    [],
    null
  );

  expect(invoiceLine).toBeInstanceOf(InvoiceLine);

});

describe('Test of file basic-invoice-line-test.json', () => {

  const basicInvoiceLineData = basicInvoiceLineTest as unknown as TInvoiceTest[];

  basicInvoiceLineData.forEach(invoiceTestJson => {

    it(invoiceTestJson.testDescribe, () => {

      const invoiceTest = invoiceTestParse(invoiceTestJson);

      invoiceTest.lines.forEach(line => {

        const invoiceLine = line.invoiceLine;
        const testResult = line.invoiceLineTestResult;

        invoiceLine.baseCalculation();

        expect(invoiceLine.baseTotalAmount.toNumber(), `Line [${line.invoiceLine.lineNumber}] base total amount`).toEqual(testResult.baseTotalAmount);
        expect(invoiceLine.totalAmount.toNumber(), `Line [${line.invoiceLine.lineNumber}] total amount`).toEqual(testResult.totalAmount);
        expect(invoiceLine.total.toNumber(), `Line [${line.invoiceLine.lineNumber}] total amount`).toEqual(testResult.total);
        expect(invoiceLine.settlementAmount.toNumber(), `Line [${line.invoiceLine.lineNumber}] settlement amount`).toEqual(testResult.settlementAmount);

      });

    });
  });

});

describe('Test calculate unit price for line', () => {

  it('When invoice and price has both the same value of vat included the price returned should be returned without alterations', () => {

    [true, false].forEach(vatIncluded => {
      (['PERCENT', 'VALUE']).forEach(vatType => {

        const price = new Decimal(9.99);

        const calcPrice = InvoiceLine.calcUnitPriceForLine(
          vatIncluded,
          {
            amount        : price,
            hasVatIncluded: vatIncluded,
            decimalsScale : 2n,
            vatTaxRate    : new Decimal(19.9),
            vatTaxRateType: vatType as 'PERCENT' | 'VALUE'
          }
        );

        expect(calcPrice.toNumber()).toEqual(price.toNumber());
      });
    });

  });

  it('When vat rate is zero the price returned should be returned without alterations', () => {

    [true, false].forEach(vatIncluded => {
      (['PERCENT', 'VALUE']).forEach(vatType => {

        const price = new Decimal(9.99);

        let calcPrice = InvoiceLine.calcUnitPriceForLine(
          vatIncluded,
          {
            amount        : price,
            hasVatIncluded: vatIncluded,
            decimalsScale : 2n,
            vatTaxRate    : new Decimal(0.0),
            vatTaxRateType: vatType as 'PERCENT' | 'VALUE'
          }
        );

        expect(calcPrice.toNumber()).toEqual(price.toNumber());

        calcPrice = InvoiceLine.calcUnitPriceForLine(
          !vatIncluded,
          {
            amount        : price,
            hasVatIncluded: vatIncluded,
            decimalsScale : 2n,
            vatTaxRate    : new Decimal(0.0),
            vatTaxRateType: vatType as 'PERCENT' | 'VALUE'
          }
        );

        expect(calcPrice.toNumber()).toEqual(price.toNumber());

      });
    });

  });

  it('Invoice vat include price without vat included', () => {


    const price = new Decimal(100.00);

    let calcPrice = InvoiceLine.calcUnitPriceForLine(
      true,
      {
        amount        : price,
        hasVatIncluded: false,
        decimalsScale : 2n,
        vatTaxRate    : new Decimal(23.0),
        vatTaxRateType: 'PERCENT'
      }
    );

    expect(calcPrice.toNumber()).toEqual(123.00);

    calcPrice = InvoiceLine.calcUnitPriceForLine(
      true,
      {
        amount        : price,
        hasVatIncluded: false,
        decimalsScale : 2n,
        vatTaxRate    : new Decimal(9.99),
        vatTaxRateType: 'VALUE'
      }
    );

    expect(calcPrice.toNumber()).toEqual(109.99);

  });

  it('Invoice vat not include price with vat included', () => {

    let calcPrice = InvoiceLine.calcUnitPriceForLine(
      false,
      {
        amount        : new Decimal(123.00),
        hasVatIncluded: true,
        decimalsScale : 2n,
        vatTaxRate    : new Decimal(23.0),
        vatTaxRateType: 'PERCENT'
      }
    );

    expect(calcPrice.toNumber()).toEqual(100.00);

    calcPrice = InvoiceLine.calcUnitPriceForLine(
      false,
      {
        amount        : new Decimal(109.99),
        hasVatIncluded: true,
        decimalsScale : 2n,
        vatTaxRate    : new Decimal(9.99),
        vatTaxRateType: 'VALUE'
      }
    );

    expect(calcPrice.toNumber()).toEqual(100.00);

  });

});


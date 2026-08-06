import basicInvoiceLineTest               from '../../test-resources/basic-invoice-line-test.json';
import invoiceLineAdjustmentTest          from '../../test-resources/invoice-line-adjustment-test.json';
import basicInvoiceWithholdingTest        from '../../test-resources/basic-invoice-withholding-test.json';
import basicInvoiceLineTestCurrency       from '../../test-resources/basic-invoice-line-test-currency.json';
import invoiceOneLineEachVatTypeTest      from '../../test-resources/invoice-one-line-each-vat-type-test.json';
import invoiceLineDiscountsTest           from '../../test-resources/invoice-line-discounts-test.json';
import invoiceHeadDiscountsTest           from '../../test-resources/invoice-head-discounts-test.json';
import invoiceLineStampTaxTest            from '../../test-resources/invoice-line-stamp-tax-test.json';
import invoiceMultipleTest                from '../../test-resources/invoice-multiple-test.json';
import invoiceMultipleTestHeadDiscount    from '../../test-resources/invoice-multiple-test-head-discount.json';
import invoiceMultipleTestVatIncluded     from '../../test-resources/invoice-multiple-test-vat-included.json';
import invoiceMultipleTestVatIncludedHeadDiscount
                                          from '../../test-resources/invoice-multiple-test-vat-included-head-discount.json';
import { TInvoiceTest }                 from './resourses/TInvoiceTest';
import { invoiceTestPerformAssertions } from './PerformAssetations';
import 'jest-expect-message';

describe('Test of file basic-invoice-line-test.json', () => {

  const basicInvoiceLineData = basicInvoiceLineTest as unknown as TInvoiceTest[];

  basicInvoiceLineData.forEach(invoiceTestJson => {
    it(invoiceTestJson.testDescribe, () => invoiceTestPerformAssertions(invoiceTestJson));
  });
});

describe('Test of file invoice-one-line-each-vat-type-test.json', () => {

  const oneVatEachLineData = invoiceOneLineEachVatTypeTest as unknown as TInvoiceTest[];

  oneVatEachLineData.forEach(invoiceTestJson => {
    it(invoiceTestJson.testDescribe, () => invoiceTestPerformAssertions(invoiceTestJson));
  });
});

describe('Test of file invoice-line-discounts-test.json', () => {

  const invoiceLineDiscountData = invoiceLineDiscountsTest as unknown as TInvoiceTest[];

  invoiceLineDiscountData.forEach(invoiceTestJson => {
    it(invoiceTestJson.testDescribe, () => invoiceTestPerformAssertions(invoiceTestJson));
  });
});

describe('Test of file invoice-head-discounts-test.json', () => {

  const invoiceHeadDiscountData = invoiceHeadDiscountsTest as unknown as TInvoiceTest[];

  invoiceHeadDiscountData.forEach(invoiceTestJson => {
    it(invoiceTestJson.testDescribe, () => invoiceTestPerformAssertions(invoiceTestJson));
  });
});

describe('Test of file invoice-line-stamp-tax-test.json', () => {

  const invoiceLineStampTaxData = invoiceLineStampTaxTest as unknown as TInvoiceTest[];

  invoiceLineStampTaxData.forEach(invoiceTestJson => {
    it(invoiceTestJson.testDescribe, () => invoiceTestPerformAssertions(invoiceTestJson));
  });
});

describe('Test of file invoice-multiple-test.json', () => {

  const invoiceMultipleTestData = invoiceMultipleTest as unknown as TInvoiceTest[];

  invoiceMultipleTestData.forEach(invoiceTestJson => {
    it(invoiceTestJson.testDescribe, () => invoiceTestPerformAssertions(invoiceTestJson));
  });
});

describe('Test of file invoice-multiple-test-head-discount.json', () => {

  const invoiceMultipleTestHeadDiscountData = invoiceMultipleTestHeadDiscount as unknown as TInvoiceTest[];

  invoiceMultipleTestHeadDiscountData.forEach(invoiceTestJson => {

    it(invoiceTestJson.testDescribe, () => invoiceTestPerformAssertions(invoiceTestJson));

  });
});

describe('Test of file invoice-multiple-test-vat-included.json', () => {

  const invoiceMultipleTestVatIncludedData = invoiceMultipleTestVatIncluded as unknown as TInvoiceTest[];

  invoiceMultipleTestVatIncludedData.forEach(invoiceTestJson => {

    it(invoiceTestJson.testDescribe, () => invoiceTestPerformAssertions(invoiceTestJson));

  });
});

describe('Test of file invoice-multiple-test-vat-included-head-discount.json', () => {

  const invoiceMultipleTestVatIncludedHeadDiscountData = invoiceMultipleTestVatIncludedHeadDiscount as unknown as TInvoiceTest[];

  invoiceMultipleTestVatIncludedHeadDiscountData.forEach(invoiceTestJson => {

    it(invoiceTestJson.testDescribe, () => invoiceTestPerformAssertions(invoiceTestJson));

  });
});

describe('Test of file basic-invoice-line-test-currency.json', () => {

  const basicInvoiceLineTestCurrencyData = basicInvoiceLineTestCurrency as unknown as TInvoiceTest[];

  basicInvoiceLineTestCurrencyData.forEach(invoiceTestJson => {
    it(invoiceTestJson.testDescribe, () => invoiceTestPerformAssertions(invoiceTestJson));
  });
});

describe('Test of file basic-invoice-withholding-test.json', () => {

  const basicInvoiceWithholdingTestData = basicInvoiceWithholdingTest as unknown as TInvoiceTest[];

  basicInvoiceWithholdingTestData.forEach(invoiceTestJson => {
    it(invoiceTestJson.testDescribe, () => invoiceTestPerformAssertions(invoiceTestJson));
  });
});

describe('Test of file invoice-line-adjustment-test.json', () => {

  const invoiceLineAdjustmentTestData = invoiceLineAdjustmentTest as unknown as TInvoiceTest[];

  invoiceLineAdjustmentTestData.forEach(invoiceTestJson => {
    it(invoiceTestJson.testDescribe, () => invoiceTestPerformAssertions(invoiceTestJson));
  });
});

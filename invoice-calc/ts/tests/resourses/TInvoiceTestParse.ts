import { Invoice, InvoiceLine } from '../../src';
import { TInvoiceTestResult }   from './TInvoiceTestResult';
import { TInvoiceLineTestResult } from './TInvoiceLineTestResult';

export type TInvoiceTestParse = {

  invoice: Invoice,
  invoiceTestResult: TInvoiceTestResult|null,
  lines: {
    invoiceLine: InvoiceLine,
    invoiceLineTestResult: TInvoiceLineTestResult,
  }[]
};

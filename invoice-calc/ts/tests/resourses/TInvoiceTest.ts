import { TInvoiceLineTest }       from './TInvoiceLineTest';
import { TInvoiceLineTestResult } from './TInvoiceLineTestResult';
import { TInvoiceTestResult }     from './TInvoiceTestResult';

export type TInvoiceTest =
  {
    testDescribe: string,
    vatIncluded: boolean,
    totalScale: number,
    discount: {
      type: 'VALUE' | 'PERCENT',
      discount: number
    },
    currencyRate: number,
    totalAdjustment: number,
    lines: {
      invoiceLineTest: TInvoiceLineTest,
      invoiceLineTestResult: TInvoiceLineTestResult
    }[],
    invoiceTestResult: TInvoiceTestResult | null
  };

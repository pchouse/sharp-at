import Decimal from 'decimal.js';

export type TInvoiceLineTest = {
  unitPrice: number,
  unitPriceScale: number,
  quantity: number,
  vat: {
    VatRate: {
      vatType: string,
      vatZone: string,
      tax: number,
      taxExemptionCode: string | null
    } | null,
    Stamp: {
      type: 'FIXED' | 'PERCENT',
      tax: number
    } | null,
  },
  discounts: {
    type: 'VALUE' | 'PERCENT',
    discount: number
  }[],
  totalAdjustment: number,
  withholdingTaxRate: Decimal | null
};

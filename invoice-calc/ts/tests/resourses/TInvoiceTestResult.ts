export type TInvoiceTestResult = {
  baseTotal: number,
  totalAmount: number,
  taxAmount: number,
  taxableAmount: number,
  totalHeadDiscount: number,
  totalLinesDiscount: number,
  totalDiscount: number,
  grossTotal: number,
  taxPayable: number,
  netTotal: number,
  totalVat: number,
  totalStamp: number,
  vatRateTotals: {
    type: string,
    zone: string,
    tax: number,
    taxExemptionCode: string | null,
    baseTaxableAmount: number,
    taxValueAmount: number,
    baseTaxable: number,
    taxValue: number
  }[],
  withholdingTax: number;
  withholdingTaxAmount: number;
};

import { InvoiceLine }   from './InvoiceLine';
import Decimal           from 'decimal.js';
import { Discount }      from './Discount';
import { VatRateTotals } from './Tax/VatRateTotals';
import { DiscountType }  from './DiscountType';
import { VatRate }       from './Tax/VatRate';
import { Stamp }         from './Tax/Stamp';
import { InvoiceError }  from './Exceptions/InvoiceError';

export class Invoice {

  /**
   * The invoice base total.
   * Calculated before any discount and if invoices has not the vat included before apply vat else with vat
   * @private
   */
  private _baseTotal: Decimal = new Decimal(0.0);

  /**
   * The invoice base total.
   * Calculated before any discount and if invoices has not the var included before apply vat else with vat
   */
  public get baseTotal(): Decimal {
    return this._baseTotal.toDecimalPlaces(Number(this.totalScale), Decimal.ROUND_HALF_UP);
  }

  /**
   * The invoice total in the invoice currency
   * @private
   */
  private _totalAmount: Decimal = new Decimal(0.0);

  /**
   * The invoice total in the invoice currency
   */
  public get totalAmount(): Decimal {
    return this._totalAmount.toDecimalPlaces(Number(this.totalScale), Decimal.ROUND_HALF_UP);
  }

  /**
   * The invoice tax amount in the invoice currency (Vat + Stamp)
   * @private
   */
  private _taxAmount: Decimal = new Decimal(0.0);

  /**
   * The invoice tax amount in the invoice currency (Vat + Stamp)
   * @private
   */
  public get taxAmount(): Decimal {
    return this._taxAmount.toDecimalPlaces(Number(this.totalScale), Decimal.ROUND_HALF_UP);
  }

  /**
   * The taxable amount in the invoice currency
   * @private
   */
  private _taxableAmount: Decimal = new Decimal(0.0);

  /**
   * The taxable amount in the invoice currency
   */
  public get taxableAmount(): Decimal {
    return this._taxableAmount.toDecimalPlaces(Number(this.totalScale), Decimal.ROUND_HALF_UP);
  }

  /**
   * The invoice gross total.
   * (Correspond to SAFT-PT DocumentTotal/GrossTotal
   * @private
   */
  private _grossTotal: Decimal = new Decimal(0.0);

  /**
   * The invoice gross total.
   * (Correspond to SAFT-PT DocumentTotal/GrossTotal
   */
  public get grossTotal() {
    return this._grossTotal.toDecimalPlaces(2, Decimal.ROUND_HALF_UP);
  }

  /**
   * The invoice tax payable.
   * (Correspond to SAFT-PT DocumentTotal/TaxPayable
   * @private
   */
  private _taxPayable: Decimal = new Decimal(0.0);

  /**
   * The invoice tax payable.
   * (Correspond to SAFT-PT DocumentTotal/TaxPayable
   */
  public get taxPayable() {
    return this._taxPayable.toDecimalPlaces(2, Decimal.ROUND_HALF_UP);
  }

  /**
   * The invoice net total.
   * (Correspond to SAFT-PT DocumentTotal/NetTotal
   * @private
   */
  private _netTotal: Decimal = new Decimal(0.0);

  /**
   * The invoice net total.
   * (Correspond to SAFT-PT DocumentTotal/NetTotal
   */
  public get netTotal() {
    return this._netTotal.toDecimalPlaces(2, Decimal.ROUND_HALF_UP);
  }

  /**
   * Vat rate totals
   * @private
   */
  private _vatRatesTotals: VatRateTotals[] = [];

  /**
   * Vat rate totals
   */
  public get vatRatesTotals(): VatRateTotals[] {
    return this._vatRatesTotals;
  }

  /**
   * Total of stamp tax
   * @private
   */
  private _totalStamp: Decimal = new Decimal(0.0);

  /**
   * Total of stamp tax
   */
  public get totalStamp() {
    return this._totalStamp;
  }

  /**
   * Vat total
   * @private
   */
  private _totalVat = new Decimal(0.0);

  /**
   * Vat total
   */
  public get totalVat() {
    return this._totalVat;
  }

  /**
   * Invoice lines stack
   * @private
   */
  private _lines: InvoiceLine[] = [];

  /**
   * Invoice lines stack
   */
  public get lines(): InvoiceLine[] {
    return this._lines;
  }

  /**
   * The total amount of discounts.
   * If the invoice has the vat included this value has also the var included else not.
   */
  public get discountAmount() {
    return this._discountLinesAmount
      .add(this._discountHeadAmount)
      .toDecimalPlaces(Number(this.totalScale), Decimal.ROUND_HALF_UP);
  }

  /**
   * The lines discount amount.
   * If the invoice has the vat included this value has also the var included else not.
   * @private
   */
  private _discountLinesAmount: Decimal = new Decimal(0.0);

  /**
   * The lines discount amount.
   * If the invoice has the vat included this value has also the var included else not.
   */
  public get discountLinesAmount() {
    return this._discountLinesAmount.toDecimalPlaces(Number(this.totalScale), Decimal.ROUND_HALF_UP);
  }

  /**
   * The head discount amount.
   * If the invoice has the vat included this value has also the var included else not.
   * @private
   */
  private _discountHeadAmount: Decimal = new Decimal(0.0);

  /**
   * The head discount amount.
   * If the invoice has the vat included this value has also the var included else not.
   */
  public get discountHeadAmount() {
    return this._discountHeadAmount.toDecimalPlaces(Number(this.totalScale), Decimal.ROUND_HALF_UP);
  }

  /**
   * The withholding tax total amount in document currency
   * @private
   */
  private _withholdingTaxAmount: Decimal = new Decimal(0.0);

  /**
   * The withholding tax total amount in document currency
   */
  public get withholdingTaxAmount() {
    return this._withholdingTaxAmount.toDecimalPlaces(Number(this.totalScale), Decimal.ROUND_HALF_UP);
  }

  /**
   * The withholding tax total amount in base currency (normally EUR)
   * @private
   */
  private _withholdingTax: Decimal = new Decimal(0.0);

  /**
   * The withholding tax total amount in base currency (normally EUR)
   * @private
   */
  public get withholdingTax() {
    return this._withholdingTax.toDecimalPlaces(2, Decimal.ROUND_HALF_UP);
  }

  /**
   * Invoice
   * @param totalScale
   * @param isVatIncluded
   * @param headDiscount
   * @param currencyRate
   * @param totalAdjustment
   */
  constructor(
    public readonly totalScale: bigint,
    public readonly isVatIncluded: boolean,
    public readonly headDiscount: Discount,
    public readonly currencyRate: Decimal,
    public readonly totalAdjustment: Decimal
  ) {

  }

  /**
   * Calculate the invoice values
   */
  public calculate(): void {

    if (this._lines.length === 0) {
      throw new InvoiceError('Invoice lines error', {
        'cause': 'Invoice without lines'
      });
    }

    // let totalBaseLines = new Decimal(0.0);
    let totalLines = new Decimal(0.0);
    let accumulatedHeadDiscount = new Decimal(0.0);

    // Calculate the lines total
    this.lines.forEach((invoiceLine) => {

      invoiceLine.baseCalculation();
      this._baseTotal = this.baseTotal.add(invoiceLine.baseTotalAmount);

      invoiceLine.discountCalculation();

      totalLines = totalLines.add(invoiceLine.totalAmount);
    });

    this._discountLinesAmount = this._baseTotal.sub(totalLines);

    // Apply header discount

    let linesIndex = 1;
    let invoiceTotal = new Decimal(0.0);

    this.lines.forEach((invoiceLine) => {

      let desc: { headDiscount: Decimal, total: Decimal };

      // Apply hed discount
      if (this.headDiscount.type === DiscountType.PERCENT) {

        desc = invoiceLine.applyHeadDiscount(this.headDiscount);

        if (this.isVatIncluded) {
          invoiceTotal = invoiceTotal.add(invoiceLine.totalAmount.sub(desc.headDiscount));
        } else {
          invoiceTotal = invoiceTotal.add(desc.total);
        }

      } else if (this.headDiscount.discount.equals(0)) {

        desc = invoiceLine.applyHeadDiscount(new Decimal(0.0));
        invoiceTotal = invoiceTotal.add(desc.total);

      } else {

        if (linesIndex === this.lines.length) {

          desc = invoiceLine.applyHeadDiscount(
            this.headDiscount.discount.sub(accumulatedHeadDiscount)
          );

          invoiceTotal = invoiceTotal.add(desc.total);

        } else {

          const factor = invoiceLine.totalAmount.div(totalLines);
          const value = this.headDiscount.discount.mul(factor);

          desc = invoiceLine.applyHeadDiscount(
            invoiceLine.unitPrice.lessThan(0) ? value.mul(-1) : value
          );

          invoiceTotal = invoiceTotal.add(desc.total);
          accumulatedHeadDiscount = accumulatedHeadDiscount.add(desc.headDiscount);
        }
      }

      invoiceTotal = invoiceTotal.toDecimalPlaces(Number(this.totalScale), Decimal.ROUND_HALF_UP);

      // Vat rates

      const lineRate = invoiceLine.vat;

      if (!(lineRate instanceof VatRate)) return;

      let vatRateTotal = this.vatRatesTotals.find(
        vrt => {
          return vrt.tax.equals(lineRate.tax) &&
            vrt.type === lineRate.type &&
            vrt.taxExemptionCode === lineRate.taxExemptionCode &&
            vrt.zone === lineRate.zone;
        }
      );

      if ((typeof vatRateTotal) === 'undefined') {

        vatRateTotal = new VatRateTotals(
          lineRate.type,
          lineRate.zone,
          lineRate.tax,
          lineRate.taxExemptionCode,
          this.totalScale,
          this.currencyRate
        );

        this.vatRatesTotals.push(vatRateTotal);
      }

      let addBaseTaxable;

      if (this.isVatIncluded) {
        vatRateTotal.addTotal(desc.total);
      } else {
        addBaseTaxable = desc.total.toDecimalPlaces(Number(this.totalScale), Decimal.ROUND_HALF_UP);
        vatRateTotal.addBaseTaxable(addBaseTaxable);
      }

      linesIndex++;
    });

    if (this.headDiscount.type === DiscountType.VALUE) {
      this._discountHeadAmount = this.headDiscount.discount;
    } else {
      this._discountHeadAmount = totalLines.sub(invoiceTotal)
        .toDecimalPlaces(Number(this.totalScale), Decimal.ROUND_HALF_UP);
    }

    // total stamp
    this.lines.forEach((invoiceLine) => {

      const lineRate = invoiceLine.vat;

      if (!(lineRate instanceof Stamp)) return;

      let addStamp: Decimal;

      if (lineRate.type === 'FIXED') {
        addStamp = lineRate.tax.mul(invoiceLine.quantity);
      } else if (this.isVatIncluded) {
        const base = invoiceLine.totalAmount.div(lineRate.tax.div(100).add(1));
        addStamp = invoiceLine.totalAmount.sub(base);
      } else {
        addStamp = invoiceLine.totalAmount.mul(lineRate.tax.div(100));
      }

      this._totalStamp = this.totalStamp.add(addStamp);

    });

    this.vatRatesTotals.forEach(vatRateTotal => {
      this._totalVat = this._totalVat.add(vatRateTotal.taxValueAmount);
    });

    this._totalVat = this._totalVat.toDecimalPlaces(Number(this.totalScale), Decimal.ROUND_HALF_UP);
    this._totalStamp = this._totalStamp.toDecimalPlaces(Number(this.totalScale), Decimal.ROUND_HALF_UP);

    this._taxAmount = this._totalVat.add(this._totalStamp);

    if (this.isVatIncluded) {
      this._totalAmount = invoiceTotal;
      this._taxableAmount = this._totalAmount.sub(this._taxAmount);
    } else {
      this._taxableAmount = invoiceTotal;
      this._totalAmount = invoiceTotal.add(this._taxAmount);
    }

    if (!this.totalAdjustment.equals(0)) {
      this._totalAmount = this._totalAmount.add(this.totalAdjustment)
        .toDecimalPlaces(Number(this.totalScale), Decimal.ROUND_HALF_UP);
    }

    this._grossTotal = this._totalAmount.mul(this.currencyRate);
    this._netTotal = this._taxableAmount.mul(this.currencyRate);
    this._taxPayable = this._taxAmount.mul(this.currencyRate);

    this.lines.forEach(invoiceLine => {

      if (invoiceLine.withholdingTaxRate === null) return;

      this._withholdingTax = this._withholdingTax.add(invoiceLine.withholdingTax);
      this._withholdingTaxAmount = this._withholdingTaxAmount.add(invoiceLine.withholdingTaxAmount);

    });
  }
}

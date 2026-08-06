import Decimal          from 'decimal.js';
import { LineError }    from './Exceptions/LineError';
import { Invoice }      from './Invoice';
import { Discount }     from './Discount';
import { VatRate }      from './Tax/VatRate';
import { Stamp }        from './Tax/Stamp';
import { DiscountType } from './DiscountType';

/**
 * Invoice line
 */
export class InvoiceLine {

  /**
   * Line total but without apply any discount.
   * If the invoice is Vat included this value has the value included else not.
   * This value is in the document currency.
   * @private
   */
  private _baseTotalAmount: Decimal = new Decimal(0.0);

  /**
   * Line total but without apply any discount.
   * If the invoice is Vat included this value has the value included else not.
   * This value is in the document currency.
   */
  public get baseTotalAmount(): Decimal {
    return this._baseTotalAmount.toDecimalPlaces(
      Number(this.invoice.totalScale), Decimal.ROUND_HALF_UP
    );
  }

  /**
   * The total amount. Is the line base total but applying also the head discount.
   * If the invoice is Vat included this value has the vat amount included else not.
   * This value is in the document currency.
   * @private
   */
  private _totalAmount: Decimal = new Decimal(0.0);

  /**
   * The total amount. Is the line base total but applying also the head discount.
   * If the invoice is Vat included this value has the vat amount included else not.
   * This value is in the document currency.
   */
  public get totalAmount(): Decimal {
    return this._totalAmount.toDecimalPlaces(
      Number(this.invoice.totalScale), Decimal.ROUND_HALF_UP
    );
  }

  /**
   * The net price.
   * The SAF-T Unit Price (Is in Euros)
   * @private
   */
  private _netPrice: Decimal = new Decimal(0.0);

  /**
   * The net price.
   * The SAF-T Unit Price (Is in Euros).
   */
  public get netPrice(): Decimal {

    if (this._baseTotalAmount.greaterThan(0) && this._netPrice.equals(0)) {
      throw new LineError(
        'Net price not calculated', {
          'cause'     : 'Net price note calculated yet',
          'lineNumber': this.lineNumber,
          'value'     : null
        });
    }

    return this._netPrice;
  }

  /**
   * SAF-T SettlementAmount (Is in Euros)
   *
   * @private
   */
  private _settlementAmount: Decimal = new Decimal(0.0);

  /**
   * SAF-T SettlementAmount (Is in Euros)
   */
  public get settlementAmount(): Decimal {
    return this._settlementAmount.toDecimalPlaces(
      Number(this.invoice.totalScale), Decimal.ROUND_HALF_UP
    );
  }

  /**
   * The line total in Euros.
   * SAFT-T debit/credit amount
   * @private
   */
  private _total = new Decimal(0.0);

  /**
   * The line total in Euros.
   * SAFT-T debit/credit amount
   * @private
   */
  public get total(): Decimal {
    return this._total.toDecimalPlaces(
      Number(this.invoice.totalScale), Decimal.ROUND_HALF_UP
    );
  }

  /**
   * Total withholding in base currency (Normally EUR)
   * @private
   */
  private _withholdingTax: Decimal = new Decimal(0.0);

  /**
   * Total withholding in base currency (Normally EUR)
   */
  public get withholdingTax(): Decimal {
    return this._withholdingTax.toDecimalPlaces(
      2, Decimal.ROUND_HALF_UP
    );
  }

  /**
   * Total withholding in document currency
   */
  public get withholdingTaxAmount(): Decimal {
    return this.withholdingTax
      .div(this.invoice.currencyRate)
      .toDecimalPlaces(Number(this.invoice.totalScale), Decimal.ROUND_HALF_UP);
  }

  /**
   * Invoice line
   * @param invoice
   * @param lineNumber
   * @param unitPrice
   * @param unitPriceScale
   * @param quantity
   * @param vat
   * @param discounts
   * @param withholdingTaxRate
   */
  constructor(
    public readonly invoice: Invoice,
    public readonly lineNumber: bigint,
    public readonly unitPrice: Decimal,
    public readonly unitPriceScale: bigint,
    public readonly quantity: Decimal,
    public readonly vat: VatRate | Stamp,
    public readonly discounts: Discount[],
    public readonly withholdingTaxRate: Decimal | null
  ) {

    if (quantity.lessThanOrEqualTo(0)) {
      throw new LineError(`Quantity error`, {
        'cause'     : `Quantity less or equal to zero`,
        'lineNumber': lineNumber,
        'value'     : quantity
      });
    }
  }

  /**
   * Calculate the product net price
   * @param totalValue
   * @private
   */
  private _calcNetPrice(totalValue: Decimal): void {

    if (this.invoice.isVatIncluded && this.vat.tax.greaterThan(0)) {
      if (this.vat instanceof VatRate || this.vat.type === 'PERCENT') {
        totalValue = totalValue.div(this.vat.tax.div(100).add(1));
      } else {
        totalValue = totalValue.sub(this.vat.tax.mul(this.quantity));
      }
    }

    this._total = totalValue.mul(this.invoice.currencyRate);
    this._netPrice = totalValue.div(this.quantity).mul(this.invoice.currencyRate);
  }

  /**
   * Calculate the settlement amount
   * @param baseSettlement
   * @private
   */
  private _calcSettlement(baseSettlement: Decimal): void {

    if (this.invoice.isVatIncluded && this.vat.tax.greaterThan(0)) {
      if (this.vat instanceof VatRate || this.vat.type === 'PERCENT') {
        baseSettlement = baseSettlement.div(this.vat.tax.div(100).add(1));
      }
    }

    this._settlementAmount = baseSettlement.mul(this.invoice.currencyRate);
  }

  /**
   * Base calculation
   */
  public baseCalculation(): void {
    this._baseTotalAmount = this.unitPrice.mul(this.quantity);
    this._totalAmount = this._baseTotalAmount;
    this._calcNetPrice(this.totalAmount);
  }

  /**
   * Calculate the invoice values applying the lines discounts
   */
  public discountCalculation(): void {

    this.discounts.forEach(discount => {

      if (discount.type === DiscountType.PERCENT && discount.discount.lessThan(0)) {
        throw new LineError(`Line discount error`, {
          'cause'     : `Discount less than zero`,
          'lineNumber': this.lineNumber,
          'value'     : discount.discount
        });
      }

      if (discount.type === DiscountType.VALUE && discount.discount.lessThan(0) && this.unitPrice.greaterThan(0)) {
        throw new LineError(`Line discount error`, {
          'cause'     : `Discount less than zero while unit price greater than 0`,
          'lineNumber': this.lineNumber,
          'value'     : discount.discount
        });
      }

      if (discount.type === DiscountType.VALUE && discount.discount.greaterThan(0) && this.unitPrice.lessThan(0)) {
        throw new LineError(`Line discount error`, {
          'cause'     : `Discount greater than zero while unit price less than 0`,
          'lineNumber': this.lineNumber,
          'value'     : discount.discount
        });
      }

      if (discount.type === DiscountType.VALUE) {
        this._totalAmount = this._totalAmount.sub(discount.discount.mul(this.quantity));
      } else {
        const disc = this._totalAmount.mul(discount.discount.div(100));
        this._totalAmount = this._totalAmount.sub(disc);
      }

    });

    this._calcNetPrice(this._totalAmount);
    this._calcSettlement(this._baseTotalAmount.sub(this._totalAmount));

  }

  /**
   * Apply head discount to the line
   * @param discount
   */
  public applyHeadDiscount(discount: Discount | Decimal):
    { headDiscount: Decimal, total: Decimal } {

    let calcTotal: Decimal;

    if (discount instanceof Discount) {

      if (discount.type === DiscountType.VALUE) {
        throw new LineError('Wrong line apply head discount', {
          'cause'     : `To apply head discount by value should be applied the absolute value to apply to the line and not the Discount instance`,
          'lineNumber': this.lineNumber,
          'value'     : null
        });
      }

      calcTotal = this._totalAmount.mul(
        (new Decimal(1)).sub(discount.discount.div(100))
      );

    } else if (discount instanceof Decimal) {

      calcTotal = this._totalAmount.sub(discount);

    } else {

      throw new LineError('Wrong line apply head discount', {
        'cause'     : 'Discount must be a instance of Discount or Decimal',
        'lineNumber': this.lineNumber,
        'value'     : null
      });
    }

    this._calcNetPrice(calcTotal);

    this._calcSettlement(this._baseTotalAmount.sub(calcTotal));

    if (this.withholdingTaxRate !== null) {
      this._withholdingTax = this._total.mul(this.withholdingTaxRate.div(100));
    }

    return {
      headDiscount: this._totalAmount.sub(calcTotal),
      total       : calcTotal
    };
  }

  /**
   * Calculate the unit price to be used in the invoice line
   * @param invoiceHasVatIncluded
   * @param price
   */
  public static calcUnitPriceForLine(
    invoiceHasVatIncluded: boolean,
    price: {
      amount: Decimal,
      hasVatIncluded: boolean,
      vatTaxRate: Decimal,
      vatTaxRateType: 'VALUE' | 'PERCENT',
      decimalsScale: bigint
    }
  ): Decimal {

    if (invoiceHasVatIncluded === price.hasVatIncluded || price.vatTaxRate.equals(0)) {
      return price.amount.toDecimalPlaces(Number(price.decimalsScale), Decimal.ROUND_HALF_UP);
    }

    if (invoiceHasVatIncluded) {
      if (price.vatTaxRateType === 'VALUE') {
        return price.amount.add(price.vatTaxRate).toDecimalPlaces(Number(price.decimalsScale), Decimal.ROUND_HALF_UP);
      }

      return price.amount.mul(
        price.vatTaxRate.div(100).add(1)).toDecimalPlaces(Number(price.decimalsScale), Decimal.ROUND_HALF_UP
      );
    }

    if (price.vatTaxRateType === 'VALUE') {
      return price.amount.sub(price.vatTaxRate).toDecimalPlaces(Number(price.decimalsScale), Decimal.ROUND_HALF_UP);
    }

    return price.amount.div(
      price.vatTaxRate.div(100).add(1)).toDecimalPlaces(Number(price.decimalsScale), Decimal.ROUND_HALF_UP
    );
  }
}

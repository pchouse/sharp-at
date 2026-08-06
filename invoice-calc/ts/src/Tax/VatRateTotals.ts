import { VatRate } from './VatRate';
import Decimal     from 'decimal.js';
import { VatType } from './VatType';
import { VatZone } from './VatZone';

export class VatRateTotals extends VatRate {

  /**
   * The base taxable amount
   * @private
   */
  private _baseTaxableAmount: Decimal = new Decimal(0.0);

  /**
   * The base taxable amount
   */
  public get baseTaxableAmount(): Decimal {
    return this._baseTaxableAmount.toDecimalPlaces(
      Number(this.taxAmountScale), Decimal.ROUND_HALF_UP
    );
  }

  /**
   * The tax amount
   * @private
   */
  private _taxValueAmount: Decimal = new Decimal(0.0);

  /**
   * The tax amount
   */
  public get taxValueAmount(): Decimal {
    return this._taxValueAmount.toDecimalPlaces(
      Number(this.taxAmountScale), Decimal.ROUND_HALF_UP
    );
  }

  /**
   * The base taxable in Euros
   */
  public get baseTaxable(): Decimal {
    return this._baseTaxableAmount.mul(this.currencyRate).toDecimalPlaces(2, Decimal.ROUND_HALF_UP);
  }

  /**
   * The tax amount in Euros
   */
  public get taxValue(): Decimal {
    return this._taxValueAmount.mul(this.currencyRate).toDecimalPlaces(2, Decimal.ROUND_HALF_UP);
  }

  /**
   *
   * @param type
   * @param zone
   * @param tax
   * @param taxExemptionCode
   * @param taxAmountScale
   * @param currencyRate
   */
  constructor(
    public readonly type: VatType,
    public readonly zone: VatZone,
    public readonly tax: Decimal,
    public readonly taxExemptionCode: string | null,
    public readonly taxAmountScale: bigint,
    public readonly currencyRate: Decimal
  ) {
    super(type, zone, tax, taxExemptionCode);
  }


  /**
   * Add base taxable amount to the vat rate totals
   * @param amount
   */
  public addBaseTaxable(amount: Decimal) {
    this._baseTaxableAmount = this._baseTaxableAmount.add(amount);
    this._taxValueAmount = this._taxValueAmount.add(
      amount.mul(this.tax.div(100))
    );
  }

  /**
   * Add total
   * @param amount
   */
  public addTotal(amount: Decimal) {

    const base = amount.div(this.tax.div(100).add(1));
    const value = amount.sub(base);

    this._baseTaxableAmount = this.baseTaxableAmount.add(base);
    this._taxValueAmount = this._taxValueAmount.add(value);
  }

}

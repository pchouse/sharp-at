import Decimal from 'decimal.js';

/**
 * Stamp tax
 */
export class Stamp {

  /**
   *
   * @param type The type of stamp tax, a fixed value or a percent per unit
   * @param tax The tax amount value or the tax rate, depending on the tax type
   */
  constructor(
    public readonly type: 'FIXED' | 'PERCENT',
    public readonly tax: Decimal
  ) {
  }

}

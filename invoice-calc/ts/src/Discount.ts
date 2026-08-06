import { DiscountType } from './DiscountType';
import Decimal          from 'decimal.js';

export class Discount {

  constructor(public readonly type: DiscountType, public readonly discount: Decimal) {

  }

}

import Decimal          from 'decimal.js';
import { VatType }      from './VatType';
import { VatZone }      from './VatZone';
import { VatRateError } from '../Exceptions/VatRateError';

/**
 * Vat a.k.a. IVA
 */
export class VatRate {

  /**
   *
   * @param type The VAT type
   * @param zone The VAT zone
   * @param tax Tha tax rate
   * @param taxExemptionCode The tax exemption code if tax rate is zero
   */
  constructor(
    public readonly type: VatType,
    public readonly zone: VatZone,
    public readonly tax: Decimal,
    public readonly taxExemptionCode: string | null
  ) {

    if (tax.lessThan(0) || tax.greaterThan(100)) {
      throw new VatRateError('Wrong tax for vat rate', {
        'cause': tax.lessThan(0) ? 'Tax less than 0' : 'Tax greater than 100'
      });
    }

    if (tax.greaterThan(0) && taxExemptionCode !== null) {
      throw new VatRateError('Wrong tax exemption code for vat rate', {
        'cause': 'Tax rate is greater than 0'
      });
    }

    if (tax.equals(0) && taxExemptionCode === null) {
      throw new VatRateError('Wrong tax exemption code for vat rate', {
        'cause': 'No tax exemption code defined and tax rate is 0'
      });
    }

    if (taxExemptionCode !== null && !/^M[0-9]{2}$/.test(taxExemptionCode)) {
      throw new VatRateError('Wrong tax exemption code for vat rate', {
        'cause': 'Wrong format of tax exemption code'
      });
    }

    if (type === VatType.ISE && !tax.equals(0)) {
      throw new VatRateError(
        'Wrong tax rate for tax type', {
          'cause': 'Tax tye is ISE but tax vat rate is not zero'
        });
    }
  }
}

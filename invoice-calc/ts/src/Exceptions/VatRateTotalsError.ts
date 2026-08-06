import { VatRateErrorCause } from './VatRateError';

/**
 * Vat rate totals error
 */
export class VatRateTotalsError extends Error {

  /**
   *
   * @param message
   * @param cause
   */
  constructor(message: string, cause: VatRateErrorCause) {
    super(message, cause);
  }
}

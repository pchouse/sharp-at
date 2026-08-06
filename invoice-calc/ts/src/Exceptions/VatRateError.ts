/**
 * Vat rate error cause
 */
export type VatRateErrorCause = ErrorOptions;

/**
 * Vat rate error
 */
export class VatRateError extends Error {
  constructor(message: string, cause: VatRateErrorCause) {
    super(message, cause);
  }
}

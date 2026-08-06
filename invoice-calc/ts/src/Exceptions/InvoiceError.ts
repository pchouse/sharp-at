/**
 * Invoice error
 */
export class InvoiceError extends Error {

  /**
   *
   * @param message
   * @param cause
   */
  constructor(message: string, cause: ErrorOptions) {
    super(message, cause);
  }

}

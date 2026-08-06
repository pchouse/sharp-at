/**
 * The causes of the price line error
 */
export type LineErrorCause = ErrorOptions & {
  lineNumber: bigint;
  value: any;
};

/**
 * Line error
 */
export class LineError extends Error {

  /**
   *
   * @param message
   * @param cause
   */
  constructor(message: string, cause: LineErrorCause) {
    super(message, cause);
  }
}

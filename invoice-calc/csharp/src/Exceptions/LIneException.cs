namespace PChouse.InvoiceCalc.Exceptions;

/// <summary>
/// Line exception
/// </summary>
/// <param name="message"></param>
/// <param name="lineErrorCause"></param>
public class LineException(string message, LineErrorCause lineErrorCause) : Exception(message)
{
    /// <summary>
    /// Line exception error cause
    /// </summary>
    public readonly LineErrorCause LineErrorCause = lineErrorCause;
}
namespace PChouse.InvoiceCalc.Exceptions;


public class InvoiceException(string message, string cause) : Exception(message)
{
    /// <summary>
    /// The exception cause
    /// </summary>
    public readonly string Cause = cause;
}

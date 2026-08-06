namespace PChouse.InvoiceCalc.Exceptions;

/// <summary>
/// Vat rate exception
/// </summary>
/// <param name="message"></param>
/// <param name="cause"></param>
public class VatRateException(string message, string cause) : Exception(message)
{
    /// <summary>
    /// The exception cause
    /// </summary>
    public readonly string Cause = cause;
}
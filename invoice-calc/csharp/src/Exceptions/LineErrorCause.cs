namespace PChouse.InvoiceCalc.Exceptions;

/// <summary>
/// Line error cause
/// </summary>
/// <param name="lineNumber"></param>
/// <param name="value"></param>
public class LineErrorCause(string cause, int lineNumber, object? value)
{
    /// <summary>
    /// The error cause
    /// </summary>
    public readonly string Cause = cause;

    /// <summary>
    /// Line number where occurred the error
    /// </summary>
    public readonly int LineNumber = lineNumber;

    /// <summary>
    /// The value that provoke the error
    /// </summary>
    public readonly object? Value = value;
}
namespace PChouse.InvoiceCalc.Tax;

/// <summary>
/// Stamp tax
/// </summary>
/// <param name="type"></param>
/// <param name="tax"></param>
public class Stamp(CalcType type, decimal tax) : IVat
{
    /// <summary>
    /// Tax calculation tax
    /// </summary>
    public readonly CalcType Type = type;

    /// <summary>
    /// Tax value
    /// </summary>
    public decimal Tax { get; } = tax;
}
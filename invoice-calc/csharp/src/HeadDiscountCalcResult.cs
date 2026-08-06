namespace PChouse.InvoiceCalc;

/// <summary>
/// Head discount calculation result
/// </summary>
/// <param name="headDiscount"></param>
/// <param name="total"></param>
public class HeadDiscountCalcResult(decimal headDiscount, decimal total)
{
    /// <summary>
    /// Head discount
    /// </summary>
    public readonly decimal HeadDiscount = headDiscount;

    /// <summary>
    /// Total
    /// </summary>
    public readonly decimal Total = total;
}
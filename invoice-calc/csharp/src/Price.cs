namespace PChouse.InvoiceCalc;

/// <summary>
/// 
/// </summary>
/// <param name="amount"></param>
/// <param name="hasVatIncluded"></param>
/// <param name="vatTaxRate"></param>
/// <param name="vatTaxRateType"></param>
/// <param name="decimalsScale"></param>
public class Price(
      decimal amount,
      bool hasVatIncluded,
      decimal vatTaxRate,
      CalcType vatTaxRateType,
      int decimalsScale
)
{
    /// <summary>
    /// 
    /// </summary>
    public readonly decimal Amount = amount;

    /// <summary>
    /// 
    /// </summary>
    public readonly bool HasVatIncluded = hasVatIncluded;

    /// <summary>
    /// 
    /// </summary>
    public readonly decimal VatTaxRate = vatTaxRate;

    /// <summary>
    /// 
    /// </summary>
    public readonly CalcType VatTaxRateType = vatTaxRateType;

    /// <summary>
    /// 
    /// </summary>
    public int DecimalsScale = decimalsScale;
}
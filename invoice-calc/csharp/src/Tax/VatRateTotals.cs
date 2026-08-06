namespace PChouse.InvoiceCalc.Tax;

/// <summary>
/// Invoice Vat rate totals
/// </summary>
/// <param name="type"></param>
/// <param name="zone"></param>
/// <param name="tax"></param>
/// <param name="taxExemptionCode"></param>
/// <param name="taxAmountScale"></param>
/// <param name="currencyRate"></param>
public class VatRateTotals(
    VatType type,
    VatZone zone,
    decimal tax,
    string? taxExemptionCode,
    int taxAmountScale,
    decimal currencyRate
  ) : VatRate(type, zone, tax, taxExemptionCode)
{
    /// <summary>
    /// Tax amount scale
    /// </summary>
    public readonly int TaxAmountScale = taxAmountScale;

    /// <summary>
    /// Currency rate
    /// </summary>
    public readonly decimal CurrencyRate = currencyRate;

    /// <summary>
    /// Base taxable amount in document currency
    /// </summary>
    private decimal _baseTaxableAmount = 0.0m;

    /// <summary>
    /// Base taxable amount in document currency
    /// </summary>
    public decimal BaseTaxableAmount
    {
        get { return Math.Round(_baseTaxableAmount, this.TaxAmountScale, MidpointRounding.AwayFromZero); }
        private set { _baseTaxableAmount = value; }
    }

    /// <summary>
    /// Tax value amount in document currency
    /// </summary>
    private decimal _taxValueAmount = 0.0m;

    /// <summary>
    /// Tax value amount in document currency
    /// </summary>
    public decimal TaxValueAmount
    {
        get { return Math.Round(_taxValueAmount, this.TaxAmountScale, MidpointRounding.AwayFromZero); }
        private set { _baseTaxableAmount = value; }
    }

    /// <summary>
    /// Base taxable in base currency (Normally EUR)
    /// </summary>
    public decimal BaseTaxable
    {
        get
        {
            return Math.Round(_baseTaxableAmount * CurrencyRate, 2, MidpointRounding.AwayFromZero);
        }
    }

    /// <summary>
    /// Base value in base currency (Normally EUR)
    /// </summary>
    public decimal TaxValue
    {
        get
        {
            return Math.Round(_taxValueAmount * CurrencyRate, 2, MidpointRounding.AwayFromZero);
        }
    }

    /// <summary>
    /// Add base taxable amount to the vat rate totals
    /// </summary>
    /// <param name="amount"></param>
    public void AddBaseTaxable(decimal amount)
    {
        _baseTaxableAmount += amount;
        _taxValueAmount += (amount * (Tax / 100));
    }

    /// <summary>
    /// Add total
    /// </summary>
    /// <param name="amount"></param>
    public void AddTotal(decimal amount)
    {
        var valueBase = amount / ((Tax / 100) + 1);
        var value = amount - valueBase;

        _baseTaxableAmount += valueBase;
        _taxValueAmount += value;
    }
}
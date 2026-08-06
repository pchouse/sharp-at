using PChouse.InvoiceCalc.Exceptions;
using PChouse.InvoiceCalc.Tax;

namespace PChouse.InvoiceCalc;

/// <summary>
/// 
/// </summary>
/// <param name="totalScale"></param>
/// <param name="isVatIncluded"></param>
/// <param name="headDiscount"></param>
/// <param name="currencyRate"></param>
/// <param name="totalAdjustment"></param>
public class Invoice(
    int totalScale,
    bool isVatIncluded,
    Discount headDiscount,
    decimal currencyRate,
    decimal totalAdjustment
)
{
    /// <summary>
    /// Total scale
    /// </summary>
    public readonly int TotalScale = totalScale;

    /// <summary>
    /// Invoice has vat included
    /// </summary>
    public readonly bool IsVatIncluded = isVatIncluded;

    /// <summary>
    /// Head discount
    /// </summary>
    public readonly Discount HeadDiscount = headDiscount;

    /// <summary>
    /// Currency rate
    /// </summary>
    public readonly decimal CurrencyRate = currencyRate;

    /// <summary>
    /// Total adjustment
    /// </summary>
    public readonly decimal TotalAdjustment = totalAdjustment;


    /// <summary>
    /// The invoice base total.
    /// Calculated before any discount and if invoices has not the vat included before apply vat else with vat
    /// </summary>
    private decimal _baseTotal = 0m;

    /// <summary>
    ///The invoice base total.
    ///Calculated before any discount and if invoices has not the var included before apply vat else with vat
    ///</summary>
    public Decimal BaseTotal
    {
        get
        {
            return Math.Round(_baseTotal, TotalScale, MidpointRounding.AwayFromZero);
        }
    }

    /// <summary>
    /// The invoice total in the invoice currency
    /// </summary>
    private decimal _totalAmount = 0m;

    /// <summary>
    /// The invoice total in the invoice currency
    /// </summary>
    public decimal TotalAmount
    {
        get
        {
            return Math.Round(_totalAmount, TotalScale, MidpointRounding.AwayFromZero);
        }
    }

    /// <summary>
    /// The invoice tax amount in the invoice currency (Vat + Stamp)
    /// </summary>
    private decimal _taxAmount = 0m;

    /// <summary>
    /// The invoice tax amount in the invoice currency (Vat + Stamp)
    /// </summary>
    public decimal TaxAmount
    {
        get
        {
            return Math.Round(_taxAmount, TotalScale, MidpointRounding.AwayFromZero);
        }
    }

    /// <summary>
    /// The taxable amount in the invoice currency
    /// </summary>
    private decimal _taxableAmount = 0m;

    /// <summary>
    /// The taxable amount in the invoice currency
    /// </summary>
    public decimal TaxableAmount
    {
        get
        {
            return Math.Round(_taxableAmount, TotalScale, MidpointRounding.AwayFromZero);
        }
    }

    /// <summary>
    /// The invoice gross total.
    /// (Correspond to SAFT-PT DocumentTotal/GrossTotal
    /// </summary>
    private decimal _grossTotal = 0m;

    /// <summary>
    /// The invoice gross total.
    /// (Correspond to SAFT-PT DocumentTotal/GrossTotal
    /// </summary>
    public decimal GrossTotal
    {
        get
        {
            return Math.Round(_grossTotal, 2, MidpointRounding.AwayFromZero);
        }
    }

    /// <summary>
    /// The invoice tax payable.
    /// (Correspond to SAFT-PT DocumentTotal/TaxPayable
    ///
    private decimal _taxPayable = 0m;

    /// <summary>
    /// The invoice tax payable.
    /// (Correspond to SAFT-PT DocumentTotal/TaxPayable
    /// </summary>
    public decimal TaxPayable
    {
        get
        {
            return Math.Round(_taxPayable, 2, MidpointRounding.AwayFromZero);
        }
    }

    /// <summary>
    /// The invoice net total.
    /// (Correspond to SAFT-PT DocumentTotal/NetTotal
    ///
    private decimal _netTotal = 0m;

    /// <summary>
    /// The invoice net total.
    /// (Correspond to SAFT-PT DocumentTotal/NetTotal
    /// </summary>
    public decimal NetTotal
    {
        get
        {
            return Math.Round(_netTotal, 2, MidpointRounding.AwayFromZero);
        }
    }

    /// <summary>
    /// Vat rate totals
    /// </summary>
    private List<VatRateTotals> _vatRatesTotals = [];

    /// <summary>
    /// Vat rate totals
    /// </summary>
    public List<VatRateTotals> VatRatesTotals()
    {
        return this._vatRatesTotals;
    }

    /// <summary>
    /// Total of stamp tax
    /// </summary>
    private decimal _totalStamp = 0m;

    /// <summary>
    /// Total of stamp tax
    /// </summary>
    public decimal TotalStamp
    {
        get
        {
            return this._totalStamp;
        }
    }

    /// <summary>
    /// Vat total
    /// </summary>
    private decimal _totalVat = 0m;

    /// <summary>
    /// Vat total
    /// </summary>
    public decimal TotalVat
    {
        get
        {
            return this._totalVat;
        }
    }

    /// <summary>
    /// Invoice lines stack
    /// </summary>
    private List<InvoiceLine> _lines = [];

    /// <summary>
    /// Invoice lines stack
    /// </summary>
    public List<InvoiceLine> Lines
    {
        get
        {
            return _lines;
        }
    }

    /// <summary>
    /// The total amount of discounts.
    /// If the invoice has the vat included this value has also the var included else not.
    /// </summary>
    public decimal DiscountAmount
    {
        get
        {
            return Math.Round(_discountLinesAmount + _discountHeadAmount, TotalScale, MidpointRounding.AwayFromZero);
        }
    }

    /// <summary>
    /// The lines discount amount.
    /// If the invoice has the vat included this value has also the var included else not.
    /// </summary>
    private decimal _discountLinesAmount = 0m;

    /// <summary>
    /// The lines discount amount.
    /// If the invoice has the vat included this value has also the var included else not.
    /// </summary>
    public decimal DiscountLinesAmount
    {
        get
        {
            return Math.Round(_discountLinesAmount, TotalScale, MidpointRounding.AwayFromZero);
        }
    }

    /// <summary>
    /// The head discount amount.
    /// If the invoice has the vat included this value has also the var included else not.
    /// </summary>
    private decimal _discountHeadAmount = 0m;

    /// <summary>
    /// The head discount amount.
    /// If the invoice has the vat included this value has also the var included else not.
    /// </summary>
    public decimal DiscountHeadAmount
    {
        get
        {
            return Math.Round(_discountHeadAmount, TotalScale, MidpointRounding.AwayFromZero);
        }
    }

    /// <summary>
    /// The withholding tax total amount in document currency
    /// </summary>
    private decimal _withholdingTaxAmount = 0m;

    /// <summary>
    /// The withholding tax total amount in document currency
    /// </summary>
    public decimal WithholdingTaxAmount
    {
        get
        {
            return Math.Round(_withholdingTaxAmount, TotalScale, MidpointRounding.AwayFromZero);
        }
    }

    /// <summary>
    /// The withholding tax total amount in base currency (normally EUR)
    /// </summary>
    private decimal _withholdingTax = 0m;

    /// <summary>
    /// The withholding tax total amount in base currency (normally EUR)
    /// </summary>
    public decimal WithholdingTax
    {
        get
        {
            return Math.Round(_withholdingTax, 2, MidpointRounding.AwayFromZero);
        }
    }

    /// <summary>
    /// Calculate the invoice values
    /// </summary>
    /// <exception cref="InvoiceException"></exception>
    public void Calculate()
    {

        if (this._lines.Count == 0)
        {
            throw new InvoiceException("Invoice lines error", "Invoice without lines");
        }

        // let totalBaseLines = new Decimal(0.0);
        var totalLines = 0m;
        var accumulatedHeadDiscount = 0m;

        // Calculate the lines total
        foreach (var invoiceLine in _lines)
        {

            invoiceLine.BaseCalculation();
            _baseTotal = BaseTotal + invoiceLine.BaseTotalAmount;

            invoiceLine.DiscountCalculation();

            totalLines += invoiceLine.TotalAmount;
        }

        _discountLinesAmount = _baseTotal - totalLines;

        // Apply header discount

        var linesIndex = 1;
        var invoiceTotal = 0m;

        foreach (var invoiceLine in _lines)
        {

            HeadDiscountCalcResult desc;

            // Apply hed discount
            if (HeadDiscount.Type == DiscountType.PERCENT)
            {

                desc = invoiceLine.ApplyHeadDiscount(HeadDiscount);

                if (IsVatIncluded)
                {
                    invoiceTotal += invoiceLine.TotalAmount - desc.HeadDiscount;
                }
                else
                {
                    invoiceTotal += desc.Total;
                }
            }
            else if (HeadDiscount.DiscountVal == 0)
            {
                desc = invoiceLine.ApplyHeadDiscount(0m);
                invoiceTotal += desc.Total;
            }
            else
            {
                if (linesIndex == _lines.Count)
                {
                    desc = invoiceLine.ApplyHeadDiscount(HeadDiscount.DiscountVal - accumulatedHeadDiscount);
                    invoiceTotal += desc.Total;
                }
                else
                {
                    var factor = invoiceLine.TotalAmount / totalLines;
                    var value = HeadDiscount.DiscountVal * factor;

                    desc = invoiceLine.ApplyHeadDiscount(
                      invoiceLine.UnitPrice < 0 ? value * -1 : value
                    );

                    invoiceTotal += desc.Total;
                    accumulatedHeadDiscount += desc.HeadDiscount;
                }
            }

            invoiceTotal = Math.Round(invoiceTotal, TotalScale, MidpointRounding.AwayFromZero);

            // Vat rates

            var lineRate = invoiceLine.Vat;

            if (lineRate is not VatRate vatRate) continue;

            var vatRateTotal = _vatRatesTotals.FirstOrDefault(vrt =>
            {
                return vrt.Tax == vatRate.Tax &&
                       vrt.Type == vatRate.Type &&
                       vrt.TaxExemptionCode == vatRate.TaxExemptionCode &&
                       vrt.Zone == vatRate.Zone;
            });

            if (vatRateTotal == null)
            {
                vatRateTotal = new VatRateTotals(
                  vatRate.Type,
                  vatRate.Zone,
                  vatRate.Tax,
                  vatRate.TaxExemptionCode,
                  TotalScale,
                  CurrencyRate
                );

                _vatRatesTotals.Add(vatRateTotal);
            }

            decimal addBaseTaxable;

            if (IsVatIncluded)
            {
                vatRateTotal.AddTotal(desc.Total);
            }
            else
            {
                addBaseTaxable = Math.Round(desc.Total, TotalScale, MidpointRounding.AwayFromZero);
                vatRateTotal.AddBaseTaxable(addBaseTaxable);
            }

            linesIndex++;
        }

        if (HeadDiscount.Type == DiscountType.VALUE)
        {
            _discountHeadAmount = HeadDiscount.DiscountVal;
        }
        else
        {
            this._discountHeadAmount = Math.Round(totalLines - invoiceTotal, TotalScale, MidpointRounding.AwayFromZero);
        }

        // total stamp
        foreach (var invoiceLine in _lines)
        {
            var lineRate = invoiceLine.Vat;

            if (lineRate is not Stamp stamp) continue;

            decimal addStamp;

            if (stamp.Type == CalcType.FIXED)
            {
                addStamp = stamp.Tax * invoiceLine.Quantity;
            }
            else if (IsVatIncluded)
            {
                var baseVal = invoiceLine.TotalAmount / ((stamp.Tax / 100) + 1);
                addStamp = invoiceLine.TotalAmount - baseVal;
            }
            else
            {
                addStamp = invoiceLine.TotalAmount * (stamp.Tax / 100);
            }

            _totalStamp = TotalStamp + addStamp;
        }

        foreach (var vatRateTotal in _vatRatesTotals)
        {
            _totalVat += vatRateTotal.TaxValueAmount;
        }

        _totalVat = Math.Round(_totalVat, TotalScale, MidpointRounding.AwayFromZero);
        _totalStamp = Math.Round(_totalStamp, TotalScale, MidpointRounding.AwayFromZero);

        _taxAmount = _totalVat + _totalStamp;

        if (IsVatIncluded)
        {
            _totalAmount = invoiceTotal;
            _taxableAmount = TotalAmount - _taxAmount;
        }
        else
        {
            _taxableAmount = invoiceTotal;
            _totalAmount = invoiceTotal + _taxAmount;
        }

        if (TotalAdjustment != 0)
        {
            _totalAmount = Math.Round(_totalAmount + TotalAdjustment, TotalScale, MidpointRounding.AwayFromZero);
        }

        _grossTotal = _totalAmount * CurrencyRate;
        _netTotal = _taxableAmount * CurrencyRate;
        _taxPayable = _taxAmount * CurrencyRate;

        foreach (var invoiceLine in _lines)
        {
            if (invoiceLine.WithholdingTaxRate == null) continue;

            _withholdingTax += invoiceLine.WithholdingTax;
            _withholdingTaxAmount += invoiceLine.WithholdingTaxAmount;
        }
    }
}

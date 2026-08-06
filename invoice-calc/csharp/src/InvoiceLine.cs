using PChouse.InvoiceCalc.Exceptions;
using PChouse.InvoiceCalc.Tax;

namespace PChouse.InvoiceCalc;

/// <summary>
/// Invoice line
/// </summary>
public class InvoiceLine
{
    /// <summary>
    /// The invoice whom this invoice belongs 
    /// </summary>
    public Invoice Invoice;

    /// <summary>
    /// Line number
    /// </summary>
    public int LineNumber;

    /// <summary>
    /// The unite price
    /// </summary>
    public decimal UnitPrice;

    /// <summary>
    /// Unit price scale
    /// </summary>
    public int UnitPriceScale;

    /// <summary>
    /// Quantity
    /// </summary>
    public decimal Quantity;

    /// <summary>
    /// Vat rate or Stamp vat
    /// </summary>
    public IVat Vat;

    /// <summary>
    /// Discounts to apply to the line 
    /// </summary>
    public List<Discount> Discounts = [];

    /// <summary>
    /// Withholding tax to apply to the line
    /// </summary>
    public decimal? WithholdingTaxRate;

    private decimal _baseTotalAmount = 0m;

    /// <summary>
    /// Line total but without apply any discount.
    /// If the invoice is Vat included this value has the value included else not.
    /// This value is in the document currency.
    /// </summary>
    public decimal BaseTotalAmount
    {
        get
        {
            return Math.Round(_baseTotalAmount, Invoice.TotalScale, MidpointRounding.AwayFromZero);
        }
    }

    private decimal _totalAmount = 0m;

    /// <summary>
    /// The total amount. Is the line base total but applying also the head discount.
    /// If the invoice is Vat included this value has the vat amount included else not.
    /// This value is in the document currency.
    /// </summary>
    public decimal TotalAmount
    {
        get
        {
            return Math.Round(_totalAmount, Invoice.TotalScale, MidpointRounding.AwayFromZero);
        }
    }

    private decimal _netPrice = 0m;

    /// <summary>
    /// The net price.
    /// The SAF-T Unit Price (Is in Euros).
    /// </summary>
    public decimal NetPrice
    {
        get
        {
            if (BaseTotalAmount > 0 && _netPrice == 0)
            {
                throw new LineException(
                  "Net price not calculated", new LineErrorCause(
                      "Net price note calculated yet", LineNumber, null
                  ));
            }

            return _netPrice;
        }
    }

    private decimal _settlementAmount = 0.0m;

    /// <summary>
    /// SAF-T SettlementAmount (Is in Euros)
    /// </summary>
    public decimal SettlementAmount
    {
        get
        {
            return Math.Round(_settlementAmount, Invoice.TotalScale, MidpointRounding.AwayFromZero);
        }
        private set
        {
            _settlementAmount = value;
        }
    }

    private decimal _total = 0m;

    /// <summary>
    /// The line total in Euros.
    // SAFT-T debit/credit amount
    /// </summary>
    public decimal Total
    {
        get
        {
            return Math.Round(_total, Invoice.TotalScale, MidpointRounding.AwayFromZero);
        }
    }

    /// <summary>
    /// Total withholding in base currency (Normally EUR)
    /// </summary>
    public decimal WithholdingTax
    {
        get
        {
            return Math.Round(field, 2, MidpointRounding.AwayFromZero);
        }
        private set
        {
            field = value;
        }
    } = 0m;

    /// <summary>
    /// Total withholding in document currency
    /// </summary>
    public decimal WithholdingTaxAmount
    {
        get
        {
            return Math.Round(WithholdingTax / Invoice.CurrencyRate, Invoice.TotalScale, MidpointRounding.AwayFromZero);
        }
    }

    /// <summary>
    /// Invoice line
    /// </summary>
    /// <param name="invoice"></param>
    /// <param name="lineNumber"></param>
    /// <param name="unitPrice"></param>
    /// <param name="unitPriceScale"></param>
    /// <param name="quantity"></param>
    /// <param name="vat"></param>
    /// <param name="discounts"></param>
    /// <param name="withholdingTaxRate"></param>
    /// <exception cref="LineException"></exception>
    public InvoiceLine(
        Invoice invoice,
        int lineNumber,
        decimal unitPrice,
        int unitPriceScale,
        decimal quantity,
        IVat vat,
        List<Discount> discounts,
        decimal? withholdingTaxRate
    )
    {
        Invoice = invoice;
        LineNumber = lineNumber;
        UnitPrice = unitPrice;
        UnitPriceScale = unitPriceScale;
        Quantity = quantity;
        Vat = vat;
        Discounts = discounts;
        WithholdingTaxRate = withholdingTaxRate;

        if (quantity <= 0)
        {
            throw new LineException("Quantity error", new LineErrorCause(
                    "Quantity less or equal to zero", lineNumber, quantity
            ));
        }
    }

    /// <summary>
    /// Calculate the product net price
    /// </summary>
    /// <param name="totalValue"></param>
    private void CalcNetPrice(decimal totalValue)
    {
        if (Invoice.IsVatIncluded && Vat.Tax > 0)
        {
            if (Vat is VatRate vatRate || (Vat is Stamp stamp && stamp.Type == CalcType.PERCENT))
            {
                totalValue = totalValue / ((Vat.Tax / 100) + 1);
            }
            else
            {
                totalValue = totalValue - (Vat.Tax * Quantity);
            }
        }

        this._total = totalValue * Invoice.CurrencyRate;
        this._netPrice = (totalValue / Quantity) * Invoice.CurrencyRate;
    }

    /// <summary>
    /// Calculate the settlement amount
    /// </summary>
    /// <param name="baseSettlement"></param>
    private void CalcSettlement(decimal baseSettlement)
    {
        if (Invoice.IsVatIncluded && Vat.Tax > 0)
        {
            if (Vat is VatRate vatRate || (Vat is Stamp stamp && stamp.Type == CalcType.PERCENT))
            {
                baseSettlement /= (Vat.Tax / 100) + 1;
            }
        }

        _settlementAmount = baseSettlement * Invoice.CurrencyRate;
    }

    /// <summary>
    /// Base calculation
    /// </summary>
    public void BaseCalculation()
    {
        if (Quantity <= 0)
        {
            throw new LineException("Quantity error", new LineErrorCause(
                    "Quantity less or equal to zero", LineNumber, Quantity
            ));
        }

        _baseTotalAmount = UnitPrice * Quantity;
        _totalAmount = _baseTotalAmount + 0;
        CalcNetPrice(TotalAmount);
    }

    /// <summary>
    /// Calculate the invoice values applying the lines discounts
    /// </summary>
    /// <exception cref="LineException"></exception>
    public void DiscountCalculation()
    {
        foreach (var discount in Discounts)
        {

            if (discount.Type == DiscountType.PERCENT && discount.DiscountVal < 0)
            {
                throw new LineException("Line discount error", new LineErrorCause(
                    "Discount less than zero", LineNumber, discount.DiscountVal
                    ));
            }

            if (discount.Type == DiscountType.VALUE && discount.DiscountVal < 0 && UnitPrice > 0)
            {
                throw new LineException("Line discount error", new LineErrorCause(
                    "Discount less than zero while unit price greater than 0", LineNumber, discount.DiscountVal
                ));
            }

            if (discount.Type == DiscountType.VALUE && discount.DiscountVal > 0 && UnitPrice < 0)
            {
                throw new LineException("Line discount error", new LineErrorCause(
                    "Discount greater than zero while unit price less than 0", LineNumber, discount.DiscountVal
                ));
            }

            if (discount.Type == DiscountType.VALUE)
            {
                _totalAmount -= discount.DiscountVal * Quantity;
            }
            else
            {
                var disc = _totalAmount * (discount.DiscountVal / 100);
                _totalAmount -= disc;
            }

        }

        CalcNetPrice(_totalAmount);
        CalcSettlement(_baseTotalAmount - _totalAmount);
    }

    /// <summary>
    /// Apply head discount to the line
    /// </summary>
    /// <param name="discount"></param>
    /// <returns></returns>
    public HeadDiscountCalcResult ApplyHeadDiscount(decimal discount)
    {
        var calcTotal = this._totalAmount - discount;

        CalcNetPrice(calcTotal);
        CalcSettlement(this._baseTotalAmount - calcTotal);

        if (WithholdingTaxRate.HasValue)
        {
            WithholdingTax = _total * (WithholdingTaxRate.Value / 100);
        }

        return new HeadDiscountCalcResult(_totalAmount - calcTotal, calcTotal);
    }

    /// <summary>
    /// Apply head discount to the line
    /// </summary>
    /// <param name="discount"></param>
    /// <returns></returns>
    /// <exception cref="LineException"></exception>
    public HeadDiscountCalcResult ApplyHeadDiscount(Discount discount)
    {


        if (discount.Type == DiscountType.VALUE)
        {
            throw new LineException("Wrong line apply head discount", new LineErrorCause(
                "To apply head discount by value should be applied the absolute value to apply to the line and not the Discount instance",
                LineNumber,
                null
            ));
        }

        var calcTotal = this._totalAmount * (1m - (discount.DiscountVal / 100));

        CalcNetPrice(calcTotal);
        CalcSettlement(this._baseTotalAmount - calcTotal);

        if (WithholdingTaxRate.HasValue)
        {
            WithholdingTax = _total * (WithholdingTaxRate.Value / 100);
        }

        return new HeadDiscountCalcResult(_totalAmount - calcTotal, calcTotal);
    }

    /// <summary>
    /// Calculate the unit price to be used in the invoice line
    /// </summary>
    /// <param name="invoiceHasVatIncluded"></param>
    /// <param name="price"></param>
    /// <returns></returns>
    public static decimal CalcUnitPriceForLine(Boolean invoiceHasVatIncluded, Price price)
    {

        if (invoiceHasVatIncluded == price.HasVatIncluded || price.VatTaxRate == 0)
        {
            return Math.Round(price.Amount, price.DecimalsScale, MidpointRounding.AwayFromZero);
        }

        if (invoiceHasVatIncluded)
        {
            if (price.VatTaxRateType == CalcType.FIXED)
            {
                return Math.Round(
                    price.Amount + price.VatTaxRate, price.DecimalsScale, MidpointRounding.AwayFromZero
                );
            }

            return Math.Round(
                price.Amount * ((price.VatTaxRate / 100) + 1), price.DecimalsScale, MidpointRounding.AwayFromZero
            );
        }

        if (price.VatTaxRateType == CalcType.FIXED)
        {
            return Math.Round(
                price.Amount - price.VatTaxRate, price.DecimalsScale, MidpointRounding.AwayFromZero
             );
        }

        return Math.Round(
            price.Amount / ((price.VatTaxRate / 100) + 1), price.DecimalsScale, MidpointRounding.AwayFromZero
        );
    }

}

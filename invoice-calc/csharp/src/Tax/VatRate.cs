using System.Text.RegularExpressions;
using PChouse.InvoiceCalc.Exceptions;

namespace PChouse.InvoiceCalc.Tax;

/// <summary>
/// Vat rate
/// </summary>
public class VatRate : IVat
{
    /// <summary>
    /// Vat rate type
    /// </summary>
    public readonly VatType Type;

    /// <summary>
    /// Vat rate zone
    /// </summary>
    public readonly VatZone Zone;

    /// <summary>
    /// Vat rate tax
    /// </summary>
    public decimal Tax { get; }

    /// <summary>
    /// Tax exemption code
    /// </summary>
    public readonly string? TaxExemptionCode;

    /// <summary>
    /// Vat rate
    /// </summary>
    /// <param name="type"></param>
    /// <param name="zone"></param>
    /// <param name="tax"></param>
    /// <param name="taxExemptionCode"></param>
    /// <exception cref="VatRateException"></exception>
    public VatRate(VatType type, VatZone zone, decimal tax, string? taxExemptionCode)
    {
        Type = type;
        Zone = zone;
        Tax = tax;
        TaxExemptionCode = taxExemptionCode;

        if (tax < 0 || tax > 100)
        {
            throw new VatRateException("Wrong tax for vat rate",
                tax < 0 ? "Tax less than 0" : "Tax greater than 100"
            );
        }

        if (tax > 0 && taxExemptionCode != null)
        {
            throw new VatRateException("Wrong tax exemption code for vat rate", "Tax rate is greater than 0");
        }

        if (tax == 0 && taxExemptionCode == null)
        {
            throw new VatRateException(
                "Wrong tax exemption code for vat rate",
                "No tax exemption code defined and tax rate is 0"
            );
        }

        if (taxExemptionCode != null && !Regex.IsMatch(taxExemptionCode, "^M[0-9]{2}$"))
        {
            throw new VatRateException(
                "Wrong tax exemption code for vat rate",
                "Wrong format of tax exemption code"
            );
        }

        if (type == VatType.ISE && tax != 0)
        {
            throw new VatRateException(
              "Wrong tax rate for tax type",
              "Tax tye is ISE but tax vat rate is not zero"
            );
        }
    }
}
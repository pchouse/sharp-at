using System.Text.Json.Serialization;

namespace PChouse.InvoiceCalc;

/// <summary>
/// Discount
/// </summary>
/// <param name="type"></param>
/// <param name="discount"></param>
public class Discount(DiscountType type, decimal discount)
{
    /// <summary>
    /// Discount type
    /// </summary>
    public readonly DiscountType Type = type;

    /// <summary>
    /// The discount val amount or percent
    /// </summary>
    public readonly decimal DiscountVal = discount;

}
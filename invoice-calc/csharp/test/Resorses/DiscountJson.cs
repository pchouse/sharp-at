using System.Text.Json.Serialization;
using PChouse.InvoiceCalc;

namespace InvoiceCalcTest.Resources;

/// <summary>
/// Discount
/// </summary>
/// <param name="type"></param>
/// <param name="discount"></param>
public class DiscountJson
{
    /// <summary>
    /// Discount type
    /// </summary>
    [JsonPropertyName("type")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public DiscountType Type { get; set; }

    /// <summary>
    /// The discount val amount or percent
    /// </summary>
    [JsonPropertyName("discount")]
    public decimal DiscountVal { get; set; }

}
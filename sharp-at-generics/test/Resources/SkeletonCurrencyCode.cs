
using System.Text.Json.Serialization;

namespace PChouse.AT.Generics.Test;

public class SkeletonCurrencyCode
{
    [JsonPropertyName("Currency")]
    public CurrencyCode Currency { get; set; }
}
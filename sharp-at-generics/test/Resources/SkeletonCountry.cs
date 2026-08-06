using System.Text.Json.Serialization;

namespace PChouse.AT.Generics.Test;

public class SkeletonCountry
{
   [JsonPropertyName("Country")]
   public Country Country { get; set; }
}
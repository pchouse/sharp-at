/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Text.Json.Serialization;
using PChouse.AT.Generics;

namespace PChouse.AT.QRCode.Library;

public record Vat
{

    /// <summary>
    /// Fields: I1, J1, K1
    /// <para>Fill in according to the technical notes for the SAF-T(PT) `TaxCountryRegion` field.</para>
    //  <para>For documents where the VAT rate—which should appear in SAF-T(PT) table 4.2, 4.3, or 4.4—is not indicated, fill in with "0" (I1:0).</para>
    /// </summary>
    [JsonPropertyName(nameof(TaxCountryRegion))]
    [Field(Position = 0, Fields = ["I1", "J1", "K1"], MaximLength = 4, IsMandatory = true)]
    public virtual required TaxCountryRegion TaxCountryRegion { get; set; }

    /// <summary>
    /// Fields: I2, J2, K2
    /// <para>VAT-exempt tax base for this fiscal region</para>
    /// <para>Total value of the tax base exempt from VAT
    /// (including taxable transactions under Stamp Duty,</para>
    /// </summary>
    [JsonPropertyName(nameof(ExemptedBaseVat))]
    [Field(Position = 1, Fields = ["I2", "J2", "K2"], MaximLength = 16, IsMandatory = false)]
    public virtual decimal? ExemptedBaseVat { get; set; } = null;

    /// <summary>
    /// Fields: I3, J3, K3
    /// VAT tax base at reduced rate < br >
    /// Total value of the tax base subject to the reduced VAT rate.
    /// @param float $value Total value of the tax base
    /// </summary>
    [JsonPropertyName(nameof(ReducedBaseVat))]
    [Field(Position = 2, Fields = ["I3", "J3", "K3"], MaximLength = 16, IsMandatory = false)]
    public virtual decimal? ReducedBaseVat { get; set; } = null;

    /// <summary>
    /// Fields: I4, J4, K4
    /// <para>Total VAT at reduced rate</para>
    /// </summary>
    [JsonPropertyName(nameof(ReducedTotalVat))]
    [Field(Position = 3, Fields = ["I4", "J4", "K4"], MaximLength = 16, IsMandatory = false)]
    public virtual decimal? ReducedTotalVat { get; set; } = null;

    /// <summary>
    /// Fields: I5, J5, K5<br>
    /// <para>VAT tax base at intermediate rate</para>
    /// <para>Total value of the tax base subject to the intermediate VAT rate.</para>
    /// </summary>
    [JsonPropertyName(nameof(IntermediateBaseVat))]
    [Field(Position = 4, Fields = ["I5", "J5", "K5"], MaximLength = 16, IsMandatory = false)]
    public virtual decimal? IntermediateBaseVat { get; set; } = null;

    /// <summary>
    /// Fields: I6, J6, K6
    /// <para>Total VAT at intermediate rate</para>
    /// </summary>
    [JsonPropertyName(nameof(IntermediateTotalVat))]
    [Field(Position = 5, Fields = ["I6", "J6", "K6"], MaximLength = 16, IsMandatory = false)]
    public virtual decimal? IntermediateTotalVat { get; set; } = null;

    /// <summary>
    /// Fields: I7, J7, K7
    /// VAT tax base at normal rate < br >
    /// Total value of the tax base subject to the normal VAT rate.
    /// </summary>
    [JsonPropertyName(nameof(NormalBaseVat))]
    [Field(Position = 6, Fields = ["I7", "J7", "K7"], MaximLength = 16, IsMandatory = false)]
    public virtual decimal? NormalBaseVat { get; set; } = null;

    /// <summary>
    /// Fields: I8, J8, K8
    /// <para>Total VAT at normal rate</para>
    /// </summary>
    [JsonPropertyName(nameof(NormalTotalVat))]
    [Field(Position = 7, Fields = ["I8", "J8", "K8"], MaximLength = 16, IsMandatory = false)]
    public virtual decimal? NormalTotalVat { get; set; } = null;

}
/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Text.Json.Serialization;
using PChouse.AT.Generics;

namespace PChouse.AT.QRCode.Library;

/// <summary>
/// The document information base properties
/// </summary>
public abstract record ADocument
{
    /// <summary>
    /// Field: A (This field is mandatory)
    /// <para>Issuer TIN(NIF)</para>
    /// <para>Fill in with the issuer's TIN (NIF) without Country prefix</para>
    /// </summary>
    [JsonPropertyName(nameof(IssuerTin))]
    [Field(Position = 0, Fields = ["A"], MaximLength = 9, IsMandatory = true)]
    public virtual required string IssuerTin { get; set; }

    /// <summary>
    /// Field: B (This field is mandatory)<br>
    /// <para>Buyer's TIN (NIF)</para>
    /// <para>Fill in with the buyer's TIN. When issuing a document</para>
    /// <para>the “Final Consumer” fill in with 999999990.</para>
    /// </summary>
    [JsonPropertyName(nameof(BuyerTin))]
    [Field(Position = 1, Fields = ["B"], MaximLength = 30, IsMandatory = true, IfEmptyValue = "999999990")]
    public virtual required string? BuyerTin { get; set; }

    /// <summary>
    /// Field: C (This field is mandatory)
    /// <para>Buyer's Country</para>
    /// <para>Fill in with the buyer's country code, as the Customers SAFT table.</para>
    /// </summary>
    [JsonPropertyName(nameof(CountryCode))]
    [Field(Position = 2, Fields = ["C"], MaximLength = 12, IsMandatory = true, IfEmptyValue = "Desconhecido")]
    public virtual required Country? CountryCode { get; set; }

    /// <summary>
    /// Field: D (This field is mandatory)
    /// <para>Document type</para>
    /// <para>Fill in according to the type of SAF-T (PT).</para>
    /// </summary>
    [JsonPropertyName(nameof(DocumentType))]
    [Field(Position = 3, Fields = ["D"], MaximLength = 2, IsMandatory = true)]
    public virtual required DocumentType DocumentType { get; set; }

    /// <summary>
    /// Field: E (This field is mandatory)
    /// <para>Document status</para>
    /// <para>Fill in according to the type of SAF-T (PT).</para>
    /// </summary>
    [JsonPropertyName(nameof(DocumentStatus))]
    [Field(Position = 4, Fields = ["E"], MaximLength = 1, IsMandatory = true)]
    public virtual required DocumentStatus DocumentStatus { get; set; }

    /// <summary>
    /// Field: F (This field is mandatory)
    ///  Document date
    /// </summary>
    [JsonPropertyName(nameof(DocumentDate))]
    [Field(Position = 5, Fields = ["F"], MaximLength = 8, IsMandatory = true)]
    public virtual required DateOnly DocumentDate { get; set; }

    /// <summary>
    /// Field: G (This field is mandatory)
    /// <para>Unique identification of the document</para>
    /// <para>Fill in according to the type of SAF-T(PT).</para>
    /// <para>Ex: 'FT A/999'</para>
    /// </summary>
    [JsonPropertyName(nameof(DocumentNumber))]
    [Field(Position = 6, Fields = ["G"], MaximLength = 60, IsMandatory = true)]
    public virtual required string DocumentNumber { get; set; }

    /// <summary>
    /// Field: H (This field is mandatory)<br>
    /// <para>ATCUD</para>
    /// <para>Ex: 'CSDF7T5H-999'</para>
    /// </summary>
    [JsonPropertyName(nameof(ATCUD))]
    [Field(Position = 7, Fields = ["H"], MaximLength = 70, IsMandatory = true, IfEmptyValue = "0")]
    public virtual required string ATCUD { get; set; }

    /// <summary>
    /// List of VAT
    /// Maxim is 3 VAT descriptions
    /// </summary>
    [JsonPropertyName(nameof(Vats))]
    [Field(Position = 8, Fields = ["I1"], MaximLength = 2, IsMandatory = true, IfEmptyValue = "0")]
    public virtual List<Vat> Vats { get; set; } = new();

    /// <summary>
    /// Field: L
    /// <para>Not subject / non-taxable in VAT(IVA)</para>
    /// <para>Total value related to non subject / nontaxable transactions in VAT.</para>
    /// </summary>
    [JsonPropertyName(nameof(TotalNonVat))]
    [Field(Position = 9, Fields = ["L"], MaximLength = 16, IsMandatory = false)]
    public virtual decimal? TotalNonVat { get; set; } = null;

    /// <summary>
    /// Field: M
    /// <para>Stamp tax</para>
    /// <para>Total value of stamp tax</para>
    /// </summary>
    [JsonPropertyName(nameof(TotalStampTax))]
    [Field(Position = 10, Fields = ["M"], MaximLength = 16, IsMandatory = false)]
    public virtual decimal? TotalStampTax { get; set; } = null;

    /// <summary>
    /// Field: N
    /// <para>TaxPayable</para>
    /// <para>Total value of VAT and Stamp Tax -TaxPayable field of SAF - T(PT)</para>
    /// </summary>
    [JsonPropertyName(nameof(TaxPayable))]
    [Field(Position = 11, Fields = ["N"], MaximLength = 16, IsMandatory = true, ZeroIsEmpty = false)]
    public virtual required decimal TaxPayable { get; set; }

    /// <summary>
    /// Field: O
    /// <para>Gross total</para>
    /// <para>Total document value - GrossTotal field of SAF-T (PT).</para>
    /// </summary>
    [JsonPropertyName(nameof(GrossTotal))]
    [Field(Position = 12, Fields = ["O"], MaximLength = 16, IsMandatory = true, ZeroIsEmpty = false)]
    public virtual required decimal? GrossTotal { get; set; }

    /// <summary>
    /// Field: P
    /// <para>Withholding Tax Amount</para>
    /// </summary>
    [JsonPropertyName(nameof(WithholdingTaxAmount))]
    [Field(Position = 13, Fields = ["P"], MaximLength = 16, IsMandatory = false)]
    public virtual decimal? WithholdingTaxAmount { get; set; } = null;

    /// <summary>
    /// Field: Q
    /// <para>4 Hash characters</para>
    /// <para>In case of documents with no digital signature hash the field is filled with 0 (zero)</para>
    /// </summary>
    [JsonPropertyName(nameof(Hash))]
    [Field(Position = 14, Fields = ["Q"], MaximLength = 4, IsMandatory = true)]
    public virtual required string Hash
    {
        get;
        set
        {
            if (value.Length >= 31)
            {
                field = string.Format(
                    "{0}{1}{2}{3}",
                    value.Substring(0, 1),
                    value.Substring(10, 1),
                    value.Substring(20, 1),
                    value.Substring(30, 1)
                );
                return;
            }

            field = value;
        }
    }

    /// <summary>
    /// Field: R
    /// <para>Program certificate number</para>
    /// </summary>
    [JsonPropertyName(nameof(ProgramCertificateNumber))]
    [Field(Position = 15, Fields = ["R"], MaximLength = 4, IsMandatory = true)]
    public virtual required int ProgramCertificateNumber { get; set; }

}

/// <summary>
/// The document information
/// </summary>
public record Document : ADocument
{
    /// <summary>
    /// Field: S
    /// <para>Other info<br>
    /// <para>Field of free filling, in which, for example,
    /// information for payment can be indicated
    /// (ex: from IBAN or Ref MB, with the tab «;»).</para>
    /// <para>This field cannot contain the asterisk character(*) and the character(:) .</para>
    /// <para>All asterisk character(*) and character(:) will be replaced by character(;)</param>
    /// </summary>
    [JsonPropertyName(nameof(OtherInfo))]
    [Field(Position = 16, Fields = ["S"], MaximLength = 65, IsMandatory = false)]
    public virtual string? OtherInfo
    {
        set { field = value?.Replace('*', ';').Replace(':', ';'); }
        get;
    }
}

/// <summary>
/// The document information
/// </summary>
public record Document<T> : ADocument where T : IOtherInfo
{
    /// <summary>
    /// Field: S
    /// <para>Other info<br>
    /// <para>Field of free filling, in which, for example,
    /// information for payment can be indicated
    /// (ex: from IBAN or Ref MB, with the tab «;»).</para>
    /// <para>This field cannot contain the asterisk character(*).</para>
    /// </summary>
    [JsonPropertyName(nameof(OtherInfo))]
    [Field(Position = 16, Fields = ["S"], MaximLength = 65, IsMandatory = false)]
    public virtual T? OtherInfo { set; get; }
}
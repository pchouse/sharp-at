

using System.Text.Json.Serialization;


/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/
namespace PChouse.AT.QRCode.Library;

public class BuilderOptions
{
    /// <summary>
    /// Define if throws exception when is found an field error
    /// </summary>
    [JsonPropertyName(nameof(ThrowExceptionOnError))]
    public virtual bool ThrowExceptionOnError { get; set; } = true;

    /// <summary>
    /// Define if validates the Portuguese TIN
    /// </summary>
    [JsonPropertyName(nameof(ValidateIssuerTin))]
    public virtual required TinValidation ValidateIssuerTin { get; set; }

    /// <summary>
    /// Define if validates the Buyer Portuguese TIN (Only Portuguese)
    /// </summary>
    [JsonPropertyName(nameof(ValidateBuyerPortugueseTin))]
    public virtual required TinValidation ValidateBuyerPortugueseTin { get; set; }

    /// <summary>
    /// Validate the document number
    /// </summary>
    [JsonPropertyName(nameof(ValidateDocumentNumber))]
    public virtual required bool ValidateDocumentNumber { get; set; }
}

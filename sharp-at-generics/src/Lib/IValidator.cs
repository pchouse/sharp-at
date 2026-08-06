/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

namespace PChouse.AT.Generics;

/// <summary>
/// Class with validators
/// </summary>
public interface IValidator
{
    /// <summary>
    /// Validate the document number
    /// </summary>
    /// <param name="documentNumber"></param>
    /// <param name="error">The validation error</param>
    /// <returns></returns>
    public bool DocumentNumber(string documentNumber, out DocumentNumberError? error);

    /// <summary>
    /// Validate if the Portuguese TIN is valid in strict mode
    /// </summary>
    /// <param name="tin">The tax identification number</param>
    /// <returns></returns>
    public bool PortugueseTin(string tin);

    /// <summary>
    /// Validate if the Portuguese TIN is valid in strict mode
    /// </summary>
    /// <param name="tin">The tax identification number</param>
    /// <param name="portugueseTinError">The validation error</param>
    /// <returns></returns>
    public bool PortugueseTin(string tin, out PortugueseTinError? portugueseTinError);

    /// <summary>
    /// Validate if the Portuguese TIN is valid
    /// </summary>
    /// <param name="tin">The tax identification number</param>
    /// <param name="strict">Validates the starting number, start with zero is always validate</param>
    /// <param name="portugueseTinError">The validation error</param>
    /// <returns></returns>
    public bool PortugueseTin(string tin, bool strict, out PortugueseTinError? portugueseTinError);

    /// <summary>
    /// Validate the Portuguese postal code structure
    /// </summary>
    /// <param name="postalCode">The postal code</param>
    /// <returns></returns>
    public bool PortuguesePostalCode(string postalCode);

}
/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;

namespace PChouse.AT.Generics;

/// <summary>
/// Class with validators
/// </summary>
public partial class Validator : IValidator
{
    /// <summary>
    /// Instance logger
    /// </summary>
    protected readonly ILogger Logger;

    /// <summary>
    /// Document number regular expression
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex("^[A-Z]+ [A-Z0-9\\._-]+/[0-9]+$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant)]
    private static partial Regex DocumentNumberRegexp();

    /// <summary>
    /// The Portuguese structure regular expression
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex("^[0-9]+$")]
    private static partial Regex PortugueseTinRegexp();

    [GeneratedRegex("^[0-9]{4}-[0-9]{3}$")]
    private static partial Regex PortuguesePostalCode();

    /// <summary>
    /// Validator instance construct
    /// </summary>
    /// <param name="logFactory">Instance logger</param>
    public Validator(ILogger<Validator> logger)
    {
        Logger = logger;
        Logger.LogTrace($"{nameof(Validator)} instance created");
    }

    /// <summary>
    /// Validate the document number
    /// </summary>
    /// <param name="documentNumber">The document number</param>
    /// <param name="error">The validation error</param>
    /// <returns></returns>
    public virtual bool DocumentNumber(string documentNumber, out DocumentNumberError? error)
    {
        if (documentNumber.Length > 60)
        {
            Logger.LogTrace(
                $"Document number ${documentNumber} is invalid because length is higher than 60 characters, is having ${documentNumber.Length}"
            );

            error = DocumentNumberError.WRONG_LENGTH;

            return false;
        }

        if (!Validator.DocumentNumberRegexp().IsMatch(documentNumber))
        {
            Logger.LogTrace(
                $"Document number ${documentNumber} is not matching RegExp"
            );

            error = DocumentNumberError.REG_EXP_NOT_MATCH;

            return false;
        }

        var spaceIndex = documentNumber.IndexOf(' ');

        var serial = documentNumber.Substring(
            spaceIndex + 1,
            documentNumber.IndexOf('/') - spaceIndex - 1
        );

        if (serial.Trim(['.', '_', '-']).Length != serial.Length)
        {
            Logger.LogTrace(
                $"Document number ${documentNumber} is invalid because serial starts or end with one of '.', '_', '-'"
            );

            error = DocumentNumberError.SERIAL_STARTS_OR_ENDS_WRONG_CHARACTER;

            return false;
        }

        Logger.LogInformation(
                $"Document number ${documentNumber} is valid"
            );

        error = null;

        return true;
    }

    /// <summary>
    /// Validate if the Portuguese TIN is valid in strict mode
    /// </summary>
    /// <param name="tin">The tax identification number</param>
    /// <returns></returns>
    public virtual bool PortugueseTin(string tin)
    {
        return PortugueseTin(tin, true, out var portugueseTinError);
    }

    /// <summary>
    /// Validate if the Portuguese TIN is valid in strict mode
    /// </summary>
    /// <param name="tin">The tax identification number</param>
    /// <param name="portugueseTinError">The validation error</param>
    /// <returns></returns>
    public virtual bool PortugueseTin(string tin, out PortugueseTinError? portugueseTinError)
    {
        var result = PortugueseTin(tin, true, out var error);
        portugueseTinError = error;
        return result;
    }

    /// <summary>
    /// Validate if the Portuguese TIN is valid
    /// </summary>
    /// <param name="tin">The tax identification number</param>
    /// <param name="strict">Validates the starting number, start with zero is always validate</param>
    /// <param name="error">The validation error</param>
    /// <returns></returns>
    public virtual bool PortugueseTin(string tin, bool strict, out PortugueseTinError? error)
    {
        if (!Validator.PortugueseTinRegexp().IsMatch(tin))
        {
            Logger.LogTrace(
                $"The Portuguese tin ${tin} is invalid because is not only numbers"
            );

            error = PortugueseTinError.IS_NOT_ONLY_NUMBERS;

            return false;
        }

        if (tin.Length != 9)
        {
            Logger.LogTrace(
                $"The Portuguese tin ${tin} is invalid because length is not 9"
            );

            error = PortugueseTinError.WRONG_LENGTH;

            return false;
        }

        if (tin.StartsWith('0'))
        {
            Logger.LogTrace(
                $"The Portuguese tin ${tin} is invalid because starts with '0'"
            );

            error = PortugueseTinError.TIN_STARTS_WITH_ZERO;

            return false;
        }

        if (strict)
        {
            var one = new[] { "1", "2", "3", "5", "6", "8" };
            var two = new[] { "45", "70", "71", "72", "74", "75", "77", "79", "90", "91", "98", "99" };

            if (!one.Contains(tin.Substring(0, 1)) && !two.Contains(tin.Substring(0, 2)))
            {
                Logger.LogTrace(
                    $"The Portuguese tin ${tin} is invalid because starts with '{tin.Substring(0, 1)}' and should start with on of: '{string.Join("', '", one)}', '{string.Join("', '", two)}'"
                );

                error = PortugueseTinError.WRONG_START_DIGITS;

                return false;
            }
        }


        int checkSum = 0;

        for (int i = 0; i < 8; i++)
        {
            checkSum += int.Parse(tin[i].ToString()) * (9 - i);
        }

        int checkDigitCalculated = 11 - (checkSum % 11);

        if (checkDigitCalculated > 9) checkDigitCalculated = 0;

        int checkDigit = int.Parse(tin[8].ToString());

        if (checkDigit != checkDigitCalculated)
        {
            Logger.LogTrace(
                $"Portuguese tin {tin} is invalid, tin check digit '{checkDigit}' calculated '{checkDigitCalculated}'"
            );

            error = PortugueseTinError.CONTROL_DIGIT_NOT_MATCH;

            return false;
        }

        Logger.LogInformation(
                $"Portuguese tin ${tin} is valid, test strict '{strict}'"
            );

        error = null;

        return true;
    }

    /// <summary>
    /// Validate the Portuguese postal code structure
    /// </summary>
    /// <param name="postalCode">The postal code</param>
    /// <returns></returns>
    public virtual bool PortuguesePostalCode(string postalCode)
    {
        if (!PortuguesePostalCode().IsMatch(postalCode))
        {
            Logger.LogInformation(
                $"Portuguese postal code {postalCode} structure is invalid"
            );

            return false;
        }

        Logger.LogInformation(
                $"Portuguese postal code {postalCode} structure is valid"
            );

        return true;
    }

}
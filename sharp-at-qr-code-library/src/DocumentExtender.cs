/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.RegularExpressions;
using PChouse.AT.Generics;

namespace PChouse.AT.QRCode.Library;

/// <summary>
/// Try to parse to a Document with an IOtherInfo as generic type 
/// </summary>
public static partial class DocumentExtender
{
    /// <summary>
    /// Regexp to check Bank transfer
    /// </summary>
    [GeneratedRegex(@"^TB;([0-9A-Z]{10,})(;([0-9]{1,}(\.[0-9]{1,})?))?(;([A-Z]{3}))?$", RegexOptions.IgnoreCase)]
    public static partial Regex BankTransferRegexp { get; }

    /// <summary>
    /// Regexp to check Multi Bank
    /// </summary>
    [GeneratedRegex(@"^MB;([0-9]{5});([0-9]{9});([0-9]{1,}\.[0-9]{2});?$", RegexOptions.IgnoreCase)]
    public static partial Regex MultiBankRegexp { get; }

    /// <summary>
    /// Regexp to check Payment info
    /// </summary>
    [GeneratedRegex(@"^([A-Z]{2});([0-9]{1,}(\.[0-9]{1,})?)(;([A-Z]{3}))?$", RegexOptions.IgnoreCase)]
    public static partial Regex PaymentInfoRegexp { get; }

    /// <summary>
    /// Convert all the document to the
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="document"></param>
    /// <returns></returns>
    public static T ConvertBaseProperties<T>(ADocument document) where T : ADocument
    {

        var clone = Activator.CreateInstance<T>();

        var properties = typeof(Document<IOtherInfo>).GetProperties()
                                .Where(p => p.Name != nameof(Document<>.OtherInfo))
                                .ToList();

        foreach (var property in properties)
        {
            property.SetValue(clone, property.GetValue(document));
        }

        return clone;
    }

    /// <summary>
    /// Covert The generic document<IOtherInfo> class to the base class Document 
    /// </summary>
    /// <param name="document"></param>
    /// <returns></returns>
    public static Document ConvertToBaseClass(this Document<IOtherInfo> document)
    {
        var clone = ConvertBaseProperties<Document>(document);
        clone.OtherInfo = document.OtherInfo?.Info();
        return clone;
    }

    /// <summary>
    /// Convert base Document into Document<IOtherInfo>
    /// </summary>
    /// <param name="document"></param>
    /// <param name="otherInfo"></param>
    /// <returns>The new Instance</returns>
    /// <exception cref="Exception"></exception>
    public static Document<IOtherInfo> Convert(this Document document, IOtherInfo otherInfo)
    {

        if (document.GetType().GetGenericArguments().Length > 0)
        {
            throw new Exception("Instance of document cannot be generics");
        }

        var clone = ConvertBaseProperties<Document<IOtherInfo>>(document);

        clone.OtherInfo = otherInfo;

        return clone;
    }

    /// <summary>
    /// Try to parse Document into Document<IOtherInfo>
    /// </summary>
    /// <param name="document">The instance to be converted</param>
    /// <param name="result">The result if parsed or null if fails</param>
    /// <returns>false if fails</returns>
    public static bool TryParse(this Document document, [NotNullWhen(true)] out Document<IOtherInfo>? result)
    {

        if (string.IsNullOrEmpty(document.OtherInfo))
        {
            result = document.Convert(new OtherInfo { Information = document.OtherInfo ?? "" });
            return true;
        }

        var otherInfoRaw = document.OtherInfo.Trim().Trim(';').Replace(" ", "");

        // Multi bank payment parser

        var multiBankMatch = MultiBankRegexp.Match(otherInfoRaw);

        if (multiBankMatch.Success)
        {
            var multiBankPayment = new MultiBankPayment
            {
                Entity = multiBankMatch.Groups[1].Value,
                Reference = multiBankMatch.Groups[2].Value,
                Amount = decimal.Parse(multiBankMatch.Groups[3].Value)
            };

            result = document.Convert(multiBankPayment);
            return true;
        }

        // Bank transfer parse 

        var bankTransferMatch = BankTransferRegexp.Match(otherInfoRaw);

        if (bankTransferMatch.Success)
        {
            var parsedCurrency = Enum.TryParse<CurrencyCode>(bankTransferMatch.Groups[6].Value, true, out var resultCurrency);
            var amountRaw = bankTransferMatch.Groups[3].Value;

            var bankTransfer = new BankTransfer
            {
                Iban = bankTransferMatch.Groups[1].Value,
                Amount = string.IsNullOrWhiteSpace(amountRaw) ? null : decimal.Parse(amountRaw),
                CurrencyCode = parsedCurrency ? resultCurrency : null
            };

            result = document.Convert(bankTransfer);
            return true;
        }

        // Payment info parse

        var paymentInfoMatch = PaymentInfoRegexp.Match(otherInfoRaw);

        if (paymentInfoMatch.Success)
        {
            if (Enum.TryParse<PaymentMechanism>(paymentInfoMatch.Groups[1].Value, true, out var paymentMechanism))
            {
                var currencyParsed = Enum.TryParse<CurrencyCode>(paymentInfoMatch.Groups[5].Value, true, out var resultCurrency);
                var amountRaw = paymentInfoMatch.Groups[2].Value;

                var paymentInfo = new PaymentInfo
                {
                    PaymentMechanism = paymentMechanism,
                    Amount = decimal.Parse(amountRaw),
                    CurrencyCode = currencyParsed ? resultCurrency : null
                };

                result = document.Convert(paymentInfo);
                return true;
            }
        }

        result = document.Convert(new OtherInfo { Information = document.OtherInfo });
        return true;
    }

}
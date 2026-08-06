
using PChouse.AT.Generics;

namespace PChouse.AT.QRCode.Library.Test;

public class DataProvider
{

    public static Document document = new Document
    {
        IssuerTin = "555555550",
        BuyerTin = "999999990",
        CountryCode = Generics.Country.PT,
        DocumentType = DocumentType.FT,
        DocumentStatus = DocumentStatus.N,
        DocumentDate = new DateOnly(),
        DocumentNumber = "FT FT/999",
        ATCUD = "0",
        TotalNonVat = 0m,
        TotalStampTax = 0m,
        TaxPayable = 99.99m,
        GrossTotal = 999.99m,
        WithholdingTaxAmount = 9.99m,
        Hash = "0000",
        ProgramCertificateNumber = 9999,
        OtherInfo = null
    };

    public static IEnumerable<(Document, IOtherInfo)> ConvertData()
    {
        yield return (document, new BankTransfer
        {
            Amount = 9.99m,
            Iban = "PT50999999999",
            CurrencyCode = null
        });

        yield return (document, new BankTransfer
        {
            Amount = 9.99m,
            Iban = "PT50999999999",
            CurrencyCode = Generics.CurrencyCode.KRW
        });

        yield return (document, new MultiBankPayment
        {
            Entity = "12345",
            Reference = "987554321",
            Amount = 9.99m
        });

        yield return (document, new PaymentInfo
        {
            PaymentMechanism = Generics.PaymentMechanism.CC,
            Amount = 999.99m,
            CurrencyCode = Generics.CurrencyCode.KRW
        });

        yield return (document, new PaymentInfo
        {
            PaymentMechanism = Generics.PaymentMechanism.CC,
            Amount = 999.99m,
            CurrencyCode = null
        });

        yield return (document, new OtherInfo
        {
            Information = "My Info"
        });

    }

    public static IEnumerable<(Document, IOtherInfo)> TryParseData()
    {
        // Bank transfer
        foreach (var amount in new decimal?[] { null, 9.99m })
        {
            foreach (var currency in new CurrencyCode?[] { null, CurrencyCode.KRW })
            {
                var bankTransfer = new BankTransfer
                {
                    Iban = "PT50002541303980721479744",
                    Amount = amount,
                    CurrencyCode = currency,
                };

                yield return (
                    document with { OtherInfo = bankTransfer.Info() },
                    bankTransfer
                );
            }
        }

        yield return (
                    document with { OtherInfo = "TB;PT50002541303980721479744;9.99;EUR" },
                    new BankTransfer
                    {
                        Iban = "PT50002541303980721479744",
                        Amount = 9.99m,
                        CurrencyCode = null
                    }
                );


        // MultiBank 

        foreach (var amount in new[] { 9m, 9.9m, 199.99m })
        {
            var multiBank = new MultiBankPayment
            {
                Entity = "12345",
                Reference = "987554321",
                Amount = amount
            };

            yield return (
                document with { OtherInfo = multiBank.Info() },
                multiBank
            );
        }

        // Payment info
        foreach (var paymentMechanism in typeof(PaymentMechanism).GetEnumValues())
        {
            foreach (var currencyCode in new CurrencyCode?[] { null, CurrencyCode.KRW })
            {
                foreach (var amount in new[] { 9m, 9.9m, 199.99m })
                {
                    var paymentInfo = new PaymentInfo
                    {
                        PaymentMechanism = (PaymentMechanism)paymentMechanism,
                        Amount = amount,
                        CurrencyCode = currencyCode
                    };

                    yield return (
                        document with { OtherInfo = paymentInfo.Info() },
                        paymentInfo
                    );
                }
            }
        }

        // OtherInfo parse
        yield return (
            document with { OtherInfo = "My info" },
            new OtherInfo { Information = "My info" }
        );
    }


    public static IEnumerable<Document<IOtherInfo>> DocumentGenericOtherInfo()
    {

        yield return document.Convert(new BankTransfer
        {
            Iban = "PT50019116154222415475790",
            Amount = 999.99m,
            CurrencyCode = null
        });

        yield return document.Convert(new MultiBankPayment
        {
            Entity = "75790",
            Reference = "999999999",
            Amount = 999.99m,
        });

        yield return document.Convert(new PaymentInfo
        {
            PaymentMechanism = PaymentMechanism.CH,
            Amount = 999.99m,
            CurrencyCode = null
        });

        yield return document.Convert(new OtherInfo
        {
            Information = "My information"
        });

    }

}

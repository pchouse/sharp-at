
namespace PChouse.AT.QRCode.Library.Test;

[TestClass]
public sealed class DocumentExtenderTest
{

    [TestMethod]
    [DynamicData(nameof(DataProvider.ConvertData), typeof(DataProvider))]
    public void TestConvert(Document document, IOtherInfo otherInfo)
    {

        var clone = document.Convert(otherInfo);

        Assert.AreEqual(document.IssuerTin, clone.IssuerTin);

        Assert.AreEqual(document.BuyerTin, clone.BuyerTin);
        Assert.AreEqual(document.CountryCode, clone.CountryCode);
        Assert.AreEqual(document.DocumentType, clone.DocumentType);
        Assert.AreEqual(document.DocumentStatus, clone.DocumentStatus);
        Assert.AreEqual(document.DocumentDate, clone.DocumentDate);
        Assert.AreEqual(document.DocumentNumber, clone.DocumentNumber);
        Assert.AreEqual(document.ATCUD, clone.ATCUD);
        Assert.AreEqual(document.TotalNonVat, clone.TotalNonVat);
        Assert.AreEqual(document.TotalStampTax, clone.TotalStampTax);
        Assert.AreEqual(document.TaxPayable, clone.TaxPayable);
        Assert.AreEqual(document.GrossTotal, clone.GrossTotal);
        Assert.AreEqual(document.WithholdingTaxAmount, clone.WithholdingTaxAmount);
        Assert.AreEqual(document.Hash, clone.Hash = "0000");
        Assert.AreEqual(document.ProgramCertificateNumber, clone.ProgramCertificateNumber);

        if ((otherInfo is BankTransfer expectedBankTransfer) && (clone.OtherInfo is BankTransfer cloneBankTransfer))
        {
            Assert.AreEqual(expectedBankTransfer.Iban, cloneBankTransfer.Iban);
        }
        else if ((otherInfo is MultiBankPayment expectedMultiBank) && (clone.OtherInfo is MultiBankPayment cloneMultiBank))
        {
            Assert.AreEqual(expectedMultiBank.Reference, cloneMultiBank.Reference);
        }
        else if ((otherInfo is PaymentInfo expectedPaymentInfo) && (clone.OtherInfo is PaymentInfo clonePaymentInfo))
        {
            Assert.AreEqual(expectedPaymentInfo.PaymentMechanism, clonePaymentInfo.PaymentMechanism);
        }
        else if ((otherInfo is OtherInfo expectedOtherInfo) && (clone.OtherInfo is OtherInfo cloneOtherInfo))
        {
            Assert.AreEqual(expectedOtherInfo.Information, cloneOtherInfo.Information);
        }
        else
        {
            Assert.Fail("Other info is not a instance off Bank Transfer");
        }
    }

    [TestMethod]
    [DynamicData(nameof(DataProvider.TryParseData), typeof(DataProvider))]
    public void TestTryParse(Document document, IOtherInfo otherInfo)
    {
        var parsed = document.TryParse(out var result);

        Assert.IsTrue(parsed);
        Assert.AreEqual(result?.OtherInfo?.GetType(), otherInfo.GetType());

        var parsedOtherType = result?.OtherInfo;

        if ((otherInfo is BankTransfer expectedBankTransfer) && parsedOtherType is BankTransfer bankTransfer)
        {
            Assert.AreEqual(expectedBankTransfer.Iban, bankTransfer.Iban);
            Assert.AreEqual(expectedBankTransfer.Amount, bankTransfer.Amount);
            Assert.AreEqual(expectedBankTransfer.CurrencyCode, bankTransfer.CurrencyCode);
        }
        else if ((otherInfo is MultiBankPayment expectedMultiBank) && parsedOtherType is MultiBankPayment multiBankPayment)
        {
            Assert.AreEqual(expectedMultiBank.Entity, multiBankPayment.Entity);
            Assert.AreEqual(expectedMultiBank.Reference, multiBankPayment.Reference);
            Assert.AreEqual(expectedMultiBank.Amount, multiBankPayment.Amount);
        }
        else if ((otherInfo is PaymentInfo expectedPaymentInfo) && parsedOtherType is PaymentInfo paymentInfo)
        {
            Assert.AreEqual(expectedPaymentInfo.PaymentMechanism, paymentInfo.PaymentMechanism);
            Assert.AreEqual(expectedPaymentInfo.Amount, paymentInfo.Amount);
            Assert.AreEqual(expectedPaymentInfo.CurrencyCode, paymentInfo.CurrencyCode);
        }
        else if ((otherInfo is OtherInfo expectedOtherInfo) && parsedOtherType is OtherInfo resultOtherInfo)
        {
            Assert.AreEqual(expectedOtherInfo.Information, resultOtherInfo.Information);
        }
        else
        {
            Assert.Fail($"Fail to parse type {otherInfo.GetType().Name}");
        }
    }

    [TestMethod]
    [DynamicData(nameof(DataProvider.DocumentGenericOtherInfo), typeof(DataProvider))]
    public void TestConvertToBaseClass(Document<IOtherInfo> document)
    {
        var clone = document.ConvertToBaseClass();

        Assert.AreEqual(clone.OtherInfo, document.OtherInfo?.Info());
    }

}
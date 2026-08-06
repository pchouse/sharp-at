using PChouse.AT.Generics;

namespace PChouse.AT.QRCode.Library.Test;

[TestClass]
public sealed class BankTransferTest
{
    [TestMethod]
    [DataRow(null, null)]
    [DataRow(null, "999.99")]
    [DataRow(CurrencyCode.AED, null)]
    [DataRow(CurrencyCode.AED, "999.99")]
    public void TestEmptyIban(CurrencyCode? currencyCode, string? amount)
    {
        var bankTransfer = new BankTransfer
        {
            Iban = "",
            CurrencyCode = currencyCode,
            Amount = amount is null ? null : decimal.Parse(amount)
        };

        Assert.IsNull(bankTransfer.Info());
    }

    [TestMethod]
    public void TestIban()
    {
        var iban = "PT509999999999";

        var bankTransfer = new BankTransfer
        {
            Iban = iban,
            CurrencyCode = null,
            Amount = null
        };

        Assert.AreEqual($"TB;{iban}", bankTransfer.Info());
    }

    [TestMethod]
    [DataRow("9.99", "9.99")]
    [DataRow("9.985", "9.99")]
    [DataRow("0", "0.00")]
    public void TestIbanAmount(string amount, string expectedAmount)
    {
        var iban = "PT509999999999";

        var bankTransfer = new BankTransfer
        {
            Iban = iban,
            CurrencyCode = null,
            Amount = decimal.Parse(amount)
        };

        Assert.AreEqual($"TB;{iban};{expectedAmount}", bankTransfer.Info());
    }

    [TestMethod]
    public void TestCurrency()
    {
        var iban = "PT509999999999";

        var bankTransfer = new BankTransfer
        {
            Iban = iban,
            CurrencyCode = CurrencyCode.KRW,
            Amount = null
        };

        Assert.AreEqual($"TB;{iban};KRW", bankTransfer.Info());
    }

    [TestMethod]
    public void TestAmountCurrency()
    {
        var iban = "PT509999999999";

        var bankTransfer = new BankTransfer
        {
            Iban = iban,
            CurrencyCode = CurrencyCode.KRW,
            Amount = 9.99m
        };

        Assert.AreEqual($"TB;{iban};9.99;KRW", bankTransfer.Info());
    }

}
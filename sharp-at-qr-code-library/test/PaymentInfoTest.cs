using PChouse.AT.Generics;

namespace PChouse.AT.QRCode.Library.Test;

[TestClass]
public sealed class PaymentInfoTest
{
    
    [TestMethod]
    [DataRow("9.99", "9.99")]
    [DataRow("9.985", "9.99")]
    public void TestEmptyCurrencyCode(string amount, string expectedAmount)
    {
        var paymentInfo = new PaymentInfo
        {
            PaymentMechanism = PaymentMechanism.CC,
            Amount = decimal.Parse(amount),
            CurrencyCode = null,
        };

        Assert.AreEqual($"CC;{expectedAmount}", paymentInfo.Info());
    }

    [TestMethod]
    [DataRow("9.99", "9.99")]
    [DataRow("9.985", "9.985")]
    public void Test(string amount, string expectedAmount)
    {
        var paymentInfo = new PaymentInfo
        {
            PaymentMechanism = PaymentMechanism.CC,
            Amount = decimal.Parse(amount),
            CurrencyCode = CurrencyCode.KWD,
        };

        Assert.AreEqual($"CC;{expectedAmount};KWD", paymentInfo.Info());
    }
}
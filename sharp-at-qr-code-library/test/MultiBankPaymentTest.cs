
namespace PChouse.AT.QRCode.Library.Test;

[TestClass]
public sealed class MultiBankPaymentTest
{
    [TestMethod]
    public void Test()
    {
        var multiBank = new MultiBankPayment
        {
              Entity = "12345",
              Reference = "123456789",
              Amount = 9.99m
        };

        Assert.AreEqual($"MB;12345;123456789;9.99", multiBank.Info());
    }   
}

namespace PChouse.AT.QRCode.Library.Test;

[TestClass]
public sealed class OtherInfoTest
{

    [TestMethod]
    public void TestEmpty()
    {
        var otherInfo = new OtherInfo { Information = "" };

        Assert.IsNull(otherInfo.Info());
    }

    [TestMethod]
    public void Test()
    {
        var otherInfo = new OtherInfo { Information = "The information" };

        Assert.AreEqual(otherInfo.Information, otherInfo.Info());
    }

}
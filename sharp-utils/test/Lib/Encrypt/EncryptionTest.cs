
using PChouse.Utils.Encrypt;

namespace PChouse.Utils.Test.Encrypt;

[TestClass]
public sealed class EncryptionTest
{

    [TestMethod]
    [DataRow(0)]
    [DataRow(1)]
    [DataRow(31)]
    [DataRow(33)]
    [DataRow(64)]
    [DataRow(128)]
    [DataRow(256)]
    public void TestWrongKeyLength(int keyLength)
    {
        Assert.ThrowsExactly<EncryptException>(() =>
        {
            var keys = new EncryptKey
            {
                Key = "".PadLeft(keyLength, '9'),
                Iv = "".PadLeft(16, '9')
            };

            _ = new Encryption(keys);
        });
    }


    [TestMethod]
    [DataRow(0)]
    [DataRow(1)]
    [DataRow(15)]
    [DataRow(17)]
    [DataRow(32)]
    [DataRow(64)]
    [DataRow(128)]
    public void TestWrongIvLength(int ivLength)
    {
        Assert.ThrowsExactly<EncryptException>(() =>
        {
            var keys = new EncryptKey
            {
                Key = "".PadLeft(32, '9'),
                Iv = "".PadLeft(ivLength, '9')
            };

            _ = new Encryption(keys);
        });
    }

    [TestMethod]
    public void TestEmptyLIteralToEncrypt()
    {
        var ex = Assert.Throws<Exception>(() =>
        {
            var keys = new EncryptKey
            {
                Key = "".PadLeft(32, '9'),
                Iv = "".PadLeft(16, '9')
            };

            var encrypt = new Encryption(keys);
            _ = encrypt.EncryptAsync("").Result;
        });

        Assert.IsTrue(ex.InnerException is EncryptException);
    }


    [TestMethod]
    public void TestEncryptAndDecrypt()
    {
        var keys = new EncryptKey
        {
            Key = "".PadLeft(32, '9'),
            Iv = "".PadLeft(16, '9')
        };

        var literal = "A999";
        var encrypt = new Encryption(keys);

        var encrypted = encrypt.EncryptAsync(literal).Result;

        Assert.IsNotNull(encrypted);
        Assert.IsNotEmpty(encrypted);
        Assert.AreNotEqual(literal, encrypted);

        Assert.AreEqual(literal, encrypt.DecryptAsync(encrypted).Result);
    }

    [TestMethod]
    public void TestSha256Hash()
    {
        var keys = new EncryptKey
        {
            Key = "".PadLeft(32, '9'),
            Iv = "".PadLeft(16, '9')
        };

        var encrypt = new Encryption(keys);

        var hash = encrypt.Sha256HashAsync("A999", "B999").Result;

        Assert.IsNotNull(hash);
        Assert.IsNotEmpty(hash);
        Assert.DoesNotContain("A999", hash);
        Assert.DoesNotContain("B999", hash);
    }

}


namespace PChouse.AT.Generics.Test;

[TestClass]
public class EnumParseTest
{
    [TestMethod]
    [DataRow(typeof(Country), "PT", false, Country.PT)]
    [DataRow(typeof(Country), "pt", true, Country.PT)]
    [DataRow(typeof(Country), "Desconhecido", false, Country.Unknown)]
    [DataRow(typeof(Country), "desconhecido", true, Country.Unknown)]
    public void Test(Type type, string value, bool ignoreCase, Enum expectedResult)
    {

        var parser = new EnumParse();

        {
            if (parser.TryFirstFromAttributes(type, value, ignoreCase, out var parseResult))
            {
                Assert.AreEqual(expectedResult, parseResult);
            }
            else
            {
                Assert.Fail("Fail to parse enum");
            }
        }

        {
            if (parser.TryFirstFromAttributes(type, value, ignoreCase, out var parseResult))
            {
                Assert.AreEqual(expectedResult, parseResult);
            }
            else
            {
                Assert.Fail("Fail to parse enum");
            }
        }

    }

    [TestMethod]
    [DataRow(typeof(Country), "Kr")]
    [DataRow(typeof(Country), "kR")]
    [DataRow(typeof(Country), "kr")]
    [DataRow(typeof(Country), "desconhecido")]
    public void TestFailCase(Type type, string value)
    {

        var parser = new EnumParse();

        {
            Assert.IsFalse(parser.TryFirstFromAttributes(type, value, false, out var parseResult));
            Assert.IsNull(parseResult);
        }

        {
            Assert.IsFalse(parser.TryFirstFromAttributes<Country>(value, false, out var parseResult));
            Assert.IsNull(parseResult);
        }
    }

    [TestMethod]
    [DataRow(typeof(Country), "Kr", Country.KR)]
    [DataRow(typeof(Country), "kR", Country.KR)]
    [DataRow(typeof(Country), "kr", Country.KR)]
    [DataRow(typeof(Country), "desconhecido", Country.Unknown)]
    public void TestFailDefaultIgnoreCase(Type type, string value, Country expected)
    {

        var parser = new EnumParse();

        {
            Assert.IsTrue(parser.TryFirstFromAttributes(type, value, out var parseResult));
            Assert.AreEqual(expected, parseResult);
        }

        {
            Assert.IsTrue(parser.TryFirstFromAttributes<Country>(value, out var parseResult));
            Assert.AreEqual(expected, parseResult);
        }
    }

}
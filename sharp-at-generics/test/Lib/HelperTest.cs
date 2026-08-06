
using System.Globalization;
using Microsoft.Extensions.Logging.Abstractions;

namespace PChouse.AT.Generics;

[TestClass]
public class HelperTest
{
    [TestMethod]
    [DataRow(1, "1969-10-05", 10)]
    [DataRow(1, "2020-01-01", 1)]
    [DataRow(1, "2020-01-31", 1)]
    [DataRow(1, "2020-12-01", 12)]
    [DataRow(1, "2020-12-31", 12)]
    [DataRow(1, "2020-09-01", 9)]
    [DataRow(1, "2020-09-30", 9)]
    [DataRow(10, "2020-10-01", 1)]
    [DataRow(10, "2020-10-31", 1)]
    [DataRow(10, "2020-12-01", 3)]
    [DataRow(10, "2020-12-31", 3)]
    [DataRow(10, "2020-01-01", 4)]
    [DataRow(10, "2020-01-31", 4)]
    [DataRow(12, "2020-01-01", 2)]
    [DataRow(12, "2020-01-31", 2)]
    [DataRow(12, "2020-12-01", 1)]
    [DataRow(12, "2020-12-31", 1)]
    public void TestDocumentPeriod(int startMonth, string date, int result)
    {
        var helper = new Helper(NullLogger<Helper>.Instance);
        var documentDate = DateOnly.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture);

        Assert.AreEqual(
            result,
            helper.DocumentPeriod(startMonth, documentDate),
            $"Start month: {startMonth}; Document date: {date};"
        );
    }
}
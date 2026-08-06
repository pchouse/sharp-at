using Microsoft.Extensions.Logging.Abstractions;
using PChouse.AT.Generics;
using PChouse.AT.QRCode.Library;

namespace PChouse.AT.QRCode.Test;

[TestClass]
public sealed class ParserTest
{
    [TestMethod]
    [DynamicData(nameof(DataProvider.BuilderStringData), typeof(DataProvider))]
    public void TestParseString(ADocument document, string expectedString)
    {
        var parser = new Parser(NullLogger<Parser>.Instance, new EnumParse());

        var documentParsed = parser.Parse(expectedString);

        Assert.AreEqual(document.IssuerTin, documentParsed.IssuerTin);
        Assert.AreEqual(document.BuyerTin ?? "999999990", documentParsed.BuyerTin);
        Assert.AreEqual(document.CountryCode ?? Country.Unknown, documentParsed.CountryCode);
        Assert.AreEqual(document.DocumentType, documentParsed.DocumentType);
        Assert.AreEqual(document.DocumentStatus, documentParsed.DocumentStatus);
        Assert.AreEqual(document.DocumentDate, documentParsed.DocumentDate);
        Assert.AreEqual(document.DocumentNumber, documentParsed.DocumentNumber);
        Assert.AreEqual(document.ATCUD, documentParsed.ATCUD);
        Assert.AreEqual(document.TotalNonVat, documentParsed.TotalNonVat);
        Assert.AreEqual(document.TotalStampTax, documentParsed.TotalStampTax);
        Assert.AreEqual(document.TaxPayable, documentParsed.TaxPayable);
        Assert.AreEqual(document.GrossTotal, documentParsed.GrossTotal);
        Assert.AreEqual(document.WithholdingTaxAmount, documentParsed.WithholdingTaxAmount);
        Assert.AreEqual(document.Hash, documentParsed.Hash);
        Assert.AreEqual(document.ProgramCertificateNumber, documentParsed.ProgramCertificateNumber);


        var otherInfoValue = document.GetType().GetProperties()
                    .First(p => p.Name == nameof(Document.OtherInfo))
                    .GetValue(document);

        if (otherInfoValue is IOtherInfo @otherInfo)
        {
            Assert.AreEqual(@otherInfo.Info(), documentParsed.OtherInfo);
        }
        else if (otherInfoValue is string @string)
        {
            Assert.AreEqual(@string, documentParsed.OtherInfo);
        }
        else
        {
            Assert.IsNull(documentParsed.OtherInfo);
        }

        Assert.AreEqual(document.Vats.Count(), documentParsed.Vats.Count());

        for (var n = 0; n < documentParsed.Vats.Count(); n++)
        {
            Assert.AreEqual(document.Vats[n].TaxCountryRegion, documentParsed.Vats[n].TaxCountryRegion);
            Assert.AreEqual(document.Vats[n].ExemptedBaseVat, documentParsed.Vats[n].ExemptedBaseVat);
            Assert.AreEqual(document.Vats[n].ReducedBaseVat, documentParsed.Vats[n].ReducedBaseVat);
            Assert.AreEqual(document.Vats[n].ReducedTotalVat, documentParsed.Vats[n].ReducedTotalVat);
            Assert.AreEqual(document.Vats[n].IntermediateBaseVat, documentParsed.Vats[n].IntermediateBaseVat);
            Assert.AreEqual(document.Vats[n].IntermediateTotalVat, documentParsed.Vats[n].IntermediateTotalVat);
            Assert.AreEqual(document.Vats[n].NormalBaseVat, documentParsed.Vats[n].NormalBaseVat);
            Assert.AreEqual(document.Vats[n].NormalTotalVat, documentParsed.Vats[n].NormalTotalVat);
        }
    }

}

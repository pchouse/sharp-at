
using System.Text;
using System.Text.Json;

namespace PChouse.AT.QRCode.Library.Test;

[TestClass]
public sealed class DocumentTest
{

    [TestMethod]
    public void TestInstanceBase()
    {
        var document = new Document
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

        Assert.IsNull(document.OtherInfo);
        Assert.AreEqual("My info", (document with { OtherInfo = "My info" }).OtherInfo);
        Assert.AreEqual("My info;2", (document with { OtherInfo = "My info*2" }).OtherInfo);
        Assert.AreEqual("My info;3", (document with { OtherInfo = "My info:3" }).OtherInfo);

        Assert.AreEqual("0000", document.Hash, "Set hash with length of 4 fail");
        Assert.AreEqual("999999999", (document with { Hash = "999999999" }).Hash);

        var hashBuilder = new StringBuilder();

        for (var n = 1; n <= 31; n++)
        {
            if (n == 1)
            {
                hashBuilder.Append('A');
            }
            else if (n == 11)
            {
                hashBuilder.Append('B');
            }
            else if (n == 21)
            {
                hashBuilder.Append('C');
            }
            else if (n == 31)
            {
                hashBuilder.Append('D');
            }
            else if (n < 10)
            {
                hashBuilder.Append(n);
            }
            else
            {
                hashBuilder.Append('X');
            }
        }

        Assert.AreEqual(
            "ABCD",
            (document with { Hash = hashBuilder.ToString() }).Hash,
            "Set hash with length higher or equal to 31 fail"
        );

        Assert.AreEqual(
            "AAAA",
            (document with { OtherInfo = "AAAA" }).OtherInfo,
            "Other info test fail"
        );
    }


    [TestMethod]
    public void TestInstanceGeneric()
    {
        var document = new Document<BankTransfer>
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

        Assert.AreEqual("0000", document.Hash, "Set hash with length of 4 fail");
        Assert.AreEqual("999999999", (document with { Hash = "999999999" }).Hash);

        var hashBuilder = new StringBuilder();

        for (var n = 1; n <= 31; n++)
        {
            if (n == 1)
            {
                hashBuilder.Append('A');
            }
            else if (n == 11)
            {
                hashBuilder.Append('B');
            }
            else if (n == 21)
            {
                hashBuilder.Append('C');
            }
            else if (n == 31)
            {
                hashBuilder.Append('D');
            }
            else if (n < 10)
            {
                hashBuilder.Append(n);
            }
            else
            {
                hashBuilder.Append('X');
            }
        }

        Assert.AreEqual(
            "ABCD",
            (document with { Hash = hashBuilder.ToString() }).Hash,
            "Set hash with length higher or equal to 31 fail"
        );

        Assert.AreEqual(
           "TB;PT0509999999999;99.99",
           (document with
           {
               OtherInfo = new BankTransfer { Iban = "PT0509999999999", Amount = 99.99m, CurrencyCode = null }
           }).OtherInfo.Info()
        );

        Assert.AreEqual(
          "TB;PT0509999999999;99.99",
          (document with
          {
              OtherInfo = new BankTransfer { Iban = "PT0509999999999", Amount = 99.994m, CurrencyCode = null }
          }).OtherInfo.Info()
       );

        Assert.AreEqual(
          "TB;PT0509999999999;99.994;AED",
          (document with
          {
              OtherInfo = new BankTransfer { Iban = "PT0509999999999", Amount = 99.994m, CurrencyCode = Generics.CurrencyCode.AED }
          }).OtherInfo.Info()
       );

        var documentMultiBank = new Document<MultiBankPayment>
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
            OtherInfo = new MultiBankPayment { Entity = "12345", Reference = "987754321", Amount = 99.99m }
        };

        Assert.AreEqual("MB;12345;987754321;99.99", documentMultiBank.OtherInfo?.Info());
    }

    [TestMethod]
    [DataRow("")]
    [DataRow(null)]
    [DataRow("A")]
    public void TestBaseSerialization(string? otherInfo)
    {
        var document = new Document
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
            OtherInfo = otherInfo
        };

        var json = JsonSerializer.Serialize<Document>(document);

        Assert.IsNotEmpty(json);

        var jsonDeserialized = JsonSerializer.Deserialize<Document>(json);

        Assert.IsNotNull(jsonDeserialized);

        Assert.AreEqual(document.IssuerTin, jsonDeserialized.IssuerTin);
        Assert.AreEqual(document.OtherInfo, jsonDeserialized.OtherInfo);

    }

    [TestMethod]
    public void TestSerializationBankTransferNull()
    {
        var document = new Document<BankTransfer>
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

        var json = JsonSerializer.Serialize<Document<BankTransfer>>(document);

        Assert.IsNotEmpty(json);

        var jsonDeserialized = JsonSerializer.Deserialize<Document<BankTransfer>>(json);

        Assert.IsNotNull(jsonDeserialized);

        Assert.AreEqual(document.IssuerTin, jsonDeserialized.IssuerTin);
        Assert.AreEqual(document.OtherInfo, jsonDeserialized.OtherInfo);

    }

    [TestMethod]
    public void TestSerializationBankTransfer()
    {
        var document = new Document<BankTransfer>
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
            OtherInfo = new BankTransfer { Iban = "PT0509999999999", Amount = 99.99m, CurrencyCode = null }
        };

        var json = JsonSerializer.Serialize<Document<BankTransfer>>(document);

        Assert.IsNotEmpty(json);

        var jsonDeserialized = JsonSerializer.Deserialize<Document<BankTransfer>>(json);

        Assert.IsNotNull(jsonDeserialized);

        Assert.AreEqual(document.IssuerTin, jsonDeserialized.IssuerTin);
        Assert.AreEqual("TB;PT0509999999999;99.99", jsonDeserialized.OtherInfo?.Info());

    }

    [TestMethod]
    public void TestSerializationMultiBank()
    {
        var document = new Document<MultiBankPayment>
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
            OtherInfo = new MultiBankPayment { Entity = "12345", Reference = "987754321", Amount = 99.99m }
        };

        var json = JsonSerializer.Serialize<Document<MultiBankPayment>>(document);

        Assert.IsNotEmpty(json);

        var jsonDeserialized = JsonSerializer.Deserialize<Document<MultiBankPayment>>(json);

        Assert.IsNotNull(jsonDeserialized);

        Assert.AreEqual(document.IssuerTin, jsonDeserialized.IssuerTin);
        Assert.AreEqual("MB;12345;987754321;99.99", document.OtherInfo?.Info());

    }

}
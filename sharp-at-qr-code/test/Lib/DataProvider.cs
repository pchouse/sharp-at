using PChouse.AT.QRCode.Library;

namespace PChouse.AT.QRCode.Test;


public class DataProvider
{
    public static IEnumerable<(ADocument, string)> BuilderStringData()
    {
        yield return Example1();
        yield return Example1BankTransfer();
        yield return Example1OtherInfo();
        yield return Example1PaymentCurrencyCodeNotEuro();
        yield return Example1PaymentCurrencyCodeNull();
        yield return Example2();
        yield return Example2OtherVatFiscalRegion();
        yield return Example3();
        yield return Example4();
        yield return BuyerTinNull();
        yield return BuyerTinAndCountryCodeNull();

    }

    public static (ADocument, string) Example1()
    {
        return (
            new Document
            {
                IssuerTin = "123456789",
                BuyerTin = "999999990",
                CountryCode = Generics.Country.PT,
                DocumentType = DocumentType.FT,
                DocumentStatus = DocumentStatus.N,
                DocumentDate = new DateOnly(2019, 12, 31),
                DocumentNumber = "FT AB2019/0035",
                ATCUD = "CSDF7T5H-0035",
                Vats = [
                    new Vat{
                        TaxCountryRegion = Generics.TaxCountryRegion.PT,
                        ExemptedBaseVat = 12000.00m,
                        ReducedBaseVat = 15000.00m,
                        ReducedTotalVat = 900.00m,
                        IntermediateBaseVat = 50000.00m,
                        IntermediateTotalVat = 6500.00m,
                        NormalBaseVat = 80000.00m,
                        NormalTotalVat = 18400.00m
                        },
                    new Vat{
                        TaxCountryRegion = Generics.TaxCountryRegion.PT_AC,
                        ExemptedBaseVat = 10000.00m,
                        ReducedBaseVat = 25000.56m,
                        ReducedTotalVat = 1000.02m,
                        IntermediateBaseVat = 75000.00m,
                        IntermediateTotalVat = 6750.00m,
                        NormalBaseVat = 100000.00m,
                        NormalTotalVat = 18000.00m
                        },
                    new Vat{
                        TaxCountryRegion = Generics.TaxCountryRegion.PT_MA,
                        ExemptedBaseVat = 5000.00m,
                        ReducedBaseVat = 12500.00m,
                        ReducedTotalVat = 625.00m,
                        IntermediateBaseVat = 25000.00m,
                        IntermediateTotalVat = 3000.00m,
                        NormalBaseVat = 40000.00m,
                        NormalTotalVat = 8800.00m
                        }

                ],
                TotalNonVat = 100.00m,
                TotalStampTax = 25.00m,
                TaxPayable = 64000.02m,
                GrossTotal = 513600.58m,
                WithholdingTaxAmount = 100.00m,
                Hash = "kLp0",
                ProgramCertificateNumber = 9999,
                OtherInfo = "TB;PT00000000000000000000000;513500.58"
            },
            "A:123456789*B:999999990*C:PT*D:FT*E:N*F:20191231*G:FT AB2019/0035*H:CSDF7T5H-0035*I1:PT*I2:12000.00*I3:15000.00*I4:900.00*I5:50000.00*I6:6500.00*I7:80000.00*I8:18400.00*J1:PT-AC*J2:10000.00*J3:25000.56*J4:1000.02*J5:75000.00*J6:6750.00*J7:100000.00*J8:18000.00*K1:PT-MA*K2:5000.00*K3:12500.00*K4:625.00*K5:25000.00*K6:3000.00*K7:40000.00*K8:8800.00*L:100.00*M:25.00*N:64000.02*O:513600.58*P:100.00*Q:kLp0*R:9999*S:TB;PT00000000000000000000000;513500.58"
        );
    }

    public static (ADocument, string) Example1BankTransfer()
    {
        return (
            new Document<BankTransfer>
            {
                IssuerTin = "123456789",
                BuyerTin = "999999990",
                CountryCode = Generics.Country.PT,
                DocumentType = DocumentType.FT,
                DocumentStatus = DocumentStatus.N,
                DocumentDate = new DateOnly(2019, 12, 31),
                DocumentNumber = "FT AB2019/0035",
                ATCUD = "CSDF7T5H-0035",
                Vats = [
                    new Vat{
                        TaxCountryRegion = Generics.TaxCountryRegion.PT,
                        ExemptedBaseVat = 12000.00m,
                        ReducedBaseVat = 15000.00m,
                        ReducedTotalVat = 900.00m,
                        IntermediateBaseVat = 50000.00m,
                        IntermediateTotalVat = 6500.00m,
                        NormalBaseVat = 80000.00m,
                        NormalTotalVat = 18400.00m
                        },
                    new Vat{
                        TaxCountryRegion = Generics.TaxCountryRegion.PT_AC,
                        ExemptedBaseVat = 10000.00m,
                        ReducedBaseVat = 25000.56m,
                        ReducedTotalVat = 1000.02m,
                        IntermediateBaseVat = 75000.00m,
                        IntermediateTotalVat = 6750.00m,
                        NormalBaseVat = 100000.00m,
                        NormalTotalVat = 18000.00m
                        },
                    new Vat{
                        TaxCountryRegion = Generics.TaxCountryRegion.PT_MA,
                        ExemptedBaseVat = 5000.00m,
                        ReducedBaseVat = 12500.00m,
                        ReducedTotalVat = 625.00m,
                        IntermediateBaseVat = 25000.00m,
                        IntermediateTotalVat = 3000.00m,
                        NormalBaseVat = 40000.00m,
                        NormalTotalVat = 8800.00m
                        }

                ],
                TotalNonVat = 100.00m,
                TotalStampTax = 25.00m,
                TaxPayable = 64000.02m,
                GrossTotal = 513600.58m,
                WithholdingTaxAmount = 100.00m,
                Hash = "kLp0",
                ProgramCertificateNumber = 9999,
                OtherInfo = new BankTransfer
                {
                    Amount = 513500.58m,
                    Iban = "PT00000000000000000000000",
                    CurrencyCode = null
                }
            },
            "A:123456789*B:999999990*C:PT*D:FT*E:N*F:20191231*G:FT AB2019/0035*H:CSDF7T5H-0035*I1:PT*I2:12000.00*I3:15000.00*I4:900.00*I5:50000.00*I6:6500.00*I7:80000.00*I8:18400.00*J1:PT-AC*J2:10000.00*J3:25000.56*J4:1000.02*J5:75000.00*J6:6750.00*J7:100000.00*J8:18000.00*K1:PT-MA*K2:5000.00*K3:12500.00*K4:625.00*K5:25000.00*K6:3000.00*K7:40000.00*K8:8800.00*L:100.00*M:25.00*N:64000.02*O:513600.58*P:100.00*Q:kLp0*R:9999*S:TB;PT00000000000000000000000;513500.58"
        );
    }

    public static (ADocument, string) Example1OtherInfo()
    {
        return (
            new Document<OtherInfo>
            {
                IssuerTin = "123456789",
                BuyerTin = "999999990",
                CountryCode = Generics.Country.PT,
                DocumentType = DocumentType.FT,
                DocumentStatus = DocumentStatus.N,
                DocumentDate = new DateOnly(2019, 12, 31),
                DocumentNumber = "FT AB2019/0035",
                ATCUD = "CSDF7T5H-0035",
                Vats = [
                    new Vat{
                        TaxCountryRegion = Generics.TaxCountryRegion.PT,
                        ExemptedBaseVat = 12000.00m,
                        ReducedBaseVat = 15000.00m,
                        ReducedTotalVat = 900.00m,
                        IntermediateBaseVat = 50000.00m,
                        IntermediateTotalVat = 6500.00m,
                        NormalBaseVat = 80000.00m,
                        NormalTotalVat = 18400.00m
                        },
                    new Vat{
                        TaxCountryRegion = Generics.TaxCountryRegion.PT_AC,
                        ExemptedBaseVat = 10000.00m,
                        ReducedBaseVat = 25000.56m,
                        ReducedTotalVat = 1000.02m,
                        IntermediateBaseVat = 75000.00m,
                        IntermediateTotalVat = 6750.00m,
                        NormalBaseVat = 100000.00m,
                        NormalTotalVat = 18000.00m
                        },
                    new Vat{
                        TaxCountryRegion = Generics.TaxCountryRegion.PT_MA,
                        ExemptedBaseVat = 5000.00m,
                        ReducedBaseVat = 12500.00m,
                        ReducedTotalVat = 625.00m,
                        IntermediateBaseVat = 25000.00m,
                        IntermediateTotalVat = 3000.00m,
                        NormalBaseVat = 40000.00m,
                        NormalTotalVat = 8800.00m
                        }

                ],
                TotalNonVat = 100.00m,
                TotalStampTax = 25.00m,
                TaxPayable = 64000.02m,
                GrossTotal = 513600.58m,
                WithholdingTaxAmount = 100.00m,
                Hash = "kLp0",
                ProgramCertificateNumber = 9999,
                OtherInfo = new OtherInfo { Information = "TB;PT00000000000000000000000;513500.58" }
            },
            "A:123456789*B:999999990*C:PT*D:FT*E:N*F:20191231*G:FT AB2019/0035*H:CSDF7T5H-0035*I1:PT*I2:12000.00*I3:15000.00*I4:900.00*I5:50000.00*I6:6500.00*I7:80000.00*I8:18400.00*J1:PT-AC*J2:10000.00*J3:25000.56*J4:1000.02*J5:75000.00*J6:6750.00*J7:100000.00*J8:18000.00*K1:PT-MA*K2:5000.00*K3:12500.00*K4:625.00*K5:25000.00*K6:3000.00*K7:40000.00*K8:8800.00*L:100.00*M:25.00*N:64000.02*O:513600.58*P:100.00*Q:kLp0*R:9999*S:TB;PT00000000000000000000000;513500.58"
        );
    }

    public static (ADocument, string) Example1PaymentCurrencyCodeNull()
    {
        return (
            new Document<PaymentInfo>
            {
                IssuerTin = "123456789",
                BuyerTin = "999999990",
                CountryCode = Generics.Country.PT,
                DocumentType = DocumentType.FT,
                DocumentStatus = DocumentStatus.N,
                DocumentDate = new DateOnly(2019, 12, 31),
                DocumentNumber = "FT AB2019/0035",
                ATCUD = "CSDF7T5H-0035",
                Vats = [
                    new Vat{
                        TaxCountryRegion = Generics.TaxCountryRegion.PT,
                        ExemptedBaseVat = 12000.00m,
                        ReducedBaseVat = 15000.00m,
                        ReducedTotalVat = 900.00m,
                        IntermediateBaseVat = 50000.00m,
                        IntermediateTotalVat = 6500.00m,
                        NormalBaseVat = 80000.00m,
                        NormalTotalVat = 18400.00m
                        },
                    new Vat{
                        TaxCountryRegion = Generics.TaxCountryRegion.PT_AC,
                        ExemptedBaseVat = 10000.00m,
                        ReducedBaseVat = 25000.56m,
                        ReducedTotalVat = 1000.02m,
                        IntermediateBaseVat = 75000.00m,
                        IntermediateTotalVat = 6750.00m,
                        NormalBaseVat = 100000.00m,
                        NormalTotalVat = 18000.00m
                        },
                    new Vat{
                        TaxCountryRegion = Generics.TaxCountryRegion.PT_MA,
                        ExemptedBaseVat = 5000.00m,
                        ReducedBaseVat = 12500.00m,
                        ReducedTotalVat = 625.00m,
                        IntermediateBaseVat = 25000.00m,
                        IntermediateTotalVat = 3000.00m,
                        NormalBaseVat = 40000.00m,
                        NormalTotalVat = 8800.00m
                        }

                ],
                TotalNonVat = 100.00m,
                TotalStampTax = 25.00m,
                TaxPayable = 64000.02m,
                GrossTotal = 513600.58m,
                WithholdingTaxAmount = 100.00m,
                Hash = "kLp0",
                ProgramCertificateNumber = 9999,
                OtherInfo = new PaymentInfo
                {
                    Amount = 513500.58m,
                    PaymentMechanism = Generics.PaymentMechanism.NU,
                    CurrencyCode = null
                }
            },
            "A:123456789*B:999999990*C:PT*D:FT*E:N*F:20191231*G:FT AB2019/0035*H:CSDF7T5H-0035*I1:PT*I2:12000.00*I3:15000.00*I4:900.00*I5:50000.00*I6:6500.00*I7:80000.00*I8:18400.00*J1:PT-AC*J2:10000.00*J3:25000.56*J4:1000.02*J5:75000.00*J6:6750.00*J7:100000.00*J8:18000.00*K1:PT-MA*K2:5000.00*K3:12500.00*K4:625.00*K5:25000.00*K6:3000.00*K7:40000.00*K8:8800.00*L:100.00*M:25.00*N:64000.02*O:513600.58*P:100.00*Q:kLp0*R:9999*S:NU;513500.58"
        );
    }

    public static (ADocument, string) Example1PaymentCurrencyCodeNotEuro()
    {
        return (
            new Document<PaymentInfo>
            {
                IssuerTin = "123456789",
                BuyerTin = "999999990",
                CountryCode = Generics.Country.PT,
                DocumentType = DocumentType.FT,
                DocumentStatus = DocumentStatus.N,
                DocumentDate = new DateOnly(2019, 12, 31),
                DocumentNumber = "FT AB2019/0035",
                ATCUD = "CSDF7T5H-0035",
                Vats = [
                    new Vat{
                        TaxCountryRegion = Generics.TaxCountryRegion.PT,
                        ExemptedBaseVat = 12000.00m,
                        ReducedBaseVat = 15000.00m,
                        ReducedTotalVat = 900.00m,
                        IntermediateBaseVat = 50000.00m,
                        IntermediateTotalVat = 6500.00m,
                        NormalBaseVat = 80000.00m,
                        NormalTotalVat = 18400.00m
                        },
                    new Vat{
                        TaxCountryRegion = Generics.TaxCountryRegion.PT_AC,
                        ExemptedBaseVat = 10000.00m,
                        ReducedBaseVat = 25000.56m,
                        ReducedTotalVat = 1000.02m,
                        IntermediateBaseVat = 75000.00m,
                        IntermediateTotalVat = 6750.00m,
                        NormalBaseVat = 100000.00m,
                        NormalTotalVat = 18000.00m
                        },
                    new Vat{
                        TaxCountryRegion = Generics.TaxCountryRegion.PT_MA,
                        ExemptedBaseVat = 5000.00m,
                        ReducedBaseVat = 12500.00m,
                        ReducedTotalVat = 625.00m,
                        IntermediateBaseVat = 25000.00m,
                        IntermediateTotalVat = 3000.00m,
                        NormalBaseVat = 40000.00m,
                        NormalTotalVat = 8800.00m
                        }

                ],
                TotalNonVat = 100.00m,
                TotalStampTax = 25.00m,
                TaxPayable = 64000.02m,
                GrossTotal = 513600.58m,
                WithholdingTaxAmount = 100.00m,
                Hash = "kLp0",
                ProgramCertificateNumber = 9999,
                OtherInfo = new PaymentInfo
                {
                    Amount = 513500.58m,
                    PaymentMechanism = Generics.PaymentMechanism.CC,
                    CurrencyCode = Generics.CurrencyCode.AED
                }
            },
            "A:123456789*B:999999990*C:PT*D:FT*E:N*F:20191231*G:FT AB2019/0035*H:CSDF7T5H-0035*I1:PT*I2:12000.00*I3:15000.00*I4:900.00*I5:50000.00*I6:6500.00*I7:80000.00*I8:18400.00*J1:PT-AC*J2:10000.00*J3:25000.56*J4:1000.02*J5:75000.00*J6:6750.00*J7:100000.00*J8:18000.00*K1:PT-MA*K2:5000.00*K3:12500.00*K4:625.00*K5:25000.00*K6:3000.00*K7:40000.00*K8:8800.00*L:100.00*M:25.00*N:64000.02*O:513600.58*P:100.00*Q:kLp0*R:9999*S:CC;513500.58;AED"
        );
    }

    public static (ADocument, string) Example2()
    {
        return (
            new Document<PaymentInfo>
            {
                IssuerTin = "123456789",
                BuyerTin = "999999990",
                CountryCode = Generics.Country.PT,
                DocumentType = DocumentType.FS,
                DocumentStatus = DocumentStatus.N,
                DocumentDate = new DateOnly(2019, 08, 12),
                DocumentNumber = "FS CDVF/12345",
                ATCUD = "CDF7T5HD-12345",
                Vats = [
                    new Vat{
                        TaxCountryRegion = Generics.TaxCountryRegion.PT,
                        NormalBaseVat = .65m,
                        NormalTotalVat = .15m
                        },
                ],
                TaxPayable = .15m,
                GrossTotal = .80m,
                Hash = "YhGV",
                ProgramCertificateNumber = 9999,
                OtherInfo = new PaymentInfo
                {
                    Amount = .80m,
                    PaymentMechanism = Generics.PaymentMechanism.NU,
                    CurrencyCode = null
                }
            },
            "A:123456789*B:999999990*C:PT*D:FS*E:N*F:20190812*G:FS CDVF/12345*H:CDF7T5HD-12345*I1:PT*I7:0.65*I8:0.15*N:0.15*O:0.80*Q:YhGV*R:9999*S:NU;0.80"
        );
    }

    public static (ADocument, string) Example2OtherVatFiscalRegion()
    {
        return (
            new Document<PaymentInfo>
            {
                IssuerTin = "123456789",
                BuyerTin = "999999990",
                CountryCode = Generics.Country.PT,
                DocumentType = DocumentType.FS,
                DocumentStatus = DocumentStatus.N,
                DocumentDate = new DateOnly(2019, 08, 12),
                DocumentNumber = "FS CDVF/12345",
                ATCUD = "CDF7T5HD-12345",
                Vats = [
                    new Vat{
                        TaxCountryRegion = Generics.TaxCountryRegion.KR,
                        NormalBaseVat = .65m,
                        NormalTotalVat = .15m
                        },
                ],
                TaxPayable = .15m,
                GrossTotal = .80m,
                Hash = "YhGV",
                ProgramCertificateNumber = 9999,
                OtherInfo = new PaymentInfo
                {
                    Amount = .80m,
                    PaymentMechanism = Generics.PaymentMechanism.NU,
                    CurrencyCode = null
                }
            },
            "A:123456789*B:999999990*C:PT*D:FS*E:N*F:20190812*G:FS CDVF/12345*H:CDF7T5HD-12345*I1:KR*I7:0.65*I8:0.15*N:0.15*O:0.80*Q:YhGV*R:9999*S:NU;0.80"
        );
    }

    public static (ADocument, string) Example3()
    {
        return (
            new Document<PaymentInfo>
            {
                IssuerTin = "500000000",
                BuyerTin = "123456789",
                CountryCode = Generics.Country.PT,
                DocumentType = DocumentType.PF,
                DocumentStatus = DocumentStatus.N,
                DocumentDate = new DateOnly(2019, 01, 23),
                DocumentNumber = "PF G2019CB/145789",
                ATCUD = "HB6FT7RV-145789",
                Vats = [
                    new Vat{
                        TaxCountryRegion = Generics.TaxCountryRegion.PT,
                        ExemptedBaseVat = 12345.34m,
                        ReducedBaseVat = 12532.65m,
                        ReducedTotalVat = 751.96m,
                        IntermediateBaseVat = 52789.00m,
                        IntermediateTotalVat = 6862.57m,
                        NormalBaseVat = 32425.69m,
                        NormalTotalVat = 7457.91m
                        },
                ],
                TaxPayable = 15072.44m,
                GrossTotal = 125165.12m,
                Hash = "r/fY",
                ProgramCertificateNumber = 9999
            },
            "A:500000000*B:123456789*C:PT*D:PF*E:N*F:20190123*G:PF G2019CB/145789*H:HB6FT7RV-145789*I1:PT*I2:12345.34*I3:12532.65*I4:751.96*I5:52789.00*I6:6862.57*I7:32425.69*I8:7457.91*N:15072.44*O:125165.12*Q:r/fY*R:9999"
        );
    }

    public static (ADocument, string) Example4()
    {
        return (
            new Document<PaymentInfo>
            {
                IssuerTin = "500000000",
                BuyerTin = "123456789",
                CountryCode = Generics.Country.PT,
                DocumentType = DocumentType.GT,
                DocumentStatus = DocumentStatus.N,
                DocumentDate = new DateOnly(2019, 07, 20),
                DocumentNumber = "GT G234CB/50987",
                ATCUD = "GTVX4Y8B-50987",
                Vats = [],
                TaxPayable = 0.0m,
                GrossTotal = 0.0m,
                Hash = "5uIg",
                ProgramCertificateNumber = 9999
            },
            "A:500000000*B:123456789*C:PT*D:GT*E:N*F:20190720*G:GT G234CB/50987*H:GTVX4Y8B-50987*I1:0*N:0.00*O:0.00*Q:5uIg*R:9999"
        );
    }

    public static (ADocument, string) BuyerTinNull()
    {
        return (
            new Document
            {
                IssuerTin = "123456789",
                BuyerTin = null,
                CountryCode = Generics.Country.PT,
                DocumentType = DocumentType.FT,
                DocumentStatus = DocumentStatus.N,
                DocumentDate = new DateOnly(2019, 12, 31),
                DocumentNumber = "FT AB2019/0035",
                ATCUD = "CSDF7T5H-0035",
                Vats = [
                    new Vat{
                        TaxCountryRegion = Generics.TaxCountryRegion.PT,
                        ExemptedBaseVat = 12000.00m,
                        ReducedBaseVat = 15000.00m,
                        ReducedTotalVat = 900.00m,
                        IntermediateBaseVat = 50000.00m,
                        IntermediateTotalVat = 6500.00m,
                        NormalBaseVat = 80000.00m,
                        NormalTotalVat = 18400.00m
                        },
                    new Vat{
                        TaxCountryRegion = Generics.TaxCountryRegion.PT_AC,
                        ExemptedBaseVat = 10000.00m,
                        ReducedBaseVat = 25000.56m,
                        ReducedTotalVat = 1000.02m,
                        IntermediateBaseVat = 75000.00m,
                        IntermediateTotalVat = 6750.00m,
                        NormalBaseVat = 100000.00m,
                        NormalTotalVat = 18000.00m
                        },
                    new Vat{
                        TaxCountryRegion = Generics.TaxCountryRegion.PT_MA,
                        ExemptedBaseVat = 5000.00m,
                        ReducedBaseVat = 12500.00m,
                        ReducedTotalVat = 625.00m,
                        IntermediateBaseVat = 25000.00m,
                        IntermediateTotalVat = 3000.00m,
                        NormalBaseVat = 40000.00m,
                        NormalTotalVat = 8800.00m
                        }

                ],
                TotalNonVat = 100.00m,
                TotalStampTax = 25.00m,
                TaxPayable = 64000.02m,
                GrossTotal = 513600.58m,
                WithholdingTaxAmount = 100.00m,
                Hash = "kLp0",
                ProgramCertificateNumber = 9999,
                OtherInfo = "TB;PT00000000000000000000000;513500.58"
            },
            "A:123456789*B:999999990*C:PT*D:FT*E:N*F:20191231*G:FT AB2019/0035*H:CSDF7T5H-0035*I1:PT*I2:12000.00*I3:15000.00*I4:900.00*I5:50000.00*I6:6500.00*I7:80000.00*I8:18400.00*J1:PT-AC*J2:10000.00*J3:25000.56*J4:1000.02*J5:75000.00*J6:6750.00*J7:100000.00*J8:18000.00*K1:PT-MA*K2:5000.00*K3:12500.00*K4:625.00*K5:25000.00*K6:3000.00*K7:40000.00*K8:8800.00*L:100.00*M:25.00*N:64000.02*O:513600.58*P:100.00*Q:kLp0*R:9999*S:TB;PT00000000000000000000000;513500.58"
        );
    }

    public static (ADocument, string) BuyerTinAndCountryCodeNull()
    {
        return (
            new Document
            {
                IssuerTin = "123456789",
                BuyerTin = null,
                CountryCode = null,
                DocumentType = DocumentType.FT,
                DocumentStatus = DocumentStatus.N,
                DocumentDate = new DateOnly(2019, 12, 31),
                DocumentNumber = "FT AB2019/0035",
                ATCUD = "CSDF7T5H-0035",
                Vats = [
                    new Vat{
                        TaxCountryRegion = Generics.TaxCountryRegion.PT,
                        ExemptedBaseVat = 12000.00m,
                        ReducedBaseVat = 15000.00m,
                        ReducedTotalVat = 900.00m,
                        IntermediateBaseVat = 50000.00m,
                        IntermediateTotalVat = 6500.00m,
                        NormalBaseVat = 80000.00m,
                        NormalTotalVat = 18400.00m
                        },
                    new Vat{
                        TaxCountryRegion = Generics.TaxCountryRegion.PT_AC,
                        ExemptedBaseVat = 10000.00m,
                        ReducedBaseVat = 25000.56m,
                        ReducedTotalVat = 1000.02m,
                        IntermediateBaseVat = 75000.00m,
                        IntermediateTotalVat = 6750.00m,
                        NormalBaseVat = 100000.00m,
                        NormalTotalVat = 18000.00m
                        },
                    new Vat{
                        TaxCountryRegion = Generics.TaxCountryRegion.PT_MA,
                        ExemptedBaseVat = 5000.00m,
                        ReducedBaseVat = 12500.00m,
                        ReducedTotalVat = 625.00m,
                        IntermediateBaseVat = 25000.00m,
                        IntermediateTotalVat = 3000.00m,
                        NormalBaseVat = 40000.00m,
                        NormalTotalVat = 8800.00m
                        }

                ],
                TotalNonVat = 100.00m,
                TotalStampTax = 25.00m,
                TaxPayable = 64000.02m,
                GrossTotal = 513600.58m,
                WithholdingTaxAmount = 100.00m,
                Hash = "kLp0",
                ProgramCertificateNumber = 9999,
                OtherInfo = "TB;PT00000000000000000000000;513500.58"
            },
            "A:123456789*B:999999990*C:Desconhecido*D:FT*E:N*F:20191231*G:FT AB2019/0035*H:CSDF7T5H-0035*I1:PT*I2:12000.00*I3:15000.00*I4:900.00*I5:50000.00*I6:6500.00*I7:80000.00*I8:18400.00*J1:PT-AC*J2:10000.00*J3:25000.56*J4:1000.02*J5:75000.00*J6:6750.00*J7:100000.00*J8:18000.00*K1:PT-MA*K2:5000.00*K3:12500.00*K4:625.00*K5:25000.00*K6:3000.00*K7:40000.00*K8:8800.00*L:100.00*M:25.00*N:64000.02*O:513600.58*P:100.00*Q:kLp0*R:9999*S:TB;PT00000000000000000000000;513500.58"
        );
    }

}

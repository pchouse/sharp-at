using PChouse.AT.QRCode.API.Library;
using PChouse.AT.QRCode.API.Test;
using PChouse.AT.QRCode.Library;

namespace PChouse.AT.QRCode.API.Resources;

public static class DataProvider
{

    public static IEnumerable<(HttpClient client, Request, string)> Data()
    {
        yield return (
            HttpClientProvider.HttpClient(),
            new Request
            {
                Document = new Document
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
                BuilderOptions = new BuilderOptions
                {
                    ThrowExceptionOnError = false,
                    ValidateDocumentNumber = false,
                    ValidateBuyerPortugueseTin = TinValidation.NONE,
                    ValidateIssuerTin = TinValidation.NONE
                }
            },
            "A:123456789*B:999999990*C:PT*D:FT*E:N*F:20191231*G:FT AB2019/0035*H:CSDF7T5H-0035*I1:PT*I2:12000.00*I3:15000.00*I4:900.00*I5:50000.00*I6:6500.00*I7:80000.00*I8:18400.00*J1:PT-AC*J2:10000.00*J3:25000.56*J4:1000.02*J5:75000.00*J6:6750.00*J7:100000.00*J8:18000.00*K1:PT-MA*K2:5000.00*K3:12500.00*K4:625.00*K5:25000.00*K6:3000.00*K7:40000.00*K8:8800.00*L:100.00*M:25.00*N:64000.02*O:513600.58*P:100.00*Q:kLp0*R:9999*S:TB;PT00000000000000000000000;513500.58"
        );
    }


    public static IEnumerable<(HttpClient client, Request, string)> DataThrow()
    {
        yield return (
            HttpClientProvider.HttpClient(),
            new Request
            {
                Document = new Document
                {
                    IssuerTin = "123456789",
                    BuyerTin = "9999999",
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
                BuilderOptions = new BuilderOptions
                {
                    ThrowExceptionOnError = false,
                    ValidateDocumentNumber = false,
                    ValidateBuyerPortugueseTin = TinValidation.STRUCTURAL,
                    ValidateIssuerTin = TinValidation.NONE
                }
            },
            "A:123456789*B:9999999*C:PT*D:FT*E:N*F:20191231*G:FT AB2019/0035*H:CSDF7T5H-0035*I1:PT*I2:12000.00*I3:15000.00*I4:900.00*I5:50000.00*I6:6500.00*I7:80000.00*I8:18400.00*J1:PT-AC*J2:10000.00*J3:25000.56*J4:1000.02*J5:75000.00*J6:6750.00*J7:100000.00*J8:18000.00*K1:PT-MA*K2:5000.00*K3:12500.00*K4:625.00*K5:25000.00*K6:3000.00*K7:40000.00*K8:8800.00*L:100.00*M:25.00*N:64000.02*O:513600.58*P:100.00*Q:kLp0*R:9999*S:TB;PT00000000000000000000000;513500.58"
        );

        yield return (
            HttpClientProvider.HttpClient(),
            new Request
            {
                Document = new Document
                {
                    IssuerTin = "123456789",
                    BuyerTin = "9999999",
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
                BuilderOptions = new BuilderOptions
                {
                    ThrowExceptionOnError = true,
                    ValidateDocumentNumber = false,
                    ValidateBuyerPortugueseTin = TinValidation.STRUCTURAL,
                    ValidateIssuerTin = TinValidation.NONE
                }
            },
            "A:123456789*B:9999999*C:PT*D:FT*E:N*F:20191231*G:FT AB2019/0035*H:CSDF7T5H-0035*I1:PT*I2:12000.00*I3:15000.00*I4:900.00*I5:50000.00*I6:6500.00*I7:80000.00*I8:18400.00*J1:PT-AC*J2:10000.00*J3:25000.56*J4:1000.02*J5:75000.00*J6:6750.00*J7:100000.00*J8:18000.00*K1:PT-MA*K2:5000.00*K3:12500.00*K4:625.00*K5:25000.00*K6:3000.00*K7:40000.00*K8:8800.00*L:100.00*M:25.00*N:64000.02*O:513600.58*P:100.00*Q:kLp0*R:9999*S:TB;PT00000000000000000000000;513500.58"
        );

    }

    public static IEnumerable<(HttpClient, RequestImage)>RequestImageData()
    {
        yield return (
            HttpClientProvider.HttpClient(),
            new RequestImage
            {
                BuilderOptions = new BuilderOptions
                {
                    ValidateBuyerPortugueseTin = TinValidation.NONE,
                    ValidateDocumentNumber = false,
                    ValidateIssuerTin = TinValidation.NONE,
                    ThrowExceptionOnError = false
                },
                ImageOptions = new ImageOptions
                {
                    ImageType = ImageType.Png
                },
                Document = new Document
                {
                    IssuerTin = "123456789",
                    BuyerTin = "9999999",
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
                }
            }
        );
    }
    
}
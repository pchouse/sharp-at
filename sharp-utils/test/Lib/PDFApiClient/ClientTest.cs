
using System.Text.Json;
using Microsoft.Extensions.Logging.Abstractions;
using PChouse.Utils.PDFApiClient;
using PChouse.Utils.PDFApiClient.Multicert;

namespace PChouse.Utils.Test.PDFApiClient;

[TestClass]
public sealed class PDFApiClientTest
{

    public static string MulticertService = "????";
    public static string MulticertPassword = "????";

    public static string PdfFilesBasePath = "/folder/path";

    [TestMethod]
    public void TestGenerateReport()
    {
        string outputSignedFilePath = $"{PdfFilesBasePath}/report-test.pdf";

        var factory = new Client.ClientFactory(NullLogger<Client>.Instance);

        var request = new RequestReport<Database>
        {
            Report = "Flower_Landscape.jasper",
            PrinterName = null,
            ReportType = ReportType.PDF,
            Copies = 3,
            DataSource = new Database
            {
                ServerAddress = "127.0.0.1",
                Provider = Provider.MARIADB,
                User = "rebelo",
                Password = "password",
                DatabaseSchema = "sakila"
            }
        };

        request.Parameters.Add(Parameter.New(ParameterType.P_STRING, "P_STRING", "Parameter String"));
        request.Parameters.Add(Parameter.New(ParameterType.P_BOOLEAN, "P_BOOLEAN", "true"));
        request.Parameters.Add(Parameter.New(ParameterType.P_DOUBLE, "P_DOUBLE", "999.49"));
        request.Parameters.Add(Parameter.New(ParameterType.P_FLOAT, "P_FLOAT", "9.09"));
        request.Parameters.Add(Parameter.New(ParameterType.P_INTEGER, "P_INTEGER", "9"));
        request.Parameters.Add(Parameter.New(ParameterType.P_LONG, "P_LONG", "49"));
        request.Parameters.Add(Parameter.New(ParameterType.P_SHORT, "P_SHORT", "109"));
        request.Parameters.Add(Parameter.New(ParameterType.P_BIGDECIMAL, "P_BIG_DECIMAL", "1999.2999"));
        request.Parameters.Add(Parameter.New(ParameterType.P_SQL_DATE, "P_SQL_DATE", "1738856468813"));
        request.Parameters.Add(Parameter.New(ParameterType.P_SQL_TIME, "P_SQL_TIME", "1738856468813"));
        request.Parameters.Add(Parameter.New(ParameterType.P_TIMESTAMP, "P_TIMESTAMP", "1738856468813"));
        request.Parameters.Add(Parameter.New(ParameterType.P_DATE, "P_DATE", "1969-10-05", "yyyy-MM-dd"));

        var client = factory.Instance(httpClientHandler: handler =>
        {
            handler.ServerCertificateCustomValidationCallback = (requestMessage, certificate, chain, sslPolicyErrors) => true;
        });

        var response = client.ReportAsync(request).GetAwaiter().GetResult();

        Assert.AreEqual(ResponseStatus.OK, response.Status);

        File.WriteAllBytes(outputSignedFilePath, Convert.FromBase64String(response.Report));

    }

    [TestMethod]
    public void TestGenerateAndSignReport()
    {
        string multicertService = MulticertService;
        string multicertPassword = MulticertPassword;
        string outputSignedFilePath = $"{PdfFilesBasePath}/report-sign-test.pdf";

        var factory = new Client.ClientFactory(NullLogger<Client>.Instance);

        var request = new RequestReport<Database, MulticertSignRequest>
        {
            Report = "Flower_Landscape.jasper",
            PrinterName = null,
            ReportType = ReportType.PDF,
            Copies = 3,
            DataSource = new Database
            {
                ServerAddress = "127.0.0.1",
                Provider = Provider.MARIADB,
                User = "rebelo",
                Password = "password",
                DatabaseSchema = "sakila"
            },
            SignPDF = new MulticertSignRequest
            {
                Description = "Test API client",
                FileName = "test_signed.pdf",
                IsTest = true,
                Password = multicertPassword,
                UserName = multicertService,
                SignatureInfo = new SignatureInfo
                {
                    Location = "Lisbon",
                    Reason = "Test API client",
                    VisibleSignatureInfo = new VisibleSignatureInfo
                    {
                        LocationType = LocationType.COORDINATE,
                        AreaBackgroundColor = "transparent",
                        LocationValue = "page=1,x=100,y=500",
                        OneOffTemplate = "Sign test",
                        Area = "width=300,height=200",
                        ImageType = ImageType.PNG,
                        ImageValue = "iVBORw0KGgoAAAANSUhEUgAAABkAAAAZCAYAAADE6YVjAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAG9JREFUeNpiNA5TSWBgYFBgoB04wAIk4oHYgYaWMDAx0AGwIHvr7Ko7jugKgMG5n4BPcen7T1efDB9LWIhQsxCID+KRf0CxJcBIXTAaJ6OWjFoyasmoJXQo6g2gVS1NLRGgVasFZMkFGofWA4AAAwASfRWdTDGoUgAAAABJRU5ErkJggg=="
                    }
                }
            }

        };

        request.Parameters.Add(Parameter.New(ParameterType.P_STRING, "P_STRING", "Parameter String"));
        request.Parameters.Add(Parameter.New(ParameterType.P_BOOLEAN, "P_BOOLEAN", "true"));
        request.Parameters.Add(Parameter.New(ParameterType.P_DOUBLE, "P_DOUBLE", "999.49"));
        request.Parameters.Add(Parameter.New(ParameterType.P_FLOAT, "P_FLOAT", "9.09"));
        request.Parameters.Add(Parameter.New(ParameterType.P_INTEGER, "P_INTEGER", "9"));
        request.Parameters.Add(Parameter.New(ParameterType.P_LONG, "P_LONG", "49"));
        request.Parameters.Add(Parameter.New(ParameterType.P_SHORT, "P_SHORT", "109"));
        request.Parameters.Add(Parameter.New(ParameterType.P_BIGDECIMAL, "P_BIG_DECIMAL", "1999.2999"));
        request.Parameters.Add(Parameter.New(ParameterType.P_SQL_DATE, "P_SQL_DATE", "1738856468813"));
        request.Parameters.Add(Parameter.New(ParameterType.P_SQL_TIME, "P_SQL_TIME", "1738856468813"));
        request.Parameters.Add(Parameter.New(ParameterType.P_TIMESTAMP, "P_TIMESTAMP", "1738856468813"));
        request.Parameters.Add(Parameter.New(ParameterType.P_DATE, "P_DATE", "1969-10-05", "yyyy-MM-dd"));

        var client = factory.Instance(httpClientHandler: handler =>
        {
            handler.ServerCertificateCustomValidationCallback = (requestMessage, certificate, chain, sslPolicyErrors) => true;
        });

        var response = client.ReportAsync(request).GetAwaiter().GetResult();

        Assert.AreEqual(ResponseStatus.OK, response.Status);

        File.WriteAllBytes(outputSignedFilePath, Convert.FromBase64String(response.Report));

    }

    [TestMethod]
    public void TestSignRequest()
    {
        string multicertService = MulticertService;
        string multicertPassword = MulticertPassword;
        string pdfFileToSign = $"{PdfFilesBasePath}/report-test.pdf";
        string outputSignedFilePath = $"{PdfFilesBasePath}/sign-test.pdf";

        var factory = new Client.ClientFactory(NullLogger<Client>.Instance);

        var request = new RequestSign<MulticertSignRequest>
        {
            PdfBase64 = Convert.ToBase64String(File.ReadAllBytes(pdfFileToSign)),
            SignPDF = new MulticertSignRequest
            {
                Description = "Test API client",
                FileName = "test_signed.pdf",
                IsTest = true,
                Password = multicertPassword,
                UserName = multicertService,
                SignatureInfo = new SignatureInfo
                {
                    Location = "Lisbon",
                    Reason = "Test API client",
                    VisibleSignatureInfo = new VisibleSignatureInfo
                    {
                        LocationType = LocationType.COORDINATE,
                        AreaBackgroundColor = "transparent",
                        LocationValue = "page=1,x=100,y=500",
                        OneOffTemplate = "Sign test",
                        Area = "width=300,height=200",
                        ImageType = ImageType.PNG,
                        ImageValue = "iVBORw0KGgoAAAANSUhEUgAAABkAAAAZCAYAAADE6YVjAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAG9JREFUeNpiNA5TSWBgYFBgoB04wAIk4oHYgYaWMDAx0AGwIHvr7Ko7jugKgMG5n4BPcen7T1efDB9LWIhQsxCID+KRf0CxJcBIXTAaJ6OWjFoyasmoJXQo6g2gVS1NLRGgVasFZMkFGofWA4AAAwASfRWdTDGoUgAAAABJRU5ErkJggg=="
                    }
                }
            }

        };

        var client = factory.Instance(httpClientHandler: handler =>
        {
            handler.ServerCertificateCustomValidationCallback = (requestMessage, certificate, chain, sslPolicyErrors) => true;
        });

        var response = client.SignAsync(request).GetAwaiter().GetResult();

        Assert.AreEqual(ResponseStatus.OK, response.Status);

        File.WriteAllBytes(outputSignedFilePath, Convert.FromBase64String(response.Report));

    }

    [TestMethod]
    public void TestPrintRequest()
    {
        string multicertService = MulticertService;
        string multicertPassword = MulticertPassword;
        string pdfFileToPrint = $"{PdfFilesBasePath}/report-test.pdf";

        var factory = new Client.ClientFactory(NullLogger<Client>.Instance);

        var request = new RequestPrint
        {
            PdfBase64 = Convert.ToBase64String(File.ReadAllBytes(pdfFileToPrint)),
            PrinterName = "PDF",
            AfterPrintOperations = []
        };

        var client = factory.Instance(httpClientHandler: handler =>
        {
            handler.ServerCertificateCustomValidationCallback = (requestMessage, certificate, chain, sslPolicyErrors) => true;
        });

        var response = client.PrintAsync(request).GetAwaiter().GetResult();

        Assert.AreEqual(ResponseStatus.OK, response.Status);

    }

    [TestMethod]
    public void TestImageRequest()
    {
        string multicertService = MulticertService;
        string multicertPassword = MulticertPassword;
        string pdfFileToImage = $"{PdfFilesBasePath}/report-test.pdf";
        string outputImageFile = $"{PdfFilesBasePath}/pdf-image-test.png";

        var factory = new Client.ClientFactory(NullLogger<Client>.Instance);

        var request = new RequestPdfToImage
        {
            Pdf = Convert.ToBase64String(File.ReadAllBytes(pdfFileToImage)),
            Dpi = 120f,
        };

        var client = factory.Instance(httpClientHandler: handler =>
        {
            handler.ServerCertificateCustomValidationCallback = (requestMessage, certificate, chain, sslPolicyErrors) => true;
        });

        var response = client.ImageAsync(request).GetAwaiter().GetResult();

        Assert.AreEqual(ResponseStatus.OK, response.Status);
        File.WriteAllBytes(outputImageFile, Convert.FromBase64String(response.Images.First()));
    }

}

using System.Net.Http.Json;
using PChouse.AT.QRCode.API.Library;
using PChouse.AT.QRCode.API.Resources;
using PChouse.AT.QRCode.Library;

namespace PChouse.AT.QRCode.API.Test;

[TestClass]
public sealed class QrCodeStringTest
{

    [TestMethod]
    [DynamicData(nameof(DataProvider.Data), typeof(DataProvider))]
    public async Task TestBuildString(HttpClient httpClient, Request request, string expected)
    {
        var httpResponse = await httpClient.PostAsJsonAsync("build-string", request);
        Assert.AreEqual(System.Net.HttpStatusCode.OK, httpResponse.StatusCode);

        var response = await httpResponse.Content.ReadFromJsonAsync<Response>();

        Assert.IsNotNull(response);
        Assert.HasCount(0, response.Error ?? []);
        Assert.AreEqual(expected, response.QrCode);
    }

    [TestMethod]
    [DynamicData(nameof(DataProvider.DataThrow), typeof(DataProvider))]
    public async Task TestBuildStringError(HttpClient httpClient, Request request, string expected)
    {
        var httpResponse = await httpClient.PostAsJsonAsync("build-string", request);

        if (request.BuilderOptions.ThrowExceptionOnError)
        {
            Assert.AreEqual(System.Net.HttpStatusCode.BadRequest, httpResponse.StatusCode);
            var response = await httpResponse.Content.ReadFromJsonAsync<ErrorResponse>();
            Assert.IsNotNull(response);

        }
        else
        {
            Assert.AreEqual(System.Net.HttpStatusCode.OK, httpResponse.StatusCode);
            var response = await httpResponse.Content.ReadFromJsonAsync<Response>();
            Assert.IsNotNull(response);
            Assert.IsGreaterThan(0, response.Error?.Count() ?? 0);
            Assert.AreEqual(expected, response.QrCode);
        }
    }

    [TestMethod]
    [DynamicData(nameof(DataProvider.RequestImageData), typeof(DataProvider))]
    public async Task TestGenerateImageAndParseToDocument(HttpClient httpClient, RequestImage request)
    {
        var httpImageResponse = await httpClient.PostAsJsonAsync("generate-image", request);

        var responseImage = await httpImageResponse.Content.ReadFromJsonAsync<Response>();

        Assert.IsNotNull(responseImage);
        Assert.IsNotNull(responseImage.QrCode);
        Assert.IsNotEmpty(responseImage.QrCode);

        var httpDocumentResponse = await httpClient.PostAsJsonAsync("image-to-document", new RequestFromImage
        {
            Image = responseImage.QrCode
        });

        var parsedDocument = await httpDocumentResponse.Content.ReadFromJsonAsync<Document>();

        Assert.AreEqual(request.Document.BuyerTin, parsedDocument?.BuyerTin);
        Assert.AreEqual(request.Document.IssuerTin, parsedDocument?.IssuerTin);
    }

    [TestMethod]
    public async Task TestGenerateImageFromStringAndParseImageToString()
    {
        var httpClient = HttpClientProvider.HttpClient();
        var request = new RequestImageFromString
        {
            QRCode = "A:123456789*B:9999999*C:PT*D:FT*E:N*F:20191231*G:FT AB2019/0035*H:CSDF7T5H-0035*I1:PT*I2:12000.00*I3:15000.00*I4:900.00*I5:50000.00*I6:6500.00*I7:80000.00*I8:18400.00*J1:PT-AC*J2:10000.00*J3:25000.56*J4:1000.02*J5:75000.00*J6:6750.00*J7:100000.00*J8:18000.00*K1:PT-MA*K2:5000.00*K3:12500.00*K4:625.00*K5:25000.00*K6:3000.00*K7:40000.00*K8:8800.00*L:100.00*M:25.00*N:64000.02*O:513600.58*P:100.00*Q:kLp0*R:9999*S:TB;PT00000000000000000000000;513500.58",
            ImageOptions = new ImageOptions
            {
                ImageType = ImageType.Svg
            }
        };

        var httpImageResponse = await httpClient.PostAsJsonAsync("generate-image-string", request);

        var responseImage = await httpImageResponse.Content.ReadFromJsonAsync<Response>();

        Assert.IsNotNull(responseImage);
        Assert.IsNotNull(responseImage.QrCode);
        Assert.IsNotEmpty(responseImage.QrCode);

        var httpDocumentResponse = await httpClient.PostAsJsonAsync("image-to-string", new RequestFromImage
        {
            Image = responseImage.QrCode
        });

        var parsedString = await httpDocumentResponse.Content.ReadFromJsonAsync<Response>();

        Assert.AreEqual(request.QRCode, parsedString?.QrCode);
    }


    [TestMethod]
    public async Task TestGenerateDocumentFromString()
    {
        var httpClient = HttpClientProvider.HttpClient();
        var request = new RequestFromString
        {
            QRCode = "A:123456789*B:9999999*C:PT*D:FT*E:N*F:20191231*G:FT AB2019/0035*H:CSDF7T5H-0035*I1:PT*I2:12000.00*I3:15000.00*I4:900.00*I5:50000.00*I6:6500.00*I7:80000.00*I8:18400.00*J1:PT-AC*J2:10000.00*J3:25000.56*J4:1000.02*J5:75000.00*J6:6750.00*J7:100000.00*J8:18000.00*K1:PT-MA*K2:5000.00*K3:12500.00*K4:625.00*K5:25000.00*K6:3000.00*K7:40000.00*K8:8800.00*L:100.00*M:25.00*N:64000.02*O:513600.58*P:100.00*Q:kLp0*R:9999*S:TB;PT00000000000000000000000;513500.58"
        };

        var httpResponse = await httpClient.PostAsJsonAsync("string-to-document", request);

        var responseDocument = await httpResponse.Content.ReadFromJsonAsync<Document>();

        Assert.IsNotNull(responseDocument);

        Assert.AreEqual("123456789", responseDocument.IssuerTin);
        Assert.AreEqual("9999999", responseDocument.BuyerTin);
    }

}

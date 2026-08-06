
namespace PChouse.AT.QRCode.API.Test;

public static class HttpClientProvider
{
    public static HttpClient HttpClient()
    {
        var application = new TestWebApplicationFactory<Program>();
        var client = application.CreateClient();
        client.BaseAddress = new Uri("http://localhost:5207");
        return client;
    }
}
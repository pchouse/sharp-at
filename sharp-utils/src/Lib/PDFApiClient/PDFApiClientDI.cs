/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace PChouse.Utils.PDFApiClient;

public static class PDFApiClientDI
{
    /// <summary>
    /// Add the pdf api client factory to the service collection
    /// </summary>
    /// <param name="serviceCollection"></param>
    public static void AddPDFApiClientFactoryService(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IClientFactory, Client.ClientFactory>();
    }

    /// <summary>
    /// Add as singleton a instance of PDF api client to services DI for https://github.com/pchouse/pt.pchouse.pdf
    /// </summary>
    /// <param name="options"></param>
    /// <param name="httpClient"></param>
    /// <param name="httpClientHandler"></param>
    /// <returns></returns>
    public static void AddPdfApiClient(
        this IServiceCollection serviceCollection,
        Action<ClientOptions>? options = null,
        Action<HttpClient>? httpClient = null,
        Action<HttpClientHandler>? httpClientHandler = null
     )
    {
        serviceCollection.AddSingleton<IClient>(service =>
        {
            var clientOptions = new ClientOptions();
            options?.Invoke(clientOptions);

            var instance = new Client(
                service.GetRequiredService<ILogger<Client>>(),
                clientOptions
                );

            httpClient?.Invoke(instance.HttpClient);
            httpClientHandler?.Invoke(instance.HttpClientHandler);

            return instance;
        });

    }

}


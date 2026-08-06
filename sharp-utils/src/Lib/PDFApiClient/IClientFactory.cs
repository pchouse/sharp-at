/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

namespace PChouse.Utils.PDFApiClient;

public interface IClientFactory
{

    /// <summary>
    /// Get Instance of PDF api client
    /// </summary>
    /// <param name="options"></param>
    /// <returns></returns>
    IClient Instance(ClientOptions options);

    /// <summary>
    /// Get Instance of PDF api client
    /// </summary>
    /// <param name="options"></param>
    /// <param name="httpClient"></param>
    /// <returns></returns>
    IClient Instance(Action<ClientOptions> options, Action<HttpClient> httpClient);

    /// <summary>
    /// Get Instance of PDF api client
    /// </summary>
    /// <param name="httpClient"></param>
    /// <returns></returns>
    IClient Instance(Action<HttpClient> httpClient);

    /// <summary>
    /// Get a instance of PDF api client
    /// </summary>
    /// <param name="options"></param>
    /// <param name="httpClient"></param>
    /// <param name="httpClientHandler"></param>
    /// <returns></returns>
    IClient Instance(Action<ClientOptions>? options = null, Action<HttpClient>? httpClient = null, Action<HttpClientHandler>? httpClientHandler = null);

}
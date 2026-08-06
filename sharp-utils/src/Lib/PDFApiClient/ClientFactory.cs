/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using Microsoft.Extensions.Logging;

namespace PChouse.Utils.PDFApiClient;

/// <summary>
/// PDF api client for https://github.com/pchouse/pt.pchouse.pdf
/// </summary>
public partial class Client
{
    /// <summary>
    /// PDF class api factory
    /// </summary>
    /// <param name="_logger"></param>
    public class ClientFactory(ILogger<Client> _logger) : IClientFactory
    {
        /// <summary>
        /// Get Instance of PDF api client for https://github.com/pchouse/pt.pchouse.pdf
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public virtual IClient Instance(ClientOptions options)
        {
            return new Client(_logger, options);
        }

        /// <summary>
        /// Get Instance of PDF api client for https://github.com/pchouse/pt.pchouse.pdf
        /// </summary>
        /// <param name="options"></param>
        /// <param name="httpClient"></param>
        /// <returns></returns>
        public virtual IClient Instance(Action<ClientOptions> options, Action<HttpClient> httpClient)
        {
            var clientOptions = new ClientOptions();
            options.Invoke(clientOptions);
            var instance = Instance(clientOptions);
            httpClient.Invoke(instance.HttpClient);
            return instance;
        }

        /// <summary>
        /// Get Instance of PDF api client for https://github.com/pchouse/pt.pchouse.pdf
        /// </summary>
        /// <param name="httpClient"></param>
        /// <returns></returns>
        public virtual IClient Instance(Action<HttpClient> httpClient)
        {
            var clientOptions = new ClientOptions();
            var instance = Instance(clientOptions);
            httpClient.Invoke(instance.HttpClient);
            return instance;
        }

        /// <summary>
        /// Get a instance of PDF api client for https://github.com/pchouse/pt.pchouse.pdf
        /// </summary>
        /// <param name="options"></param>
        /// <param name="httpClient"></param>
        /// <param name="httpClientHandler"></param>
        /// <returns></returns>
        public virtual IClient Instance(Action<ClientOptions>? options = null, Action<HttpClient>? httpClient = null, Action<HttpClientHandler>? httpClientHandler = null)
        {
            var clientOptions = new ClientOptions();
            options?.Invoke(clientOptions);
            var instance = Instance(clientOptions);
            httpClient?.Invoke(instance.HttpClient);
            httpClientHandler?.Invoke(instance.HttpClientHandler);
            return instance;
        }
    }
}
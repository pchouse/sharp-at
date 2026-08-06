/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace PChouse.Utils.PDFApiClient;

/// <summary>
/// Client for https://github.com/pchouse/pt.pchouse.pdf
/// </summary>
public partial class Client : IClient
{

    /// <summary>
    /// Logger for the Client class
    /// </summary>
    private readonly ILogger _logger;

    /// <summary>
    /// HTTP client for making requests to the PDF API
    /// </summary>
    public virtual HttpClient HttpClient { get; }

    /// <summary>
    /// The http client handler
    /// </summary>
    public virtual HttpClientHandler HttpClientHandler { get; }

    /// <summary>
    /// Client options
    /// </summary>
    private ClientOptions _options;

    /// <summary>
    /// Constructor for the Client class.
    /// Client for: https://github.com/pchouse/pt.pchouse.pdf
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="configManager"></param>
    /// <param name="appState"></param>
    public Client(ILogger<Client> logger, ClientOptions options)
    {
        _logger = logger;
        _options = options;

        _logger.LogTrace($"new instance of {nameof(Client)}");

        HttpClientHandler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (requestMessage, certificate, chain, sslPolicyErrors) =>
            {
                // If no errors accept the certificate
                if (sslPolicyErrors == System.Net.Security.SslPolicyErrors.None)
                {
                    return true;
                }

                // Accept the you self sign certificate (ssl certificate hash)
                if (
                    !string.IsNullOrWhiteSpace(_options.EndpointSslCertificateHash) &&
                    certificate?.Thumbprint == _options.EndpointSslCertificateHash
                )
                {
                    return true;
                }

                return false;
            }
        };

        HttpClient = new HttpClient(HttpClientHandler)
        {
            BaseAddress = new Uri(
                string.IsNullOrWhiteSpace(options.PdfApiEndpoint)
                    ? throw new ClientException("PDF API Endpoint is empty")
                    : options.PdfApiEndpoint
            ),
        };
    }

    /// <summary>
    /// Sends a asynchronous request to the PDF API and returns the response.
    /// </summary>
    /// <param name="path"></param>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="ClientException"></exception>
    /// <exception cref="OperationCanceledException"></exception>
    private async Task<T> RequestAsync<T>(string path, object request, CancellationToken cancellationToken)
    {

        _logger.LogInformation("PDF api client request {path}", path);

        var json = JsonSerializer.Serialize(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        using var httpResponse = await HttpClient.PostAsync(path, content, cancellationToken);

        var jsonResponse = await httpResponse.Content.ReadFromJsonAsync<T>(cancellationToken);

        if (jsonResponse is null)
        {
            _logger.LogError("Client pdf ip response null for path {path}", path);
            throw new ClientException($"Client pdf ip response null for path {path}");
        }

        return jsonResponse;
    }

    /// <summary>
    /// Sends a asynchronous request to the PDF API to generate a report.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="ClientException"></exception>
    /// <exception cref="OperationCanceledException"></exception>
    public virtual Task<Response> ReportAsync<T, U>(RequestReport<T, U> request, CancellationToken? cancellationToken = null)
        where T : IDataSource
        where U : ISignPDF
    {
        var path = string.IsNullOrWhiteSpace(_options.PdfApiReportPath)
                ? throw new ClientException("No PDF API report path")
                : _options.PdfApiReportPath;

        return RequestAsync<Response>(path, request, cancellationToken ?? CancellationToken.None);
    }

    /// <summary>
    /// Sends a asynchronous request to the PDF API to generate a report.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="ClientException"></exception>
    /// <exception cref="OperationCanceledException"></exception>
    public virtual Task<Response> ReportAsync<T>(RequestReport<T> request, CancellationToken? cancellationToken = null)
     where T : IDataSource
    {

        var path = string.IsNullOrWhiteSpace(_options.PdfApiReportPath)
                ? throw new ClientException("No PDF API report path")
                : _options.PdfApiReportPath;

        return RequestAsync<Response>(path, request, cancellationToken ?? CancellationToken.None);
    }

    /// <summary>
    /// Sends a asynchronous request to the PDF API to sign a document.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="ClientException"></exception>
    /// <exception cref="OperationCanceledException"></exception>
    public virtual Task<Response> SignAsync<T>(RequestSign<T> request, CancellationToken? cancellationToken = null)
    where T : ISignPDF
    {
        var path = string.IsNullOrWhiteSpace(_options.PdfApiSignPath)
                ? throw new ClientException("No PDF API sign path")
                : _options.PdfApiSignPath;

        return RequestAsync<Response>(path, request, cancellationToken ?? CancellationToken.None);
    }

    /// <summary>
    /// Sends a asynchronous request to the PDF API to print a document.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="ClientException"></exception>
    /// <exception cref="OperationCanceledException"></exception>
    public virtual Task<Response> PrintAsync(RequestPrint request, CancellationToken? cancellationToken = null)
    {
        var path = string.IsNullOrWhiteSpace(_options.PdfApiPrintPath)
                ? throw new ClientException("No PDF API print path")
                : _options.PdfApiPrintPath;

        return RequestAsync<Response>(path, request, cancellationToken ?? CancellationToken.None);
    }

    /// <summary>
    /// Sends a asynchronous request to the PDF API to convert a PDF document to an image.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="ClientException"></exception>
    /// <exception cref="OperationCanceledException"></exception>
    public virtual Task<PdfToImageResponse> ImageAsync(RequestPdfToImage request, CancellationToken? cancellationToken = null)
    {
        var path = string.IsNullOrWhiteSpace(_options.PdfApiImagePath)
                ? throw new ClientException("No PDF API image path")
                : _options.PdfApiImagePath;

        return RequestAsync<PdfToImageResponse>(path, request, cancellationToken ?? CancellationToken.None);
    }

    /// <summary>
    /// Tests asynchronous the communication with the PDF API.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public virtual async Task<Response> TestCommunicationAsync()
    {
        if (string.IsNullOrWhiteSpace(_options.TestCommunicationTest))
        {
            _logger.LogError("Test communication path is not configured");
            throw new InvalidOperationException("Test communication path is not configured");
        }

        using var httpResponse = await HttpClient.GetAsync(_options.TestCommunicationTest);

        var jsonResponse = await httpResponse.Content.ReadFromJsonAsync<Response>();

        if (jsonResponse is null)
        {
            _logger.LogError("Client pdf api test communication without response");
            throw new ClientException("Client pdf api test communication without response");
        }

        return jsonResponse;
    }

    /// <summary>
    /// Dispose
    /// </summary>
    public void Dispose()
    {
        try
        {
            HttpClient.CancelPendingRequests();
            HttpClientHandler.Dispose();
            HttpClient.Dispose();
            GC.SuppressFinalize(this);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error disposing PDF api client");
        }
    }

}

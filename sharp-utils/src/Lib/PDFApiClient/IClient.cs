/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using PChouse.Utils.PDFApiClient.Multicert;

namespace PChouse.Utils.PDFApiClient;

public interface IClient : IDisposable
{

    /// <summary>
    /// HTTP client for making requests to the PDF API
    /// </summary>
    HttpClient HttpClient { get; }

    /// <summary>
    /// The http client handler
    /// </summary>
    HttpClientHandler HttpClientHandler { get; }

    /// <summary>
    /// Sends a asynchronous request to the PDF API to convert a PDF document to an image.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="ClientException"></exception>
    /// <exception cref="OperationCanceledException"></exception>
    Task<PdfToImageResponse> ImageAsync(RequestPdfToImage request, CancellationToken? cancellationToken = null);

    /// <summary>
    /// Sends a asynchronous request to the PDF API to print a document.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="ClientException"></exception>
    /// <exception cref="OperationCanceledException"></exception>
    Task<Response> PrintAsync(RequestPrint request, CancellationToken? cancellationToken = null);

    /// <summary>
    /// Sends a asynchronous request to the PDF API to generate a report.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="ClientException"></exception>
    /// <exception cref="OperationCanceledException"></exception>
    Task<Response> ReportAsync<T, U>(RequestReport<T, U> request, CancellationToken? cancellationToken = null)
     where T : IDataSource
     where U : ISignPDF;

    /// <summary>
    /// Sends a asynchronous request to the PDF API to generate a report.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="ClientException"></exception>
    /// <exception cref="OperationCanceledException"></exception>
    Task<Response> ReportAsync<T>(RequestReport<T> request, CancellationToken? cancellationToken = null)
     where T : IDataSource;

    /// <summary>
    /// Sends a asynchronous request to the PDF API to sign a document.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="ClientException"></exception>
    /// <exception cref="OperationCanceledException"></exception>
    Task<Response> SignAsync<T>(RequestSign<T> request, CancellationToken? cancellationToken = null) where T : ISignPDF;

    /// <summary>
    /// Tests asynchronous the communication with the PDF API.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    Task<Response> TestCommunicationAsync();
}

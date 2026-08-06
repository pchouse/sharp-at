/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

namespace PChouse.Utils.PDFApiClient;

public class ClientOptions
{
    /// <summary>
    /// Default: https://127.0.0.1:4999/
    /// </summary>
    public virtual string? PdfApiEndpoint { get; set; } = "https://127.0.0.1:4999/";

    /// <summary>
    /// Default: sing
    /// </summary>
    public virtual string? PdfApiSignPath { get; set; } = "sign";

    /// <summary>
    /// Default: report
    /// </summary>
    public virtual string? PdfApiReportPath { get; set; } = "report";

    /// <summary>
    /// Default: print
    /// </summary>
    public virtual string? PdfApiPrintPath { get; set; } = "print";

    /// <summary>
    /// Default: image
    /// </summary>
    public virtual string? PdfApiImagePath { get; set; } = "image";

    /// <summary>
    /// Default: test
    /// </summary>
    public virtual string TestCommunicationTest { get; set; } = "test";

    /// <summary>
    /// The self sign certificate hash
    /// </summary>
    public virtual string EndpointSslCertificateHash { get; set; } = string.Empty;

}
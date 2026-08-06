/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Text.Json.Serialization;

namespace PChouse.Utils.PDFApiClient;


public partial class RequestReport<T, U>
: RequestReport<T>
where T : IDataSource
where U : ISignPDF
{

    [JsonPropertyName("signPDF")]
    public virtual U? SignPDF { get; set; } = default;

}

public partial class RequestReport<T>
where T : IDataSource
{
    [JsonRequired]
    [JsonPropertyName("report")]
    public virtual required string Report { get; set; }

    [JsonPropertyName("printerName")]
    public virtual string? PrinterName { get; set; } = default;

    [JsonRequired]
    [JsonPropertyName("reportType")]
    public virtual required ReportType ReportType { get; set; }

    [JsonRequired]
    [JsonPropertyName("parameters")]
    public virtual ICollection<Parameter> Parameters { get; set; } = new List<Parameter>();

    [JsonPropertyName("metadata")]
    public virtual Metadata? Metadata { get; set; } = default;

    [JsonRequired]
    [JsonPropertyName("dataSource")]
    public virtual required T DataSource { get; set; }

    [JsonPropertyName("copies")]
    public virtual int Copies { get; set; } = 1;

    [JsonRequired]
    [JsonPropertyName("encoding")]
    public virtual string Encoding { get; set; } = "UTF-8";

    [JsonPropertyName("pdfProperties")]
    public virtual PdfProperties? PdfProperties { get; set; } = default;

    [JsonPropertyName("afterPrintOperations")]
    public virtual ICollection<AfterPrintOperations> AfterPrintOperations { get; set; } = new List<AfterPrintOperations>();

    [JsonExtensionData]
    public virtual IDictionary<string, object> AdditionalProperties { get; set; } = new Dictionary<string, object>();
}


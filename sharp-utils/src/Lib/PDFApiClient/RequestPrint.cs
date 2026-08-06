/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Text.Json.Serialization;

namespace PChouse.Utils.PDFApiClient;

public partial class RequestPrint
{
    [JsonRequired]
    [JsonPropertyName("pdfBase64")]
    public virtual required string PdfBase64 { get; set; }

    [JsonRequired]
    [JsonPropertyName("printerName")]
    public virtual required string PrinterName { get; set; }

    [JsonRequired]
    [JsonPropertyName("afterPrintOperations")]
    public required ICollection<afterPrintOperations> AfterPrintOperations { get; set; }

    [JsonExtensionData]
    public IDictionary<string, object> AdditionalProperties { get; set; } = new Dictionary<string, object>();

}


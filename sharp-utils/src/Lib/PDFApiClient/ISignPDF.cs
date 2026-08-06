/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Text.Json.Serialization;

namespace PChouse.Utils.PDFApiClient;

public abstract class ISignPDF
{

    [JsonExtensionData]
    public virtual IDictionary<string, object> AdditionalProperties { get; set; } = new Dictionary<string, object>();

}


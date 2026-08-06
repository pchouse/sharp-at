/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Text.Json.Serialization;

namespace PChouse.Utils.PDFApiClient.Multicert;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TemplateType
{
    /// <summary>
    /// ONE_OFF: Visible signature template in a velocity format will be provided in the oneOffTemplate request attribute.
    /// </summary>
    [JsonStringEnumMemberName(nameof(ONE_OFF))]
    ONE_OFF,

    /// <summary>
    /// SERVICE_CUSTOMIZATION: Uses the issuer service default visible signature template or if not found falls back on the system template.
    /// </summary>
    [JsonStringEnumMemberName(nameof(SERVICE_CUSTOMIZATION))]
    SERVICE_CUSTOMIZATION
}


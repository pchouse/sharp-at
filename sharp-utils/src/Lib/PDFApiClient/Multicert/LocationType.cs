/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Text.Json.Serialization;

namespace PChouse.Utils.PDFApiClient.Multicert;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum LocationType
{
    /// <summary>
    /// SIGN_FIELD: Use a signature field present in the document. The signature field name should be set in the locationValue request attribute.
    /// </summary>
    [JsonStringEnumMemberName(nameof(SIGN_FIELD))]
    SIGN_FIELD,

    /// <summary>
    /// COORDINATE: Use a coordinate to find the correct document position. Supported format: page=<page_number>,x=<lower_left_x>,y=<lower_left_y>
    /// </summary>
    [JsonStringEnumMemberName(nameof(COORDINATE))]
    COORDINATE,

    /// <summary>
    /// PLACEHOLDER: Use a placeholder (unique word). System will scan the document for the given placeholder and stop in the first occurrence. The lower left part of the word will be used as a coordinate to start the drawing of the signature canvas from top/left orientation. If the placeholder is not found, an error will be thrown and the signature will not be applied. 
    /// The placeholder value should be set in the locationValue request attribute.
    /// </summary>
    [JsonStringEnumMemberName(nameof(PLACEHOLDER))]
    PLACEHOLDER
}


/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Text.Json.Serialization;

namespace PChouse.Utils.PDFApiClient.Multicert;

public class MulticertSignRequest : ISignPDF
{


    [JsonPropertyName("type")]
    public string Type { get; } = "multicert";

    /// <summary>
    /// The ID of the certificate to be used for signing.
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("username")]
    public virtual required string UserName { get; set; }

    /// <summary>
    /// The password for the certificate to be used for signing.
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("password")]
    public virtual required string Password { get; set; }

    /// <summary>
    /// maxLength: 100
    /// minLength: 1
    /// example: Ext_Ref
    /// Unique external reference id set by the client service.
    /// This should be used to query the document status if an infrastructure error occurs during operation 
    /// execution that could leave its status incoherent between client and server.
    /// A good approach is to have the service client id prefixed to the external reference to 
    /// avoid collisions with other external references already registered in the system.
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("externalReference")]
    public virtual string ExternalReference { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// The description of the document to be signed.
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("description")]
    public virtual required string Description { get; set; }

    /// <summary>
    /// maxLength: 128
    /// minLength: 1
    /// example: document_example.pdf
    /// Name of the document.
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("fileName")]
    public virtual required string FileName { get; set; }

    /// <summary>
    /// The signature information to be applied to the document.
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("signatureInfo")]
    public virtual required SignatureInfo SignatureInfo { get; set; }

    /// <summary>
    /// Defines if the request is a test request or not.
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("isTest")]
    public virtual required bool IsTest { get; set; }

}


/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Text.Json.Serialization;

namespace PChouse.Utils.PDFApiClient;


public class Database : IDataSource
{

    [JsonPropertyName("type")]
    public virtual string Type { get; } = "database";

    [JsonRequired]
    [JsonPropertyName("provider")]
    public virtual Provider Provider { get; set; }

    [JsonRequired]
    [JsonPropertyName("serverAddress")]
    public virtual required string ServerAddress { get; set; }

    [JsonPropertyName("serverPort")]
    public virtual int? ServerPort { get; set; } = default;

    [JsonPropertyName("databaseSchema")]
    public virtual string? DatabaseSchema { get; set; } = default;

    [JsonPropertyName("user")]
    public virtual string? User { get; set; } = default;

    [JsonPropertyName("password")]
    public virtual string? Password { get; set; } = default;

    [JsonRequired]
    [JsonPropertyName("encrypt")]
    public virtual bool Encrypt { get; set; } = true;

    [JsonRequired]
    [JsonPropertyName("trustServerCertificate")]
    public virtual bool TrustServerCertificate { get; set; } = true;

}


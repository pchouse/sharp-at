/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/


namespace PChouse.Utils.JWT;

public class TokenOptions
{
    /// <summary>
    /// The life time of the access token in minutes
    /// </summary>
    public int AccessTokenLifeTime { get; set; } = 59;

    /// <summary>
    /// The life time of the refresh token in minutes
    /// </summary>
    public int RefreshTokenLifeTime { get; set; } = 59 * 24;

    /// <summary>
    /// The short time access life time token in minutes
    /// </summary>
    public int ShortTokenLifeTime { get; set; } = 2;

    /// <summary>
    /// The token issuer
    /// </summary>
    public string TokenIssuer { get; set; } = "api";

}
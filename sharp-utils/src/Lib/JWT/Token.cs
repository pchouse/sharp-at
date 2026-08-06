/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;

namespace PChouse.Utils.JWT;

/// <summary>
/// Class to handle JWT
/// </summary>
public class Token : IToken
{

    /// <summary>
    /// The logger
    /// </summary>
    protected ILogger Logger;

    /// <summary>
    /// The token options
    /// </summary>
    protected TokenOptions TokenOptions;

    /// <summary>
    /// Encryption key 
    /// </summary>
    private string _key;

    /// <summary>
    /// Token
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="tokenOptions"></param>
    /// <param name="key"></param>
    public Token(ILogger<Token> logger, TokenOptions tokenOptions, string key)
    {
        Logger = logger;
        TokenOptions = tokenOptions;
        _key = key;
    }

    /// <summary>
    /// Generate a JWT 
    /// 
    /// claimsFactory should return something like:
    /// var claims = new[]
    ///    {
    ///        new Claim(JwtRegisteredClaimNames.Sub, userAccount.ToJson()),
    ///        new Claim(JwtRegisteredClaimNames.Aud, company.ToJson()),
    ///        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
    ///        new Claim("roles", System.Text.Json.JsonSerializer.Serialize(
    ///           new[] { "user-role",  "admin-role" } 
    ///         ))
    ///     };
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="claimsFactory"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public Task<string?> GenerateTokenAsync(JwtType type, Action<List<Claim>> claimsFactory)
    {
        var claims = new List<Claim>();
        claimsFactory.Invoke(claims);
        return GenerateTokenAsync(type, claims.ToArray());
    }

    /// <summary>
    /// Generate a JWT 
    /// 
    /// claimsFactory should return something like:
    /// var claims = new[]
    ///    {
    ///        new Claim(JwtRegisteredClaimNames.Sub, userAccount.ToJson()),
    ///        new Claim(JwtRegisteredClaimNames.Aud, company.ToJson()),
    ///        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
    ///        new Claim("roles", System.Text.Json.JsonSerializer.Serialize(
    ///           new[] { "user-role",  "admin-role" } 
    ///         ))
    ///     };
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="claimsFactory"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public Task<string?> GenerateTokenAsync(JwtType type, Func<Claim[]> claimsFactory)
    {
        return GenerateTokenAsync(type, claimsFactory.Invoke());
    }

    /// <summary>
    /// Generate a JWT 
    /// 
    /// claims array should be something like:
    /// var claims = new[]
    ///    {
    ///        new Claim(JwtRegisteredClaimNames.Sub, userAccount.ToJson()),
    ///        new Claim(JwtRegisteredClaimNames.Aud, company.ToJson()),
    ///        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
    ///        new Claim("roles", System.Text.Json.JsonSerializer.Serialize(
    ///           new[] { "user-role",  "admin-role" } 
    ///         ))
    ///     };
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="claimsFactory"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public async Task<string?> GenerateTokenAsync(JwtType type, Claim[] claims)
    {
        var expires = type switch
        {
            JwtType.ACCESS => DateTime.UtcNow.AddMinutes(TokenOptions.AccessTokenLifeTime),
            JwtType.REFRESH => DateTime.UtcNow.AddMinutes(TokenOptions.RefreshTokenLifeTime),
            JwtType.SHORT_TIME => DateTime.UtcNow.AddMinutes(TokenOptions.ShortTokenLifeTime),
            _ => throw new InvalidOperationException("JWT Key not found in configuration.")
        };

        var token = new JwtSecurityToken(
            issuer: TokenOptions.TokenIssuer,
            null,
            claims: claims,
            expires: expires,
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key)),
                SecurityAlgorithms.HmacSha256
              )
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }

    /// <summary>
    /// Parse the token in the http request header
    /// </summary>
    /// <param name="httpContextAccessor"></param>
    /// <returns></returns>
    public JwtSecurityToken? HttpRequestToken(IHttpContextAccessor? httpContextAccessor)
    {
        Logger.LogTrace("Get token");

        var token = httpContextAccessor?.HttpContext?.Request.Headers[HeaderNames.Authorization]
            .FirstOrDefault(h => h?.StartsWith("Bearer ") == true)
            ?.Replace("Bearer ", "");

        return string.IsNullOrWhiteSpace(token)
            ? null
            : new JwtSecurityTokenHandler().ReadJwtToken(token);
    }

}
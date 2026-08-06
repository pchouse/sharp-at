/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace PChouse.Utils.JWT;

/// <summary>
/// Class to handle JWT
/// </summary>
public interface IToken
{

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
    public Task<string?> GenerateTokenAsync(JwtType type, Action<List<Claim>> claimsFactory);

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
    public Task<string?> GenerateTokenAsync(JwtType type, Func<Claim[]> claimsFactory);

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
    public Task<string?> GenerateTokenAsync(JwtType type, Claim[] claims);

    /// <summary>
    /// Parse the token in the http request header
    /// </summary>
    /// <param name="httpContextAccessor"></param>
    /// <returns></returns>
    public JwtSecurityToken? HttpRequestToken(IHttpContextAccessor? httpContextAccessor);

}
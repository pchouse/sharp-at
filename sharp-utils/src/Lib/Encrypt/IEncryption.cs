/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Security.Cryptography;
using System.Text;

namespace PChouse.Utils.Encrypt;

/// <summary>
/// Encrypt
/// </summary>
public interface IEncryption
{

    /// <summary>
    /// Encrypt a string
    /// </summary>
    /// <param name="literal"></param>
    /// <returns></returns>
    /// <exception cref="EncryptException"></exception>
    public Task<string> EncryptAsync(string literal);

    /// <summary>
    /// Decrypt a string
    /// </summary>
    /// <returns></returns>
    public Task<string> DecryptAsync(string encrypted);

    /// <summary>
    /// Hash a string with SHA256
    /// </summary>
    /// <param name="salt">To add at the end of the string</param>
    /// <returns></returns>
    public Task<string> Sha256HashAsync(string literal, string salt);

}

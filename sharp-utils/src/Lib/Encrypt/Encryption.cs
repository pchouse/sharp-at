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
public class Encryption : IEncryption
{

    /// <summary>
    /// The encrypt keys
    /// </summary>
    private IEncryptKey _encryptKey;

    /// <summary>
    /// Encrypt strings
    /// </summary>
    /// <param name="encryptKey"></param>
    /// <exception cref="EncryptException"></exception>
    public Encryption(IEncryptKey encryptKey)
    {
        if (Encoding.UTF8.GetBytes(encryptKey.Key).Length != 32)
        {
            throw new EncryptException("Key encryption must be 32 byte long");
        }

        if (Encoding.UTF8.GetBytes(encryptKey.Iv).Length != 16)
        {
            throw new EncryptException("Initialization vector must be 16 byte long");
        }

        _encryptKey = encryptKey;
    }

    /// <summary>
    /// Encrypt a string
    /// </summary>
    /// <param name="literal"></param>
    /// <returns></returns>
    /// <exception cref="EncryptException"></exception>
    public async Task<string> EncryptAsync(string literal)
    {
        if (string.IsNullOrWhiteSpace(literal))
        {
            throw new EncryptException("The string to encrypt is empty");
        }

        using var aes = Aes.Create();

        aes.Key = Encoding.UTF8.GetBytes(_encryptKey.Key);
        aes.IV = Encoding.UTF8.GetBytes(_encryptKey.Iv);

        var encryption = aes.CreateEncryptor(aes.Key, aes.IV);

        await using var ms = new MemoryStream();
        await using var cs = new CryptoStream(ms, encryption, CryptoStreamMode.Write);
        await using var sw = new StreamWriter(cs);

        await sw.WriteAsync(literal);

        await sw.FlushAsync();
        await cs.FlushFinalBlockAsync();
        await ms.FlushAsync();

        var byteArray = ms.ToArray();

        return Convert.ToBase64String(byteArray);
    }

    /// <summary>
    /// Decrypt a string, this static method is useful if you need to decrypt at start up
    /// </summary>
    /// <param name="encrypted"></param>
    /// <param name="encryptKey"></param>
    /// <returns></returns>
    /// <exception cref="EncryptException"></exception>
    public static async Task<string> DecryptAsync(string encrypted, IEncryptKey encryptKey)
    {
        if (string.IsNullOrWhiteSpace(encrypted))
        {
            throw new EncryptException("The encrypted string is empty");
        }

        using var aes = Aes.Create();

        aes.Key = Encoding.UTF8.GetBytes(encryptKey.Key);
        aes.IV = Encoding.UTF8.GetBytes(encryptKey.Iv);

        var decrypt = aes.CreateDecryptor(aes.Key, aes.IV);

        await using var ms = new MemoryStream(Convert.FromBase64String(encrypted));
        await using var cs = new CryptoStream(ms, decrypt, CryptoStreamMode.Read);

        using var sr = new StreamReader(cs);
        return await sr.ReadToEndAsync();
    }

    /// <summary>
    /// Decrypt a string
    /// </summary>
    /// <returns></returns>
    public Task<string> DecryptAsync(string encrypted)
    {
        return DecryptAsync(encrypted, _encryptKey);
    }

    /// <summary>
    /// Hash a string with SHA256
    /// </summary>
    /// <param name="salt">To add at the end of the string</param>
    /// <returns></returns>
    public Task<string> Sha256HashAsync(string literal, string salt)
    {
        return Task.Run(() =>
        {
            var bytesString = Encoding.UTF8.GetBytes($"{literal}{salt}");

            var bytes = SHA256.HashData(bytesString);

            var sb = new StringBuilder();

            foreach (var b in bytes)
            {
                sb.Append(b.ToString("x2"));
            }

            var hash = sb.ToString();

            return hash;
        });
    }

}

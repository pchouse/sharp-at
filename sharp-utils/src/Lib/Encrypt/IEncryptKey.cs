/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/


namespace PChouse.Utils.Encrypt;

public interface IEncryptKey
{
    /// <summary>
    /// A 32 bytes (256 bits) key size
    /// </summary>
    public string Key { get; }

    /// <summary>
    /// A 16 bytes (128 bits) initialization vector
    /// </summary>
    public string Iv { get; }

}
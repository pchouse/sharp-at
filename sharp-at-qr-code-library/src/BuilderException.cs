

using System.Security.Cryptography.X509Certificates;


/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/
namespace PChouse.AT.QRCode.Library;

/// <summary>
/// QRCode Builder Exception
/// </summary>
public class BuilderException : Exception
{
    /// <summary>
    /// Error
    /// </summary>
    public virtual Error? Error { get; set; }

    /// <summary>
    /// Builder exception
    /// </summary>
    /// <param name="message"></param>
    public BuilderException(string? message) : base(message)
    {

    }

}
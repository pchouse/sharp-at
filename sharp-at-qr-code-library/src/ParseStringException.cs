/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/
namespace PChouse.AT.QRCode.Library;

/// <summary>
/// 
/// </summary>
public class ParseStringException : Exception
{

    /// <summary>
    /// The field where the error was found
    /// </summary>
    public virtual string Field { get; set; } = string.Empty;

    /// <summary>
    /// QRCode string parse Exception
    /// </summary>
    /// <param name="message"></param>
    public ParseStringException(string? message) : base(message)
    {

    }

}

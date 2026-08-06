/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using PChouse.AT.QRCode.Library;

namespace PChouse.AT.QRCode;

/// <summary>
/// The QR code string builder
/// </summary>
public interface IBuilder
{
    /// <summary>
    /// Build the QRcode string
    /// </summary>
    /// <param name="errors"></param>
    /// <param name="document"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    string BuildString(ref List<ErrorField> errors, ADocument document, BuilderOptions options);

    /// <summary>
    /// Build the QRcode string 
    /// </summary>
    /// <param name="document"></param>
    /// <param name="options"></param>
    /// <param name="errors"></param>
    /// <returns></returns>
    /// <exception cref="BuilderException"></exception>
    string BuildString(ADocument document, BuilderOptions options, out List<ErrorField> errors);

    /// <summary>
    /// Build the String to the QRCode asynchronous
    /// </summary>
    /// <param name="document"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    Task<BuildStringAsyncResult> BuildStringAsync(ADocument document, BuilderOptions options);
    
}
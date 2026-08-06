/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/
namespace PChouse.AT.QRCode.Library;

/// <summary>
/// QRCode Builder Exception
/// </summary>
public class BuilderFieldException : BuilderException
{
    /// <summary>
    /// The error founded
    /// </summary>
    new public virtual required Error Error { get; set; }

    /// <summary>
    /// The field where the error was found
    /// </summary>
    public virtual required FieldAttribute Field { get; set; }

    /// <summary>
    /// Builder field exception
    /// </summary>
    public BuilderFieldException() : base(null)
    {

    }

    /// <summary>
    /// Builder field exception
    /// </summary>
    /// <param name="message"></param>
    public BuilderFieldException(string? message) : base(message)
    {

    }

    /// <summary>
    /// Builder field exception
    /// </summary>
    /// <param name="message"></param>
    /// <param name="error"></param>
    /// <param name="field"></param>
    public BuilderFieldException(string? message, Error error, FieldAttribute field) : base(message)
    {
        Error = error;
        Field = field;
    }

    /// <summary>
    /// Builder field exception
    /// </summary>
    /// <param name="error"></param>
    /// <param name="field"></param>
    public BuilderFieldException(Error error, FieldAttribute field) : base(null)
    {
        Error = error;
        Field = field;
    }
}
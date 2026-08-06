/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Reflection;

namespace PChouse.AT.QRCode.Library;

/// <summary>
/// The properties class
/// </summary>
public record Properties
{
    /// <summary>
    /// The class field property info (From Type reflection)
    /// </summary>
    public virtual required PropertyInfo PropertyInfo { get; set; }

    /// <summary>
    /// The QRcode field properties (From the field attributes)
    /// </summary>
    public virtual required FieldAttribute Field { get; set; }

}

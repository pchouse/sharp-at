/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using PChouse.AT.Generics;

namespace PChouse.AT.QRCode.Library;

/// <summary>
/// Parsers for document type
/// </summary>
public static class DocumentTypeExtender
{

    /// <summary>
    /// Parse from InvoiceType
    /// </summary>
    /// <param name="invoiceType"></param>
    /// <returns></returns>
    public static DocumentType Parse(InvoiceType invoiceType)
    {
        return Enum.Parse<DocumentType>(invoiceType.ToString());
    }

    /// <summary>
    /// Parse from Movement type
    /// </summary>
    /// <param name="movementType"></param>
    /// <returns></returns>
    public static DocumentType Parse(MovementType movementType)
    {
        return Enum.Parse<DocumentType>(movementType.ToString());
    }

    /// <summary>
    /// Parse from WorkType
    /// </summary>
    /// <param name="workType"></param>
    /// <returns></returns>
    public static DocumentType Parse(WorkType workType)
    {
        return Enum.Parse<DocumentType>(workType.ToString());
    }

    /// <summary>
    /// Parse from payment type
    /// </summary>
    /// <param name="paymentType"></param>
    /// <returns></returns>
    public static DocumentType Parse(PaymentType paymentType)
    {
        return Enum.Parse<DocumentType>(paymentType.ToString());
    }

}
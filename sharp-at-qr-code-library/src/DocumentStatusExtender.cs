/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using PChouse.AT.Generics;

namespace PChouse.AT.QRCode.Library;

/// <summary>
/// Parsers for document status
/// </summary>
public static class DocumentStatusExtender
{

    /// <summary>
    /// Parse from InvoiceStatus
    /// </summary>
    /// <param name="invoiceStatus"></param>
    /// <returns></returns>
     public static DocumentStatus Parse(InvoiceStatus invoiceStatus)
    {
        return Enum.Parse<DocumentStatus>(invoiceStatus.ToString());
    }

    /// <summary>
    /// Parse from MovementStatus
    /// </summary>
    /// <param name="movementStatus"></param>
    /// <returns></returns>
    public static DocumentStatus Parse(MovementStatus movementStatus)
    {
        return Enum.Parse<DocumentStatus>(movementStatus.ToString());
    }

    /// <summary>
    /// Parse from WorkStatus
    /// </summary>
    /// <param name="workStatus"></param>
    /// <returns></returns>
    public static DocumentStatus Parse(WorkStatus workStatus)
    {
        return Enum.Parse<DocumentStatus>(workStatus.ToString());
    }

    /// <summary>
    /// Parse from PaymentStatus
    /// </summary>
    /// <param name="paymentStatus"></param>
    /// <returns></returns>
    public static DocumentStatus Parse(PaymentStatus paymentStatus)
    {
        return Enum.Parse<DocumentStatus>(paymentStatus.ToString());
    }

}
/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Net.Mail;
using System.Net.Mime;

namespace PChouse.Utils.EMail;

public interface IMailer : IDisposable, IAsyncDisposable
{

    /// <summary>
    /// Email subject
    /// </summary>
    string EmailSubject { get; set; }

    /// <summary>
    /// Email body
    /// </summary>
    string EmailBody { get; set; }

    /// <summary>
    /// Email to
    /// </summary>
    MailAddressCollection EmailTo { get; }

    /// <summary>
    /// Email CC
    /// </summary>
    MailAddressCollection EmailCc { get; }

    /// <summary>
    /// Email BCC
    /// </summary>
    MailAddressCollection EmailBcc { get; }

    /// <summary>
    /// Gets the exception that occurred during the operation, if any.
    /// </summary>
    Exception? SendError { get; }

    /// <summary>
    /// Mail message engine
    /// </summary>
    MailMessage MailMessage { get; }

    /// <summary>
    /// SMTP transport
    /// </summary>
    SmtpClient SmtpClient { get; }

    /// <summary>
    /// Send the mail asynchronously
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    Task SendAsync();

    /// <summary>
    /// Send the email asynchronously
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <exception cref="EmailException"></exception>
    /// <exception cref="Exception"></exception>
    Task SendAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Add attachment to the email
    /// </summary>
    /// <param name="bytes">the file content</param>
    /// <param name="fileName">The file name</param>
    /// <param name="contentType">The mime type of the file</param>
    void AddAttachment(byte[] bytes, string fileName, ContentType contentType);

    /// <summary>
    /// Add attachment to the email
    /// </summary>
    /// <param name="base64">The document as base64 encoded string</param>
    /// <param name="fileName">The file name</param>
    /// <param name="contentType">The mime type of the file</param>
    void AddAttachment(string base64, string fileName, ContentType contentType);

    /// <summary>
    /// Add attachment to the email
    /// </summary>
    /// <param name="base64">The document as base64 encoded string</param>
    /// <param name="fileName">The file name</param>
    /// <param name="contentType">The mime type of the file</param>
    /// <param name="cancellationToken"></param>
    Task AddAttachmentPath(string path, string fileName, ContentType contentType, CancellationToken? cancellationToken = null);

}
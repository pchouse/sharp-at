/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using Microsoft.Extensions.Logging;

namespace PChouse.Utils.EMail;

public partial class Mailer : IMailer, IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Logger
    /// </summary>
    private ILogger<Mailer> _logger;

    /// <summary>
    /// Email subject
    /// </summary>
    public string EmailSubject { get; set; } = string.Empty;

    /// <summary>
    /// Email body
    /// </summary>
    public string EmailBody { get; set; } = string.Empty;

    /// <summary>
    /// Email to
    /// </summary>
    public MailAddressCollection EmailTo { get; } = new MailAddressCollection();

    /// <summary>
    /// Email CC
    /// </summary>
    public MailAddressCollection EmailCc { get; } = new MailAddressCollection();

    /// <summary>
    /// Email BCC
    /// </summary>
    public MailAddressCollection EmailBcc { get; } = new MailAddressCollection();

    /// <summary>
    /// Gets the exception that occurred during the operation, if any.
    /// </summary>
    public Exception? SendError { get; private set; } = default;

    /// <summary>
    /// Mail message engine
    /// </summary>
    public MailMessage MailMessage { get; } = new();

    /// <summary>
    /// SMTP transport
    /// </summary>
    public SmtpClient SmtpClient { get; } = new();

    /// <summary>
    /// Mailer
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="options"></param>
    /// <exception cref="MailerException"></exception>
    protected Mailer(ILogger<Mailer> logger, MailerOptions options)
    {
        _logger = logger;

        MailMessage.IsBodyHtml = options.IsHtml;
        if (string.IsNullOrWhiteSpace(options.SmtpServer) is false)
        {
            SmtpClient.Host = options.SmtpServer;
        }

        if (options.SmtpPort < 1 || options.SmtpPort > 65535)
        {
            throw new MailerException("Port number is invalid");
        }

        SmtpClient.Port = options.SmtpPort;
        SmtpClient.EnableSsl = options.EnableSsl;


        var networkCredential = new NetworkCredential(
            options.UserName,
            options.SecurePassword
        );

        SmtpClient.Credentials = networkCredential;

        if (options.FromAddress is not null)
        {
            MailMessage.From = options.FromAddress;
            MailMessage.Sender = options.FromAddress;
        }

        if (options.SenderAddress is not null)
        {
            MailMessage.Sender = options.SenderAddress;
        }

        SmtpClient.DeliveryFormat = SmtpDeliveryFormat.International;
        SmtpClient.UseDefaultCredentials = false;
    }

    /// <summary>
    /// Sen the email asynchronously
    /// </summary>
    /// <returns></returns>
    public virtual Task SendAsync()
    {
        return SendAsync(CancellationToken.None);
    }

    /// <summary>
    /// Send the email asynchronously
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <exception cref="EmailException"></exception>
    /// <exception cref="Exception"></exception>
    /// <exception cref="ArgumentOutOfRangeArgument">If port number is out of range<exception>
    public virtual async Task SendAsync(CancellationToken cancellationToken)
    {
        _logger.LogDebug($"Method {nameof(SendAsync)} was called");

        if (string.IsNullOrWhiteSpace(SmtpClient.Host))
        {
            _logger.LogError("SMTP server address is not configured in options");
            throw new MailerException("SMTP server address is not configured in options");
        }

        if (MailMessage.From is null)
        {
            _logger.LogError("SMTP from address not set");
            throw new MailerException("SMTP from address not set");
        }

        if (MailMessage.Sender is null)
        {
            MailMessage.Sender = MailMessage.From;
        }

        if (string.IsNullOrWhiteSpace(EmailBody))
        {
            const string message = "Is not possible to send the email because does not have body";
            _logger.LogDebug(message);
            throw new MailerException(message);
        }

        if (string.IsNullOrEmpty(EmailSubject))
        {
            const string message = "Is not possible to send email because does not have subject";
            _logger.LogDebug(message);
            throw new MailerException(message);
        }

        if (EmailTo.Count == 0)
        {
            const string message = "Email without 'TO' address";
            _logger.LogDebug(message);
            throw new MailerException(message);
        }

        foreach (var email in EmailTo) MailMessage.To.Add(email);
        foreach (var email in EmailCc) MailMessage.CC.Add(email);
        foreach (var email in EmailBcc) MailMessage.Bcc.Add(email);

        MailMessage.Subject = EmailSubject;
        MailMessage.Body = EmailBody;

        if (cancellationToken.IsCancellationRequested)
        {
            _logger.LogWarning("The send email was cancelled by the cancellation token");
            return;
        }

        if (cancellationToken.IsCancellationRequested)
        {
            _logger?.LogWarning("The send email was cancelled by the cancellation token");
            return;
        }

        bool? error = null;

        SmtpClient.SendCompleted += (_, e) =>
        {
            try
            {
                if (e.Error != null)
                {
                    _logger?.LogError($"Error sending email {e.Error}");
                    error = true;
                }
                else
                {
                    error = false;
                }

                SendError = e.Error;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error sending email {ex.InnerException?.Message ?? ex.Message}");
            }
        };

        SmtpClient.SendAsync(MailMessage, Guid.NewGuid());

        while (!error.HasValue)
        {
            if (!cancellationToken.IsCancellationRequested) continue;
            SmtpClient.SendAsyncCancel();
            _logger.LogWarning("The send email was cancelled by the cancellation token");
            return;
        }

        if (error.Value)
        {
            _logger.LogError($"Error sending email: {SendError?.Message ?? "unknown error"}", SendError);
            throw new MailerException(
                $"Error sending email: {SendError?.Message ?? "unknown error"}"
             );
        }
    }

    /// <summary>
    /// Add attachment to the email
    /// </summary>
    /// <param name="bytes">the file content</param>
    /// <param name="fileName">The file name</param>
    /// <param name="contentType">The mime type of the file</param>
    public virtual void AddAttachment(byte[] bytes, string fileName, ContentType contentType)
    {
        var stream = new MemoryStream(bytes);

        var attachment = new Attachment(stream, null, contentType.MediaType);

        var disposition = attachment.ContentDisposition;

        disposition?.ModificationDate = new DateTime();
        disposition?.ReadDate = new DateTime();
        disposition?.FileName = fileName;
        disposition?.Size = bytes.Length;
        disposition?.DispositionType = DispositionTypeNames.Attachment;

        MailMessage.Attachments.Add(attachment);
    }

    /// <summary>
    /// Add attachment to the email
    /// </summary>
    /// <param name="base64">The document as base64 encoded string</param>
    /// <param name="fileName">The file name</param>
    /// <param name="contentType">The mime type of the file</param>
    public virtual void AddAttachment(string base64, string fileName, ContentType contentType)
    {
        AddAttachment(Convert.FromBase64String(base64), fileName, contentType);
    }

    /// <summary>
    /// Add attachment to the email
    /// </summary>
    /// <param name="base64">The document as base64 encoded string</param>
    /// <param name="fileName">The file name</param>
    /// <param name="contentType">The mime type of the file</param>
    /// <param name="cancellationToken"></param>
    public virtual async Task AddAttachmentPath(string path, string fileName, ContentType contentType, CancellationToken? cancellationToken = null)
    {
        AddAttachment(
            await File.ReadAllBytesAsync(path, cancellationToken ?? CancellationToken.None),
            fileName,
            contentType
         );
    }

    /// <summary>
    /// Dispose synchronously
    /// </summary>
    public void Dispose()
    {
        DisposeAsync().AsTask().ConfigureAwait(false).GetAwaiter().GetResult();
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Dispose asynchronously
    /// </summary>
    public async ValueTask DisposeAsync()
    {
        await CastAndDispose(SmtpClient);
        await CastAndDispose(MailMessage);

        GC.SuppressFinalize(this);

        return;

        static async ValueTask CastAndDispose(IDisposable resource)
        {
            if (resource is IAsyncDisposable resourceAsyncDisposable)
                await resourceAsyncDisposable.DisposeAsync();
            else
                resource.Dispose();
        }
    }

}

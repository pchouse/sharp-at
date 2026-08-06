/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Net.Mail;


namespace PChouse.Utils.EMail;

public interface IMailerFactory
{

    /// <summary>
    /// Create a instance of Mailer with the default MailerOptions
    /// </summary>
    /// <returns></returns>
    IMailer Instance();

    /// <summary>
    /// Create a instance of Mailer and set the Mailer Options
    /// </summary>
    /// <param name="options"></param>
    /// <returns></returns>
    IMailer Instance(Action<MailerOptions> options);

    /// <summary>
    /// Create a instance of Mailer with MailerOptions
    /// </summary>
    /// <param name="options"></param>
    /// <returns></returns>
    IMailer Instance(MailerOptions options);

    /// <summary>
    ///  Create a instance of Mailer and set the Mailer Options
    /// </summary>
    /// <param name="options"></param>
    /// <param name="smtpClient"></param>
    /// <param name="mailMessage"></param>
    /// <returns></returns>
    IMailer Instance(Action<MailerOptions>? options = null, Action<SmtpClient>? smtpClient = null, Action<MailMessage>? mailMessage = null);

}
/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Net.Mail;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace PChouse.Utils.EMail;

public partial class Mailer
{
    /// <summary>
    /// Mail factory
    /// </summary>
    /// <param name="service"></param>
    public class MailerFactory(IServiceProvider service) : IMailerFactory
    {

        /// <summary>
        /// Create a instance of Mailer with the default MailerOptions
        /// </summary>
        /// <returns></returns>
        public virtual IMailer Instance()
        {
            return Instance(new MailerOptions());
        }

        /// <summary>
        /// Create a instance of Mailer and set the Mailer Options
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public virtual IMailer Instance(Action<MailerOptions> options)
        {
            var mailerOptions = new MailerOptions();
            options.Invoke(mailerOptions);
            return Instance(mailerOptions);
        }

        /// <summary>
        /// Create a instance of Mailer with MailerOptions
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public virtual IMailer Instance(MailerOptions options)
        {
            var mailer = new Mailer(
                service.GetRequiredService<ILogger<Mailer>>(),
                options
            );

            return mailer;
        }

        /// <summary>
        ///  Create a instance of Mailer and set the Mailer Options
        /// </summary>
        /// <param name="options"></param>
        /// <param name="smtpClient"></param>
        /// <param name="mailMessage"></param>
        /// <returns></returns>
        public virtual IMailer Instance(
            Action<MailerOptions>? options = null,
            Action<SmtpClient>? smtpClient = null,
            Action<MailMessage>? mailMessage = null
            )
        {
            var mailerOptions = new MailerOptions();
            options?.Invoke(mailerOptions);
            var instance = Instance(mailerOptions);
            smtpClient?.Invoke(instance.SmtpClient);
            mailMessage?.Invoke(instance.MailMessage);
            return instance;
        }

    }
}
/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Net.Mail;
using System.Security;

namespace PChouse.Utils.EMail;

public class MailerOptions
{
    public string SmtpServer { get; set; } = string.Empty;


    public int SmtpPort { get; set; } = default;


    public bool EnableSsl { get; set; } = true;

    public string UserName { get; set; } = string.Empty;

    public SecureString? SecurePassword { get; set; } = default;

    public string Password
    {
        set
        {
            SecurePassword = new();
            foreach (var c in value) SecurePassword.AppendChar(c);
        }
    }

    public MailAddress? FromAddress { get; set; } = default;

    /// <summary>
    /// Set if is different from 'FromAddress' 
    /// </summary>
    public MailAddress? SenderAddress { get; set; } = default;

    public bool IsHtml { get; set; } = true;

}
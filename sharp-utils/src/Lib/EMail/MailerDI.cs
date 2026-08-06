/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using Microsoft.Extensions.DependencyInjection;

namespace PChouse.Utils.EMail;

/// <summary>
/// Mailer factory
/// </summary>
public static class MailerDI
{
    /// <summary>
    /// Add mailer factory to teh DI
    /// </summary>
    /// <param name="serviceCollection"></param>
    public static void AddMailerFactory(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IMailerFactory, Mailer.MailerFactory>(service =>
        {
            return new Mailer.MailerFactory(service);
        });
    }
}


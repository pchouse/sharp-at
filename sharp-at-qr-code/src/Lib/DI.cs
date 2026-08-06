/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using Microsoft.Extensions.DependencyInjection;


namespace PChouse.AT.QRCode;

public static class DI
{

    /// <summary>
    /// Add library to the DI
    /// </summary>
    /// <param name="services"></param>
    public static void AddAtQrCode(this IServiceCollection services)
    {

        services.AddSingleton<IBuilder, Builder>();

        services.AddSingleton<IImage, Image>();

        services.AddSingleton<IParser, Parser>();
    }
}
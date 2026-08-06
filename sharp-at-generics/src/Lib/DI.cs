/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using Microsoft.Extensions.DependencyInjection;

namespace PChouse.AT.Generics;

public static class DI
{
    public static void AddAtGenerics(this IServiceCollection services)
    {
        services.AddSingleton<IValidator, Validator>();

        services.AddSingleton<IHelper, Helper>();

        services.AddSingleton<IEnumParse, EnumParse>();

    }
}
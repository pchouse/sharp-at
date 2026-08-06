/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using Microsoft.Extensions.DependencyInjection;

namespace PChouse.Utils.EF;

public static class ScopedDatabaseContextFactoryDI
{
    public static void AddScopedDatabaseContextFactory(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IScopedDatabaseContextFactory, ScopedDatabaseContextFactory>();
    }
}
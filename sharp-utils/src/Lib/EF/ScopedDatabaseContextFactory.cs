/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace PChouse.Utils.EF;

public class ScopedDatabaseContextFactory(ILogger<ScopedDatabaseContextFactory> _logger, IServiceProvider service)
    : IScopedDatabaseContextFactory
{
    public virtual T GetScopedContext<T>() where T : ADbContext
    {
        _logger.LogTrace($"Going to create a new scoped database context type {typeof(T)}");

        var scope = service.CreateScope();
        var scopedService = scope.ServiceProvider;
        var dbContext = scopedService.GetRequiredService<T>();

        dbContext.OnDispose += (s, e) =>
        {
            try
            {
                scope.Dispose();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error creating a new scoped database context type {typeof(T)}");
            }
        };

        return dbContext;
    }
}
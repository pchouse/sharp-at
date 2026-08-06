/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

namespace PChouse.Utils.EF;

/// <summary>
/// The scoped database context factory
/// </summary>
public interface IScopedDatabaseContextFactory
{
    /// <summary>
    /// Get the database scoped context
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T GetScopedContext<T>() where T : ADbContext;
}
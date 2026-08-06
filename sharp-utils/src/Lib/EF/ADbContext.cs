/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using Microsoft.EntityFrameworkCore;

namespace PChouse.Utils.EF;

public abstract class ADbContext : DbContext
{
    /// <summary>
    /// Create instance of Db connection
    /// Ex:
    /// With connection string;
    /// var connectionString = "The connection string";
    /// new ADbContext( new DbContextOptionsBuilder().UseSqlServer(connectionString).Options )
    /// 
    /// For test:
    /// var connection = Effort.DbConnectionFactory.CreateTransient();
    /// new ADbContext( new DbContextOptionsBuilder().UseSqlServer(connection).Options )
    /// </summary>
    /// <param name="options"></param>
    public ADbContext(DbContextOptions options)
        : base(options)
    {
    }

    /// <summary>
    /// OnDispose event
    /// </summary>
    public event EventHandler? OnDispose;

    /// <summary>
    /// Flag to indicate if the context should be disposed on unit tests
    /// </summary>
    public bool DisposeOnUnitTest = false;

    /// <summary>
    /// Define if is running unit tests
    /// </summary>
    public bool IsUnitTest = false;

    /// <summary>
    /// Dispose the context and raise OnDispose event
    /// </summary>
    public override void Dispose()
    {
        if (!DisposeOnUnitTest && IsUnitTest) return;

        OnDispose?.Invoke(this, EventArgs.Empty);
        base.Dispose();
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Dispose the context asynchronously and raise OnDispose event
    /// </summary>
    /// <returns></returns>
    public override ValueTask DisposeAsync()
    {
        if (!DisposeOnUnitTest && System.Diagnostics.Debugger.IsAttached) return ValueTask.CompletedTask;
        OnDispose?.Invoke(this, EventArgs.Empty);
        GC.SuppressFinalize(this);
        return base.DisposeAsync();
    }
}
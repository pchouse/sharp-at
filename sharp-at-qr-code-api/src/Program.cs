/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using NLog;
using NLog.Web;
using PChouse.AT.Generics;
using PChouse.AT.QRCode;
using PChouse.AT.QRCode.API;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetLogger("at-qr-code-api");
logger.Debug("Start QR code API");

try
{
    var builder = WebApplication.CreateBuilder(args);

    AppDomain.CurrentDomain.UnhandledException += (s, e) =>
    {
        NLog.LogManager.GetLogger("Application").Error(e.ExceptionObject as Exception, "Unhandled Application Exception");
        NLog.LogManager.Flush();
    };

    System.Threading.Tasks.TaskScheduler.UnobservedTaskException += (s, e) =>
    {
        NLog.LogManager.GetLogger("Application").Warn(e.Exception, "Unobserved Task Exception");
    };

    builder.Logging.ClearProviders();
    builder.Logging.AddConsole();
    builder.Host.UseNLog();

    // Add services to the container.

    builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
            options.JsonSerializerOptions.DictionaryKeyPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
        });

    // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
    builder.Services.AddOpenApi();

    // Services daemon 
    builder.Host.UseWindowsService();
    builder.Host.UseSystemd();

    builder.Services.AddAtGenerics();
    builder.Services.AddAtQrCode();

    var app = builder.Build();

    app.UseMiddleware<AccountingMiddleware>();
    app.UseMiddleware<IPAuthorizationMiddleware>();
    app.UseMiddleware<HandleExceptionsMiddleware>();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();
    app.UseRequestLocalization();

    // LogHttpMiddleware must be the last middleware to cache the response status
    app.UseMiddleware<LogHttpMiddleware>();

    app.Run();

}
catch (Exception e)
{
    logger.Fatal(e, "Fatal error in QR code API");
}
finally
{
    logger.Debug("QR code API shutdown");
    NLog.LogManager.Flush();
    NLog.LogManager.Shutdown();
}

public partial class Program { }
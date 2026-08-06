/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

namespace PChouse.AT.QRCode.API;


/// <summary>
/// Log the requests
/// </summary>
public class LogHttpMiddleware
{
    /// <summary>
    /// The http logger
    /// </summary>
    private ILogger _httpLogger;

    /// <summary>
    /// The instance logger
    /// </summary>
    private ILogger _logger;

    /// <summary>
    /// Request delegate
    /// </summary>
    private readonly RequestDelegate _next;

    /// <summary>
    /// Authorization class
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="configuration"></param>
    public LogHttpMiddleware(RequestDelegate next, ILoggerFactory loggerFactory, ILogger<LogHttpMiddleware> logger)
    {
        _logger = logger;
        _httpLogger = loggerFactory.CreateLogger("httpLogger");
        _next = next;
    }

    /// <summary>
    /// Method to be invoked
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public virtual async Task Invoke(HttpContext context)
    {
        await _next.Invoke(context);

        try
        {
            var message = $"\"{context.Request.Method} {context.Request.Path} {context.Request.Protocol} {context.Response.StatusCode} {context.Connection.RemotePort}";
            
            _httpLogger?.LogDebug(message);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error invoking the log http middleware");
        }


    }
}

/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

namespace PChouse.AT.QRCode.API;


/// <summary>
/// Log the requests
/// </summary>
public class AccountingMiddleware
{
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
    public AccountingMiddleware(RequestDelegate next, ILogger<AccountingMiddleware> logger, IConfiguration configuration)
    {
        _logger = logger;
        _next = next;
    }

    /// <summary>
    /// Method to be invoked
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public virtual async Task Invoke(HttpContext context)
    {
        _logger?.LogTrace(
            $"Request from IP {context.Connection.RemoteIpAddress}, To route {context.GetEndpoint()?.DisplayName}"
            );

        await _next.Invoke(context);
    }
}

/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Net.Sockets;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace PChouse.AT.QRCode.API;

/// <summary>
/// Authorization class
/// </summary>
public class IPAuthorizationMiddleware
{
    /// <summary>
    /// The instance logger
    /// </summary>
    protected ILogger _logger;

    /// <summary>
    /// List of allowed end deny IPs
    /// </summary>
    protected Dictionary<AddressFamily, (List<string>?, List<string>?)> ipsList;

    /// <summary>
    /// Request delegate
    /// </summary>
    private readonly RequestDelegate _next;

    /// <summary>
    /// Authorization class
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="configuration"></param>
    public IPAuthorizationMiddleware(RequestDelegate next, ILogger<IPAuthorizationMiddleware> logger, IConfiguration configuration)
    {
        _logger = logger;
        _next = next;
        ipsList = new()
        {
            {
                AddressFamily.InterNetwork,
                (
                    configuration.GetSection($"IPs:Allow:IPv4").Get<List<string>>(),
                    configuration.GetSection($"IPs:Deny:IPv4").Get<List<string>>()
                )
            },
            {
                AddressFamily.InterNetworkV6,
                (
                    configuration.GetSection($"IPs:Allow:IPv6").Get<List<string>>(),
                    configuration.GetSection($"IPs:Deny:IPv6").Get<List<string>>()
                )
            }
        };
    }

    /// <summary>
    /// Check authorization for the IP
    /// </summary>
    /// <param name="ip">The IP to verify</param>
    /// <param name="error">The error is denied</param>
    /// <returns></returns>
    public virtual bool CheckIP(IPAddress? ip, [NotNullWhen(false)] out string? error)
    {

        _logger.LogTrace($"Start verifying IP address {ip}");

        error = null;

        if (ip is null) return true;

        if (ip.AddressFamily != AddressFamily.InterNetwork &&
            ip.AddressFamily != AddressFamily.InterNetworkV6)
        {
            _logger.LogInformation($"IP {ip} is not IPv4 or IPv6 ito will be allowed");
            return true;
        }

        var ips = ipsList[ip.AddressFamily];

        var allowIps = ips.Item1;
        var denyIps = ips.Item2;

        if (denyIps?.Contains(ip.ToString()) ?? false)
        {
            error = $"IP {ip.AddressFamily} [ {ip} ] is black listed, deny";
            _logger.LogDebug($"IP {ip} is black listed, deny");
            return false;
        }

        if ((allowIps?.Count() ?? 0) != 0)
        {
            if (allowIps!.Contains(ip.ToString()))
            {
                _logger.LogInformation($"Allow list contain {ip}, allow");
                return true;
            }

            _logger.LogInformation($"IP {ip} is not in the allow list, deny");
            error = $"IP {ip} is not in the allow list, deny";
            return false;
        }

        _logger.LogInformation("Allow list is empty, allow all");
        return true;
    }

    /// <summary>
    /// The method to be invoked
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public virtual async Task Invoke(HttpContext context)
    {
        var ip = context.Connection.RemoteIpAddress;
        if (ip is not null && CheckIP(ip, out var error) == false)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            context.Response.Headers.Append("x-qr-code-api-deny", error ?? "Unknown ip authorization error");
            return;
        }

        await _next.Invoke(context);
    }
}

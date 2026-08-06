/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using PChouse.AT.QRCode.API.Library;
using PChouse.AT.QRCode.Library;

namespace PChouse.AT.QRCode.API;

/// <summary>
/// Handler for the controller exception
/// </summary>
public class HandleExceptionsMiddleware
{

    /// <summary>
    /// Request delegate
    /// </summary>
    private readonly RequestDelegate _next;

    /// <summary>
    /// Handler for the controller exceptions
    /// </summary>
    /// <param name="next"></param>
    public HandleExceptionsMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    /// <summary>
    /// The method to be invoked
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public virtual async Task Invoke(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (BuilderFieldException e)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync<ErrorResponse>(new ErrorResponse
            {
                Error = e.Error,
                Field = string.Join(",", e.Field.Fields),
                Message = e.Message
            });
        }
        catch (BuilderException e)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync<ErrorResponse>(new ErrorResponse
            {
                Error = e.Error,
                Message = e.Message
            });
        }
        catch (Exception e)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync<ErrorResponse>(new ErrorResponse
            {
                Message = e.Message
            });
        }
    }
}
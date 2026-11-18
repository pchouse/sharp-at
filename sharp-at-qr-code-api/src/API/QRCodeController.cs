/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using Microsoft.AspNetCore.Mvc;
using PChouse.AT.QRCode.API.Library;
using PChouse.AT.QRCode.Library;

namespace PChouse.AT.QRCode.API;

/// <summary>
/// QRcode endpoints controller
/// </summary>
/// <param name="logger"></param>
/// <param name="builder"></param>
/// <param name="image"></param>
/// <param name="parser"></param>
[ApiController]
[Route("")]
public class QRCodeController(
        ILogger<QRCodeController> logger,
        IBuilder builder,
        IImage image,
        IParser parser
    ) : ControllerBase
{

    /// <summary>
    /// The logger
    /// </summary>
    private ILogger _logger = logger;

    /// <summary>
    /// Request QRCode string
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("build-string")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    public async Task<ActionResult<Response>> BuildString([FromBody] Request request)
    {
        var build = await builder.BuildStringAsync(request.Document, request.BuilderOptions);

        return new Response
        {
            QrCode = build.QrCodeString,
            Error = [.. build.Errors.Select(e => e.Error)]
        };
    }

    /// <summary>
    /// Generate QRCode image from a Document instance
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("generate-image")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    public async Task<ActionResult<Response>> GenerateImage([FromBody] RequestImage request)
    {
        var build = await builder.BuildStringAsync(request.Document, request.BuilderOptions);
        var qrCode = await image.GenerateImageAsync(build.QrCodeString, request.ImageOptions);

        return new Response
        {
            QrCode = qrCode is null ? "" : Convert.ToBase64String(qrCode),
            Error = [.. build.Errors.Select(e => e.Error)]
        };
    }

    /// <summary>
    /// Generate a QRCode image from a QRCode string
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("generate-image-string")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    public async Task<ActionResult<Response>> GenerateImageFromString([FromBody] RequestImageFromString request)
    {
        var qrCode = await image.GenerateImageAsync(request.QRCode, request.ImageOptions);

        return new Response
        {
            QrCode = qrCode is null ? "" : Convert.ToBase64String(qrCode),
            Error = []
        };
    }

    /// <summary>
    /// Ger the QRCode string from the QRCode image
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("image-to-string")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    public async Task<ActionResult<Response>> ParseImageToString([FromBody] RequestFromImage request)
    {

        if (string.IsNullOrWhiteSpace(request.Image))
        {
            _logger.LogDebug("Image is empty");
            throw new Exception("Image field is empty");
        }

        var imageBytes = Convert.FromBase64String(request.Image);

        if (imageBytes.Length == 0)
        {
            _logger.LogDebug("Image conversion from base64 is empty");
            throw new Exception("Image conversion from base64 is empty");
        }

        var qrCode = request.ImageParseType switch
        {
            ImageParseType.Jpeg or
            ImageParseType.Png => image.DecodeBitmapImage(imageBytes),
            ImageParseType.Svg => image.DecodeSvgImage(imageBytes),
            _ => image.DecodeImage(imageBytes)
        };

        return new Response { QrCode = qrCode };
    }

    /// <summary>
    /// Get a instance od Document from the QRCode image
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("image-to-document")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Document))]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    public async Task<ActionResult<Document>> ParseImageToDocument([FromBody] RequestFromImage request)
    {
        if (string.IsNullOrWhiteSpace(request.Image))
        {
            _logger.LogDebug("Image is empty");
            throw new Exception("Image field is empty");
        }

        var imageBytes = Convert.FromBase64String(request.Image);

        if (imageBytes.Length == 0)
        {
            _logger.LogDebug("Image conversion from base64 is empty");
            throw new Exception("Image conversion from base64 is empty");
        }

        var qrCode = request.ImageParseType switch
        {
            ImageParseType.Jpeg or
            ImageParseType.Png => image.DecodeBitmapImage(imageBytes),
            ImageParseType.Svg => image.DecodeSvgImage(imageBytes),
            _ => image.DecodeImage(imageBytes)
        };

        return parser.Parse(qrCode);
    }

    /// <summary>
    /// Get the QRCode string from a Document instance
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("string-to-document")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Document))]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    public async Task<ActionResult<Document>> ParseStringToDocument([FromBody] RequestFromString request)
    {
        return parser.Parse(request.QRCode);
    }

    /// <summary>
    /// Service health check
    /// </summary>
    /// <returns></returns>
    [HttpGet("HealthCheck")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    public ActionResult<string> HealthCheck()
    {
        return $"Ok => From {HttpContext.Connection.RemoteIpAddress}";
    }

}

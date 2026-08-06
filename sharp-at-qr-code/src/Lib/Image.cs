/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using PChouse.AT.QRCode.Library;
using SkiaSharp;
using Svg.Skia;
using ZXing;
using ZXing.QrCode;
using ZXing.QrCode.Internal;
using ZXing.SkiaSharp;


namespace PChouse.AT.QRCode;

/// <summary>
/// Generate and decode QR code images
/// </summary>
/// <param name="logger"></param>
public partial class Image(ILogger<Image> logger) : IImage
{

    /// <summary>
    /// Verify the qr code string
    /// </summary>
    [GeneratedRegex("^A:[0-9]{9}(\\*([A-Z][1-8]?:.+))+$")]
    protected partial Regex QRCodeStringRegexp { get; }

    /// <summary>
    /// The instance logger
    /// </summary>
    protected ILogger Logger = logger;

    /// <summary>
    /// Generate the QR code image
    /// </summary>
    /// <param name="qrCodeString"></param>
    /// <param name="imageOptions"></param>
    /// <exception cref="Exception"></exception>
    public virtual Task<byte[]> GenerateImageAsync(string qrCodeString, ImageOptions imageOptions)
    {
        return Task.Run(() =>
        {
            return GenerateImage(qrCodeString, imageOptions);
        });
    }

    /// <summary>
    /// Generate the QR code image
    /// </summary>
    /// <param name="qrCodeString"></param>
    /// <param name="imageOptions"></param>
    /// <exception cref="Exception"></exception>
    public virtual byte[] GenerateImage(string qrCodeString, ImageOptions imageOptions)
    {

        Logger.LogTrace($"Start generate image for string {qrCodeString}, image-type {imageOptions.ImageType}");

        qrCodeString = qrCodeString.Trim();

        if (!QRCodeStringRegexp.IsMatch(qrCodeString))
        {
            throw new ImageException("The QR code string is not valid");
        }

        var internalQRcode = ZXing.QrCode.Internal.Encoder.encode(
                            qrCodeString,
                            ErrorCorrectionLevel.M,
                            new Dictionary<EncodeHintType, object> { { EncodeHintType.CHARACTER_SET, "UTF-8" } }
        );

        var version = internalQRcode.Version.VersionNumber < 9
            ? 9
            : internalQRcode.Version.VersionNumber;

        var encodingOptions = new QrCodeEncodingOptions
        {
            QrVersion = version,
            ErrorCorrection = ErrorCorrectionLevel.M,
            Margin = 4,
            CharacterSet = string.IsNullOrWhiteSpace(imageOptions.CharacterSet)
                                        ? "UTF-8"
                                        : imageOptions.CharacterSet
        };

        if (imageOptions.Height.HasValue) encodingOptions.Height = imageOptions.Height.Value;
        if (imageOptions.Width.HasValue) encodingOptions.Width = imageOptions.Width.Value;

        var imageBytes = imageOptions.ImageType switch
        {
            // IF SVG generate svg
            ImageType.Svg => GenerateSvgImage(qrCodeString, encodingOptions),

            // IF png or jpg
            var t when t.TryParse(out SKEncodedImageFormat? type)
                => GenerateBitmapImage(qrCodeString, encodingOptions, type.Value, imageOptions.Quality),

            // If no handler throw
            _ => throw new Exception($"Image type {imageOptions.ImageType} not handled")
        };

        if (imageBytes.Length == 0)
        {
            throw new ImageException("Error generating image");
        }

        return imageBytes;
    }

    /// <summary>
    /// Generate the QR code as a bitmap image (the type in image options)
    /// </summary>
    /// <param name="qrCodeString"></param>
    /// <param name="imageOptions"></param>
    /// <param name="version"></param>
    /// <returns></returns>
    protected byte[] GenerateBitmapImage(string qrCodeString, QrCodeEncodingOptions options, SKEncodedImageFormat format, int quality)
    {
        var writer = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,
            Options = options
        };

        using SKBitmap bitmap = writer.Write(qrCodeString);
        using var image = SKImage.FromBitmap(bitmap);
        using var data = image.Encode(format, quality);

        return data.ToArray();
    }

    /// <summary>
    /// Generate the QR code as Svg image
    /// </summary>
    /// <param name="qrCodeString"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    protected byte[] GenerateSvgImage(string qrCodeString, QrCodeEncodingOptions options)
    {
        return Encoding.UTF8.GetBytes(
            GenerateSvgRawImage(qrCodeString, options)
        );
    }

    /// <summary>
    /// Generate the QR code as Svg image
    /// </summary>
    /// <param name="qrCodeString"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    protected string GenerateSvgRawImage(string qrCodeString, QrCodeEncodingOptions options)
    {
        var writerSvg = new BarcodeWriterSvg
        {
            Format = BarcodeFormat.QR_CODE,
            Options = options
        };

        return writerSvg.Write(qrCodeString).Content;
    }

    /// <summary>
    /// Decode image 
    /// </summary>
    /// <param name="image"></param>
    /// <returns></returns>
    /// <exception cref="ImageException"></exception>
    public virtual string DecodeImage(byte[] image)
    {
        return Convert.ToHexString(image.Take(300).ToArray()) switch
        {
            // Png, Jpg
            var t when t.StartsWith("89504E470D0A1A0A") || t.StartsWith("FFD8FF")
                       => DecodeBitmapImage(image),

            // SVG
            var t when t.Contains(Convert.ToHexString(Encoding.UTF8.GetBytes(@"<svg xmlns=")))
                        => DecodeSvgImage(image),

            _ => throw new ImageException($"No auto detection for this image")
        };
    }

    /// <summary>
    /// Decode a bitmap QR code image
    /// </summary>
    /// <param name="image"></param>
    /// <returns></returns>
    /// <exception cref="ImageException"></exception>
    public virtual string DecodeBitmapImage(byte[] image)
    {
        using var bitmap = SKBitmap.Decode(image);

        if (bitmap == null) throw new ImageException("Error decoding image");

        var reader = new BarcodeReader();

        var readerResult = reader.Decode(bitmap);

        return readerResult.Text;
    }

    /// <summary>
    /// Decode SVG image
    /// </summary>
    /// <param name="image"></param>
    /// <returns></returns>
    public virtual string DecodeSvgImage(byte[] image)
    {
        return DecodeSvgImage(Encoding.UTF8.GetString(image));

    }

    /// <summary>
    /// Decode SVG image
    /// </summary>
    /// <param name="image"></param>
    /// <returns></returns>
    /// <exception cref="ImageException"></exception>
    public virtual string DecodeSvgImage(string image)
    {
        using var svg = new SKSvg();

        svg.FromSvg(image);

        if (svg.Picture == null) throw new ImageException("Fail to decode SVG QR code image");

        int width = (int)Math.Ceiling(svg.Picture.CullRect.Width);
        int height = (int)Math.Ceiling(svg.Picture.CullRect.Height);

        using var bitmap = new SKBitmap(width, height);
        using var canvas = new SKCanvas(bitmap);

        canvas.Clear(SKColors.White);
        canvas.DrawPicture(svg.Picture);
        canvas.Flush();

        var reader = new BarcodeReader();
        var result = reader.Decode(bitmap);

        return result.Text;
    }

}
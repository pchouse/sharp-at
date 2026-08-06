/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Diagnostics.CodeAnalysis;
using PChouse.AT.QRCode.Library;
using SkiaSharp;

namespace PChouse.AT.QRCode;

public static class ImageTypeExtender
{

    /// <summary>
    /// Parse Image type to SKEncodedImageFormat 
    /// </summary>
    /// <param name="imageType"></param>
    /// <returns></returns>
    public static SKEncodedImageFormat Parse(this ImageType imageType)
    {
        return Enum.Parse<SKEncodedImageFormat>(imageType.ToString());
    }

    /// <summary>
    /// Try parse Image type to SKEncodedImageFormat 
    /// </summary>
    /// <param name="imageType"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    public static bool TryParse(this ImageType imageType, [NotNullWhen(true)] out SKEncodedImageFormat? result)
    {
        var parse = Enum.TryParse<SKEncodedImageFormat>(imageType.ToString(), out var parseResult);
        result = parseResult;
        return parse;
    }

}

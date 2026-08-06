/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using PChouse.AT.QRCode.Library;

namespace PChouse.AT.QRCode;

/// <summary>
/// Generate and decode QR code images
/// </summary>
public interface IImage
{

    /// <summary>
    /// Generate the QR code image
    /// </summary>
    /// <param name="qrCodeString"></param>
    /// <param name="imageOptions"></param>
    /// <exception cref="Exception"></exception>
    public byte[] GenerateImage(string qrCodeString, ImageOptions imageOptions);

    /// <summary>
    /// Generate the QR code image
    /// </summary>
    /// <param name="qrCodeString"></param>
    /// <param name="imageOptions"></param>
    /// <exception cref="Exception"></exception>

    public Task<byte[]> GenerateImageAsync(string qrCodeString, ImageOptions imageOptions);

    /// <summary>
    /// Decode image 
    /// </summary>
    /// <param name="image"></param>
    /// <returns></returns>
    /// <exception cref="ImageException"></exception>
    public string DecodeImage(byte[] image);

    /// <summary>
    /// Decode a bitmap QR code image
    /// </summary>
    /// <param name="image"></param>
    /// <returns></returns>
    /// <exception cref="ImageException"></exception>
    public string DecodeBitmapImage(byte[] image);

    /// <summary>
    /// Decode SVG image
    /// </summary>
    /// <param name="image"></param>
    /// <returns></returns>
    public string DecodeSvgImage(byte[] image);

    /// <summary>
    /// Decode SVG image
    /// </summary>
    /// <param name="image"></param>
    /// <returns></returns>
    /// <exception cref="ImageException"></exception>
    public string DecodeSvgImage(string image);

}
/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PChouse.Utils.PDFApiClient.Multicert;

/// <summary>
/// Visible signature information attributes to be applied to a document. Restricted support for PDF documents only
/// </summary>
public class VisibleSignatureInfo
{
    /// <summary>
    /// ONE_OFF: Visible signature template in a velocity format will be provided in the oneOffTemplate request attribute.
    /// SERVICE_CUSTOMIZATION: Uses the issuer service default visible signature template or if not found falls back on the system template.
    /// </summary>
    [JsonPropertyName("templateType")]
    public virtual TemplateType TemplateType { get; set; } = TemplateType.ONE_OFF;

    /// <summary>
    /// example: Digitally Signed by $certificate.getCommonName().
    /// If templateType = ONE_OFF, then a velocity template value format must be set here.
    /// </summary>
    [JsonPropertyName("oneOffTemplate")]
    public virtual string? OneOffTemplate { get; set; } = default;

    /// <summary>
    /// SIGN_FIELD: Use a signature field present in the document. The signature field name should be set in the locationValue request attribute.
    /// COORDINATE: Use a coordinate to find the correct document position.Supported format: page=<page_number>,x=<lower_left_x>,y=<lower_left_y>
    /// PLACEHOLDER: Use a placeholder(unique word). 
    /// System will scan the document for the given placeholder and stop in the first occurrence.
    /// The lower left part of the word will be used as a coordinate to start the drawing of the signature 
    /// canvas from top/left orientation.
    /// If the placeholder is not found, an error will be thrown and the signature will not be applied. 
    /// The placeholder value should be set in the locationValue request attribute.
    /// </summary>
    [Required]
    [JsonPropertyName("locationType")]
    public virtual required LocationType LocationType { get; set; }

    /// <summary>
    /// example: page=1,x=70,y=100
    /// Location value for the appropriate locationType defined.
    /// The unit of measurement for coordinates x and y are pixels (px).
    /// </summary>
    [Required]
    [JsonPropertyName("locationValue")]
    public virtual required string LocationValue { get; set; }

    /// <summary>
    /// example: width=155,height=50
    /// Area of the signature canvas.
    /// The unit of measurement for area is pixel(px).
    /// If not set will default to system settings.
    /// </summary>
    [JsonPropertyName("area")]
    public virtual string? Area { get; set; } = default;

    /// <summary>
    /// example: #FFFFFF
    /// Background color for visible signature area.NOTE: support web color in hex triplet format or the 'transparent' reserved word.
    /// </summary>
    [JsonPropertyName("areaBackgroundColor")]
    public virtual string AreaBackgroundColor { get; set; } = "transparent";

    /// <summary>
    /// example: color=#000000,size=11
    /// Font style of the signature text.The unit of measurement for font size is point (pt). If not set will default to system settings.NOTE: Only black color is currently supported.
    /// </summary>
    [JsonPropertyName("font")]
    public virtual string? Font { get; set; } = default;

    /// <summary>
    /// IMAGE_PNG: Image type set to visible signatures. Supported of png image in the format Base64.
    /// IMAGE_JPG: Image type set to visible signatures.Supported of jpg image in the format Base64.
    /// </summary>
    [JsonPropertyName("imageType")]
    public virtual ImageType? ImageType { get; set; } = default;

    /// <summary>
    /// Image value in base64 that will be applied to visible signatures.
    /// </summary>
    [JsonPropertyName("imageValue")]
    public virtual string? ImageValue { get; set; } = default;

    /// <summary>
    /// Default constructor for VisibleSignatureInfo.
    /// </summary>
    public VisibleSignatureInfo() { }

    /// <summary>
    /// Constructor for VisibleSignatureInfo with parameters.
    /// </summary>
    /// <param name="oneOffTemplate">example: Digitally Signed by $certificate.getCommonName().</param>
    /// <param name="locationType"></param>
    /// <param name="locationValue"></param>
    public VisibleSignatureInfo(string oneOffTemplate, LocationType locationType, string locationValue)
    {
        OneOffTemplate = oneOffTemplate;
        LocationType = locationType;
        LocationValue = locationValue;
    }


    /// <summary>
    /// Set the image to be used in the signature.
    /// </summary>
    /// <param name="path"></param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="FileNotFoundException"></exception>
    /// <exception cref="Exception"></exception>
    public virtual void SetImage(string path)
    {
        if (path == null) throw new ArgumentNullException(nameof(path), "Path cannot be null");

        if (System.IO.File.Exists(path) == false) throw new FileNotFoundException($"File not found: {path}");

        ImageValue = Convert.ToBase64String(System.IO.File.ReadAllBytes(path));

        var extension = Path.GetExtension(path).ToLower();

        ImageType = extension switch
        {
            var e when e.Equals(".jpg", StringComparison.InvariantCultureIgnoreCase) ||
                        e.Equals(".jpeg", StringComparison.InvariantCultureIgnoreCase)
                        => Multicert.ImageType.JPG,

            ".png" => Multicert.ImageType.PNG,

            _ => throw new Exception(
                    $"Unsupported image type. Supported types are .jpg and .png. Type '{extension}'"
                )
        };
    }

    /// <summary>
    /// Set the area of the signature.
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public virtual void SetArea(int width, int height)
    {
        if (width <= 0) throw new ArgumentOutOfRangeException(nameof(width), "Width must be greater than 0");
        if (height <= 0) throw new ArgumentOutOfRangeException(nameof(height), "Height must be greater than 0");
        Area = $"width={width},height={height}";
    }

    /// <summary>
    /// Set the location of the signature.
    /// </summary>
    /// <param name="page"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public virtual void SetLocationCoordinate(int page, int x, int y)
    {
        if (page <= 0) throw new ArgumentOutOfRangeException(nameof(page), "Page must be greater than 0");
        if (x < 0) throw new ArgumentOutOfRangeException(nameof(x), "X must be greater than or equal to 0");
        if (y < 0) throw new ArgumentOutOfRangeException(nameof(y), "Y must be greater than or equal to 0");
        LocationValue = $"page={page},x={x},y={y}";
        LocationType = LocationType.COORDINATE;
    }

    /// <summary>
    /// Set the location of the signature to a placeholder in the document.
    /// </summary>
    /// <param name="placeholder"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public virtual void SetLocationPlaceholder(string placeholder)
    {
        if (string.IsNullOrEmpty(placeholder)) throw new ArgumentNullException(nameof(placeholder), "Placeholder cannot be null or empty");
        LocationValue = placeholder;
        LocationType = LocationType.PLACEHOLDER;
    }

    /// <summary>
    /// Set the location of the signature to a field in the document.
    /// </summary>
    /// <param name="fieldName"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public virtual void SetLocationField(string fieldName)
    {
        if (string.IsNullOrEmpty(fieldName)) throw new ArgumentNullException(nameof(fieldName), "Field name cannot be null or empty");
        LocationValue = fieldName;
        LocationType = LocationType.SIGN_FIELD;
    }

    /// <summary>
    /// Set the background color of the signature area.
    /// </summary>
    /// <param name="color"></param>
    /// <param name="size"></param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public virtual void SetFont(string color, int size)
    {
        if (string.IsNullOrEmpty(color)) throw new ArgumentNullException(nameof(color), "Color cannot be null or empty");
        if (size <= 0) throw new ArgumentOutOfRangeException(nameof(size), "Size must be greater than 0");
        if (color.Length != 7 || color[0] != '#') throw new ArgumentException("Color must be in the format #RRGGBB", nameof(color));
        Font = $"color={color},size={size}";

    }
}


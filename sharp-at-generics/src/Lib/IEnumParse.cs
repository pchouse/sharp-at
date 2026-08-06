using System.Diagnostics.CodeAnalysis;

public interface IEnumParse
{

    /// <summary>
    /// Try to parse the Enum first from XmlEnumAttribute 
    /// than Json JsonStringEnumMemberNameAttribute 
    /// and than from enum field name
    /// </summary>
    /// <param name="enum"></param>
    /// <param name="type"></param>
    /// <param name="value"></param>
    /// <param name="ignoreCase"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public bool TryFirstFromAttributes(
        Type type,
        string value,
        bool ignoreCase,
        [NotNullWhen(true)] out object? result
        );

    /// <summary>
    /// Try to parse the Enum first from XmlEnumAttribute 
    /// than Json JsonStringEnumMemberNameAttribute 
    /// and than from enum field name
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <param name="ignoreCase"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    public bool TryFirstFromAttributes<T>(string value, bool ignoreCase, [NotNullWhen(true)] out T? result) where T : struct;


    /// <summary>
    /// Try to parse the Enum first from XmlEnumAttribute 
    /// than Json JsonStringEnumMemberNameAttribute 
    /// and than from enum field name
    /// </summary>
    /// <param name="enum"></param>
    /// <param name="type"></param>
    /// <param name="value"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public bool TryFirstFromAttributes(Type type, string value, [NotNullWhen(true)] out object? result);

    /// <summary>
    /// Try to parse the Enum first from XmlEnumAttribute 
    /// than Json JsonStringEnumMemberNameAttribute 
    /// and than from enum field name
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    public bool TryFirstFromAttributes<T>(string value, [NotNullWhen(true)] out T? result) where T : struct;

}
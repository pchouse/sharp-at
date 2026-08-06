/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Xml.Serialization;

namespace PChouse.AT.Generics;

public class EnumParse : IEnumParse
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
        )
    {

        if (!type.IsEnum) throw new ArgumentException($"Type {type.Name} is not a Enum type");

        result = null;

        foreach (var enumValue in type.GetEnumValues())
        {
            if (enumValue is null) continue;

            var fieldInfo = type.GetField(enumValue.ToString()!);

            if (fieldInfo is null) continue;

            var xmlAttribute = fieldInfo.GetCustomAttribute<XmlEnumAttribute>();

            if (xmlAttribute?.Name is not null)
            {
                if (xmlAttribute.Name.Equals(value) || (ignoreCase && xmlAttribute.Name.ToUpper().Equals(value.ToUpper())))
                {
                    result = enumValue;
                    return true;
                }
            }

            var jsonAttribute = fieldInfo.GetCustomAttribute<XmlEnumAttribute>();

            if (jsonAttribute?.Name is not null)
            {
                if (jsonAttribute.Name.Equals(value) || (ignoreCase && jsonAttribute.Name.ToUpper().Equals(value.ToUpper())))
                {
                    result = enumValue;
                    return true;
                }
            }

        }

        if (Enum.TryParse(type, value, ignoreCase, out var parseResult))
        {
            result = parseResult;
            return true;
        }

        return false;
    }

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
    public bool TryFirstFromAttributes<T>(string value, bool ignoreCase, [NotNullWhen(true)] out T? result) where T : struct
    {
        if (TryFirstFromAttributes(typeof(T), value, ignoreCase, out object? parseResult))
        {
            result = (T)parseResult;
            return true;
        }

        result = null;
        return false;
    }

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
    public bool TryFirstFromAttributes(Type type, string value, [NotNullWhen(true)] out object? result)
    {

        if (TryFirstFromAttributes(type, value, true, out object? parseResult))
        {
            result = parseResult;
            return true;
        }

        result = null;
        return false;
    }

    /// <summary>
    /// Try to parse the Enum first from XmlEnumAttribute 
    /// than Json JsonStringEnumMemberNameAttribute 
    /// and than from enum field name
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    public bool TryFirstFromAttributes<T>(string value, [NotNullWhen(true)] out T? result) where T : struct
    {
        if (TryFirstFromAttributes(typeof(T), value, true, out object? parseResult))
        {
            result = (T)parseResult;
            return true;
        }

        result = null;
        return false;
    }
}
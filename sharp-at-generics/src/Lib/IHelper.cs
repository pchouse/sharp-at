/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/


namespace PChouse.AT.Generics;

/// <summary>
/// A class with helper methods
/// </summary>
public interface IHelper
{
    /// <summary>
    /// Calculate the document period
    /// </summary>
    /// <param name="startMonth">The fiscal start date</param>
    /// <param name="documentDate">The document date</param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public int DocumentPeriod(int startMonth, DateOnly documentDate);

}
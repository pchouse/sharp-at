/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using Microsoft.Extensions.Logging;

namespace PChouse.AT.Generics;

/// <summary>
/// A class with helper methods
/// </summary>
public class Helper : IHelper
{
    /// <summary>
    /// Instance logger
    /// </summary>
    protected readonly ILogger Logger;

    public Helper(ILogger<Helper> logger)
    {
        Logger = logger;
        Logger.LogTrace($"{nameof(Helper)} instance created");
    }

    /// <summary>
    /// Calculate the document period
    /// </summary>
    /// <param name="startMonth">The fiscal start date</param>
    /// <param name="documentDate">The document date</param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public virtual int DocumentPeriod(int startMonth, DateOnly documentDate)
    {
        if (startMonth < 1 || startMonth > 12)
        {
            Logger.LogError($"{nameof(DocumentPeriod)}: Start month out of range 1 to 12, actual value {startMonth}");

            throw new ArgumentOutOfRangeException(
                nameof(startMonth), startMonth, "Fiscal year start month should be between 1 and 12"
            );
        }

        var period = 0;

        if (startMonth == 1)
        {
            period = documentDate.Month;
        }
        else if (documentDate.Month >= startMonth)
        {
            period = documentDate.Month - startMonth + 1;
        }
        else
        {
            period = 13 - startMonth + documentDate.Month;
        }

        Logger.LogTrace(
            $"{nameof(DocumentPeriod)}: For fiscal year start month {startMonth} and document date {documentDate.ToShortDateString()}"
        );

        return period;
    }

}
/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Security.Claims;

namespace PChouse.Utils.JWT;

public enum JwtType
{
    /// <summary>
    /// To access the application
    /// </summary>
    ACCESS,

    /// <summary>
    /// To renew the token
    /// </summary>
    REFRESH,

    /// <summary>
    /// Token short life time for when you need to pass the token in the url
    /// </summary>
    SHORT_TIME
}
/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

namespace PChouse.Utils.JWT;

public class BearerOptions
{
    /// <summary>
    /// The key to encrypt the token
    /// </summary>
    public string TokenKey { get; set; } = string.Empty;

    /// <summary>
    /// Roles authorization
    /// </summary>
    public string[] Roles { get; set; } = [];

}
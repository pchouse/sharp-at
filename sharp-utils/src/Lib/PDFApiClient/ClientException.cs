/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/


namespace PChouse.Utils.PDFApiClient;

/// <summary>
/// PDF API Exception
/// </summary>
/// <param name="message"></param>
public class ClientException(string message) : Exception(message);
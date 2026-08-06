

using PChouse.AT.QRCode.Library;


/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/
namespace PChouse.AT.QRCode;

public interface IParser
{
    Document Parse(string qrCodeString);
}
/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/


using System.Data.Common;
using System.Reflection;
using System.Text.Json.Serialization;

namespace PChouse.AT.QRCode.Library.Test;

[TestClass]
public sealed class JsonAttributeTest
{

    [TestMethod]
    [DataRow(typeof(ImageType))]
    [DataRow(typeof(TinValidation))]
    [DataRow(typeof(BankTransfer))]
    [DataRow(typeof(BuilderOptions))]
    [DataRow(typeof(Document))]
    [DataRow(typeof(DocumentStatus))]
    [DataRow(typeof(DocumentType))]
    [DataRow(typeof(Error))]
    [DataRow(typeof(ImageOptions))]
    [DataRow(typeof(MultiBankPayment))]
    [DataRow(typeof(OtherInfo))]
    [DataRow(typeof(PaymentInfo))]
    public void TestJsonAttribute(Type type)
    {
        List<MemberInfo> fields = new();

        if (type.IsEnum)
        {
            fields.AddRange(type.GetFields().Where(f => !f.Name.StartsWith("value")).ToList());
        }
        else
        {
            fields.AddRange(type.GetProperties().ToList());
        }

        Assert.IsNotEmpty(fields, $"{type.Name} does not have fields or properties");

        foreach (var fieldInfo in fields)
        {
            var attributes = fieldInfo.GetCustomAttributes(
                 type.IsEnum
                     ? typeof(JsonStringEnumMemberNameAttribute)
                     : typeof(JsonPropertyNameAttribute)
                );

            Assert.AreEqual(1, attributes?.Count() ?? 0, $"Member: '{type.Name}', field: '{fieldInfo.Name}'");

            var attribute = attributes!.First();

            string attributeName = string.Empty;

            if (attribute is JsonStringEnumMemberNameAttribute jsonEnum)
            {
                attributeName = jsonEnum.Name;
            }
            else if (attribute is JsonPropertyNameAttribute jsonProperty)
            {
                attributeName = jsonProperty.Name;
            }

            Assert.AreEqual(fieldInfo.Name, attributeName, $"Member: '{type.Name}', field: '{fieldInfo.Name}'");
        }
    }

}
using System.Runtime.Serialization;

namespace PChouse.InvoiceCalc.Tax;

public enum VatType
{
    [EnumMember(Value = "ISE")]
    ISE,

    [EnumMember(Value = "RED")]
    RED,

    [EnumMember(Value = "INT")]
    INT,

    [EnumMember(Value = "NOR")]
    NOR,

    [EnumMember(Value = "OUT")]
    OUT,
}
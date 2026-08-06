using System.Runtime.Serialization;

namespace PChouse.InvoiceCalc;

public enum DiscountType
{
    [EnumMember(Value = "VALUE")]
    VALUE,

    [EnumMember(Value = "PERCENT")]
    PERCENT
}
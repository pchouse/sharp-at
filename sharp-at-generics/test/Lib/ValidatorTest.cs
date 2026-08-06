
using Microsoft.Extensions.Logging.Abstractions;

namespace PChouse.AT.Generics.Test;


[TestClass]
public sealed class ValidatorTest
{

    [TestMethod]
    [DataRow("FT FT/999", true, null)]
    [DataRow("FT 9/9", true, null)]
    [DataRow("FT99 FT/999", false, DocumentNumberError.REG_EXP_NOT_MATCH)]
    [DataRow("FT/999", false, DocumentNumberError.REG_EXP_NOT_MATCH)]
    [DataRow(" FT/999", false, DocumentNumberError.REG_EXP_NOT_MATCH)]
    [DataRow("F.T FT/999", false, DocumentNumberError.REG_EXP_NOT_MATCH)]
    [DataRow("F_T FT/999", false, DocumentNumberError.REG_EXP_NOT_MATCH)]
    [DataRow("F-T. FT/999", false, DocumentNumberError.REG_EXP_NOT_MATCH)]
    [DataRow("FT FT.2099-A_B/999", true, null)]
    [DataRow("FT .FT.2099-A_B/999", false, DocumentNumberError.SERIAL_STARTS_OR_ENDS_WRONG_CHARACTER)]
    [DataRow("FT _FT.2099-A_B/999", false, DocumentNumberError.SERIAL_STARTS_OR_ENDS_WRONG_CHARACTER)]
    [DataRow("FT -FT.2099-A_B/999", false, DocumentNumberError.SERIAL_STARTS_OR_ENDS_WRONG_CHARACTER)]
    [DataRow("FT FT.2099-A_B./999", false, DocumentNumberError.SERIAL_STARTS_OR_ENDS_WRONG_CHARACTER)]
    [DataRow("FT FT.2099-A_B-/999", false, DocumentNumberError.SERIAL_STARTS_OR_ENDS_WRONG_CHARACTER)]
    [DataRow("FT FT.2099-A_B_/999", false, DocumentNumberError.SERIAL_STARTS_OR_ENDS_WRONG_CHARACTER)]
    [DataRow(
        "FT 99/9999999999999999999999999999999999999999999999999999999",
        false,
        DocumentNumberError.WRONG_LENGTH
        )
    ]
    public void TestDocumentNumber(string documentNumber, bool isValid, DocumentNumberError? error)
    {
        var validator = new Validator(NullLogger<Validator>.Instance);

        Assert.AreEqual(
            isValid,
            validator.DocumentNumber(documentNumber, out var errorValidate),
            $"Testing {documentNumber} should return {isValid}"
            );

        Assert.AreEqual(error, errorValidate);
    }

    [TestMethod]
    [DataRow("999999990", true, true, null)]
    [DataRow("999999990", false, true, null)]
    [DataRow("140388311", true, true, null)]
    [DataRow("140388311", false, true, null)]
    [DataRow("140388310", true, false, PortugueseTinError.CONTROL_DIGIT_NOT_MATCH)]
    [DataRow("140388310", false, false, PortugueseTinError.CONTROL_DIGIT_NOT_MATCH)]
    [DataRow("217932878", false, true, null)]
    [DataRow("217932878", true, true, null)]
    [DataRow("217932879", false, false, PortugueseTinError.CONTROL_DIGIT_NOT_MATCH)]
    [DataRow("217932870", false, false, PortugueseTinError.CONTROL_DIGIT_NOT_MATCH)]
    [DataRow("328361879", false, true, null)]
    [DataRow("328361879", true, true, null)]
    [DataRow("328361871", false, false, PortugueseTinError.CONTROL_DIGIT_NOT_MATCH)]
    [DataRow("328361870", false, false, PortugueseTinError.CONTROL_DIGIT_NOT_MATCH)]
    [DataRow("536841098", false, true, null)]
    [DataRow("536841098", true, true, null)]
    [DataRow("536841091", false, false, PortugueseTinError.CONTROL_DIGIT_NOT_MATCH)]
    [DataRow("536841090", false, false, PortugueseTinError.CONTROL_DIGIT_NOT_MATCH)]
    [DataRow("667481117", false, true, null)]
    [DataRow("667481117", true, true, null)]
    [DataRow("667481111", false, false, PortugueseTinError.CONTROL_DIGIT_NOT_MATCH)]
    [DataRow("667481110", false, false, PortugueseTinError.CONTROL_DIGIT_NOT_MATCH)]
    [DataRow("820966037", false, true, null)]
    [DataRow("820966037", true, true, null)]
    [DataRow("820966031", false, false, PortugueseTinError.CONTROL_DIGIT_NOT_MATCH)]
    [DataRow("820966030", false, false, PortugueseTinError.CONTROL_DIGIT_NOT_MATCH)]
    [DataRow("906679249", false, true, null)]
    [DataRow("906679249", true, true, null)]
    [DataRow("906679241", false, false, PortugueseTinError.CONTROL_DIGIT_NOT_MATCH)]
    [DataRow("906679240", false, false, PortugueseTinError.CONTROL_DIGIT_NOT_MATCH)]
    [DataRow("90A979240", true, false, PortugueseTinError.IS_NOT_ONLY_NUMBERS)]
    [DataRow("90A979240", false, false, PortugueseTinError.IS_NOT_ONLY_NUMBERS)]
    [DataRow("90667929", true, false, PortugueseTinError.WRONG_LENGTH)]
    [DataRow("90667929", false, false, PortugueseTinError.WRONG_LENGTH)]
    [DataRow("099979249", true, false, PortugueseTinError.TIN_STARTS_WITH_ZERO)]
    [DataRow("099979249", false, false, PortugueseTinError.TIN_STARTS_WITH_ZERO)]
    [DataRow("439979249", true, false, PortugueseTinError.WRONG_START_DIGITS)]
    [DataRow("439979249", false, false, PortugueseTinError.CONTROL_DIGIT_NOT_MATCH)]
    public void TestPortugueseTin(string tin, bool strict, bool isValid, PortugueseTinError? error)
    {
        var validator = new Validator(NullLogger<Validator>.Instance);

        Assert.AreEqual(
            isValid,
            validator.PortugueseTin(tin, strict, out var errorValidated),
            $"Testing Portuguese {tin} strict {strict} should return {isValid}, error: {errorValidated.ToString()}"
        );

        Assert.AreEqual(error, errorValidated);
    }

    [TestMethod]
    [DataRow("0000-000", true)]
    [DataRow("9999-999", true)]
    [DataRow("0000-999", true)]
    [DataRow("9999-000", true)]
    [DataRow("1100-900", true)]
    [DataRow("0000-0000", false)]
    [DataRow("000-0000", false)]
    [DataRow("000-000", false)]
    [DataRow("1234-4321", false)]
    [DataRow("123-4321", false)]
    [DataRow("123-321", false)]
    public void TestPortuguesePostalCode(string postalCode, bool isValid)
    {
        var validator = new Validator(NullLogger<Validator>.Instance);

        Assert.AreEqual(isValid, validator.PortuguesePostalCode(postalCode));
    }

}
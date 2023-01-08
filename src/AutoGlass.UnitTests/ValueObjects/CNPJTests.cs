using AutoGlass.Domain.ValueObjects;
using AutoGlass.UnitTests.FakeData;
using Xunit;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AutoGlass.UnitTests.ValueObjects;

public sealed class CNPJTests
{
    [Theory]
    [InlineData("")]
    [InlineData("              ")]
    [InlineData("00000000000000")]
    public void ShouldBe_Invalid_When_Number_IsInvalid(string number)
    {
        var cnpj = new CNPJ(number);

        Assert.False(cnpj.IsValid);
    }

    [Theory]
    [InlineData(CNPJData.FORMATTED_CNPJ)]
    [InlineData(CNPJData.UNFORMATTED_CNPJ)]
    public void ShouldBe_Valid_When_Number_IsCNPJ(string number)
    {
        var cnpj = new CNPJ(number);

        Assert.True(cnpj.IsValid);
    }
}
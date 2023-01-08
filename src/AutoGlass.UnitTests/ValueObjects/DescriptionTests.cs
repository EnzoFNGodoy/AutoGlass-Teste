using AutoGlass.Domain.ValueObjects;
using AutoGlass.UnitTests.FakeData;

namespace AutoGlass.UnitTests.ValueObjects;

public sealed class DescriptionTests
{
    private const string MORE_THAN_100_CHARACTERS = "fhbunjieyswefhnwwefhujniklofoehujniFHUNIOOfnidbhhbnsdfosdfbnidbsUIOFJDSBNOJIFDOBJIKdfbikodfbkodSBdasdaasd";

    [Theory]
    [InlineData("")]
    [InlineData("     ")]
    [InlineData(MORE_THAN_100_CHARACTERS)]
    public void ShouldBe_Invalid_When_Text_IsInvalid(string text)
    {
        var description = new Description(text);

        Assert.False(description.IsValid);
    }

    [Theory]
    [InlineData(DescriptionData.VALID_DESCRIPTION)]
    [InlineData(DescriptionData.MINIMAL_DESCRIPTION)]
    [InlineData(DescriptionData.MAXIMAL_DESCRIPTION)]
    public void ShouldBe_Valid_When_Text_IsValid(string text)
    {
        var description = new Description(text);

        Assert.True(description.IsValid);
    }
}
using Domain.Users;
using Domain.Users.ValueObjects;
using FluentAssertions;

namespace Domain.UnitTests.Users;

public sealed class NameTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Name_Should_ThrowArgumentException_WhenValueIsInvalid(string? value)
    {
        // Act
        Name Action() => new(value);

        // Assert
        FluentActions.Invoking(Action).Should().Throw<ArgumentNullException>()
            .Which
            .ParamName.Should().Be("value");
    }
}

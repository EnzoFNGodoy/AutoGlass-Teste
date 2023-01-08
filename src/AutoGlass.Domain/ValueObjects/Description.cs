using AutoGlass.Domain.Core.ValueObjects;
using Flunt.Validations;

namespace AutoGlass.Domain.ValueObjects;

public sealed class Description : ValueObject
{
    private Description() // Empty constructor for EF
    { }

    public Description(string text)
    {
        Text = text;

        AddNotifications(new Contract<Description>()
            .Requires()
            .IsNotNullOrWhiteSpace(Text, "Description.Text", "The description cannot be empty.")
            .IsGreaterOrEqualsThan(3, Text.Length, "Description.Text", "The description must be greather or equals than 3 characters.")
            .IsLowerOrEqualsThan(100, Text.Length, "Description.Text", "The description must be lower or equals than 100 characters.")
            );
    }

    public string Text { get; private set; }

    public override string ToString() => Text;
}
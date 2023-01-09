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
            .IsNotNullOrWhiteSpace(Text, "Description.Text", "A descrição não pode ser vazia.")
            .IsLowerOrEqualsThan(3, Text.Length, "Description.Text", "A descrição precisa conter pelo menos 3 caracteres.")
            .IsGreaterOrEqualsThan(100, Text.Length, "Description.Text", "A descrição precisa conter no máximo 100 caracteres.")
            );
    }

    public string Text { get; private set; } = null!;

    public override string ToString() => Text;
}
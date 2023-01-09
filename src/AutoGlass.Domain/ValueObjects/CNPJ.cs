using AutoGlass.Domain.Core.ValueObjects;
using Flunt.Extensions.Br.Validations;
using Flunt.Validations;

namespace AutoGlass.Domain.ValueObjects;

public sealed class Cnpj : ValueObject
{
    private Cnpj() // Empty constructor for EF
    { }

    public Cnpj(string number)
    {
        Format(ref number);
        Number = number;

        AddNotifications(new Contract<Cnpj>()
            .Requires()
            .IsCnpj(Number, "CNPJ.Number", "CNPJ Inválido.")
            );
    }

    public string Number { get; private set; } = null!;

    private static void Format(ref string number)
    {
        number = number.Replace(".", string.Empty)
            .Replace("-", string.Empty)
            .Replace("/", string.Empty);
    }

    public override string ToString() => Number;
}
using AutoGlass.Domain.Core.ValueObjects;
using Flunt.Extensions.Br.Validations;
using Flunt.Validations;

namespace AutoGlass.Domain.ValueObjects;

public sealed class CNPJ : ValueObject
{
    private CNPJ() // Empty constructor for EF
    { }

    public CNPJ(string number)
    {
        Format(ref number);
        Number = number;

        AddNotifications(new Contract<CNPJ>()
            .Requires()
            .IsCnpj(Number, "CNPJ.Number", "The CNPJ is invalid.")
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
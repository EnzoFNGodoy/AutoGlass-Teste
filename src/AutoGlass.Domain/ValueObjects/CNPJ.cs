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
        Number = number;

        AddNotifications(new Contract<CNPJ>()
            .Requires()
            .IsCnpj(Number, "CNPJ.Number", "The CNPJ is invalid.")
            );
    }

    public string Number { get; private set; }

    public override string ToString() => Number;
}
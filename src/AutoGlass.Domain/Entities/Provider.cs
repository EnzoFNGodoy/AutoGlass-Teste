using AutoGlass.Domain.Core.Entities;
using AutoGlass.Domain.ValueObjects;

namespace AutoGlass.Domain.Entities;

public sealed class Provider : Entity
{
    private Provider() // Empty constructor for EF
    { }

    public Provider(Description description, CNPJ cnpj)
    {
        Description = description;
        CNPJ = cnpj;
    }

    public Description Description { get; set; }
    public CNPJ CNPJ { get; set; }
}
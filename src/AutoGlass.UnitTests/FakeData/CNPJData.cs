using AutoGlass.Domain.ValueObjects;

namespace AutoGlass.UnitTests.FakeData;

public sealed class CNPJData
{
    internal const string FORMATTED_CNPJ = "97.353.941/0001-40";
    internal const string UNFORMATTED_CNPJ = "97353941000140";

    internal CNPJ ValidCNPJ = new(UNFORMATTED_CNPJ);
    internal CNPJ InvalidCNPJ = new("");
}
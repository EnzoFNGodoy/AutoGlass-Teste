using AutoGlass.Domain.ValueObjects;

namespace AutoGlass.UnitTests.FakeData;

public sealed class DescriptionData
{
    internal const string MINIMAL_DESCRIPTION = "abc";
    internal const string MAXIMAL_DESCRIPTION = "fgu9obhwenasfiouasdhnfgusdg8sdhgisdjgnsdkojngosdknfkpNDOasbfjmsdkpfasjbdakodmoaskdnsjoandmkoasmddda";
    internal const string VALID_DESCRIPTION = "Descrição Válida";

    internal Description ValidDescription = new(VALID_DESCRIPTION);
    internal Description InvalidDescription = new("");
}
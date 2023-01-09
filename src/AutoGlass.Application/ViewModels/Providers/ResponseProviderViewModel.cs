namespace AutoGlass.Application.ViewModels.Providers;

public sealed record ResponseProviderViewModel
{
    public Guid Id { get; set; }
    public string Description { get; set; } = null!;
    public string CNPJ { get; set; } = null!;
}
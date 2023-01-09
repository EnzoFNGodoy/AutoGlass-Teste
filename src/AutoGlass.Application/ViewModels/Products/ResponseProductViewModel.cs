using AutoGlass.Application.ViewModels.Providers;

namespace AutoGlass.Application.ViewModels.Products;

public sealed record ResponseProductViewModel
{
    public Guid Id { get; set; }
    public string Description { get; set; } = null!;
    public DateTime ProductionDate { get; set; }
    public DateTime ExpirationDate { get; set; }
    public bool IsActive { get; set; }
    public ResponseProviderViewModel? Provider { get; set; }
}
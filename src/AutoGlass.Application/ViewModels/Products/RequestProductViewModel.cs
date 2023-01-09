using AutoGlass.Application.ViewModels.Providers;
using System.ComponentModel.DataAnnotations;

namespace AutoGlass.Application.ViewModels.Products;

public sealed record RequestProductViewModel
{
    public string Description { get; set; } = null!;

    public DateTime ProductionDate { get; set; }

    public DateTime ExpirationDate { get; set; }

    public bool IsActive { get; set; } = true;

    public RequestProviderViewModel Provider { get; set; } = null!;
}
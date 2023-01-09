using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace AutoGlass.Application.ViewModels.Providers;

public sealed record RequestProviderViewModel
{
    public string Description { get; set; } = null!;

    public string CNPJ { get; set; } = null!;
}
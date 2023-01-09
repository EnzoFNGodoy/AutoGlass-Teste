using AutoGlass.Application.ViewModels.Products;
using AutoGlass.Application.ViewModels.Providers;
using AutoGlass.Domain.Entities;
using AutoMapper;

namespace AutoGlass.Application.AutoMapper;

public sealed class DomainToViewModelProfile : Profile
{
	public DomainToViewModelProfile()
	{
		CreateMap<Product, ResponseProductViewModel>();
		CreateMap<Provider, ResponseProviderViewModel>();
	}
}
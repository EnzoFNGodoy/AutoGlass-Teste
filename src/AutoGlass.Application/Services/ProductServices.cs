using AutoGlass.Application.Interfaces;
using AutoGlass.Application.Services.Core;
using AutoGlass.Application.ViewModels.Products;
using AutoGlass.Domain.Entities;
using AutoGlass.Domain.Interfaces;
using AutoGlass.Domain.ValueObjects;
using AutoGlass.Infra.Data.Transactions;
using AutoMapper;
using Flunt.Notifications;

namespace AutoGlass.Application.Services;

public sealed class ProductServices : IProductServices
{
    private readonly IProductRepository _productRepository;
    private readonly IProviderRepository _providerRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public ProductServices(
        IProductRepository productRepository,
        IProviderRepository providerRepository,
        IMapper mapper,
        IUnitOfWork unitOfWork
        )
    {
        _productRepository = productRepository;
        _providerRepository = providerRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<ServiceResponse> Create(RequestProductViewModel request)
    {
        var productDescription = new Description(request.Description);
        var product = new Product(
            productId: Guid.NewGuid(),
            description: productDescription,
            productionDate: request.ProductionDate,
            expirationDate: request.ExpirationDate,
            isActive: request.IsActive
            );

        var provider = await _providerRepository.GetOneWhere(p => p.CNPJ.Number == request.Provider.CNPJ);

        var providerDescription = new Description(request.Provider.Description);
        var providerCNPJ = new Cnpj(request.Provider.CNPJ);

        if (provider is null)
        {
            provider = new Provider(
                providerId: Guid.NewGuid(),
                description: providerDescription,
                cnpj: providerCNPJ
                );

            product.SetProvider(provider);

            if (provider.IsValid)
                await _providerRepository.Insert(provider);
        }
        else if (provider.IsValid)
        {
            product.SetProvider(provider);
            provider.UpdateDescription(providerDescription);
            await _providerRepository.Update(provider);
        }

        if (!product.IsValid || product.Provider is null)
            return new ServiceResponse(false, product.Notifications.ToList());

        await _productRepository.Insert(product);

        if (await _unitOfWork.SaveChangesAsync() <= 0)
            return new ServiceResponse(false, "Erro ao criar o produto.");

        return new ServiceResponse(true, "Produto criado com sucesso.");
    }

    public async Task<ServiceResponse> Edit(Guid id, RequestProductViewModel request)
    {
        var product = await _productRepository.GetOneWhere(p => p.Id == id && p.IsActive);

        if (product is null)
            return new ServiceResponse(false, "O produto não foi encontrado.");

        var productDescription = new Description(request.Description);
        product = new Product(
            productId: id,
            description: productDescription,
            productionDate: request.ProductionDate,
            expirationDate: request.ExpirationDate,
            isActive: request.IsActive
        );

        var provider = await _providerRepository.GetOneWhere(p => p.CNPJ.Number == request.Provider.CNPJ);

        var providerDescription = new Description(request.Provider.Description);
        var providerCNPJ = new Cnpj(request.Provider.CNPJ);

        if (provider is null)
        {
            provider = new Provider(
                providerId: Guid.NewGuid(),
                description: providerDescription,
                cnpj: providerCNPJ
                );

            product.SetProvider(provider);
            await _providerRepository.Insert(provider);
        }
        else if (provider.IsValid)
        {
            product.SetProvider(provider);
            provider.UpdateDescription(providerDescription);
            await _providerRepository.Update(provider);
        }

        if (!product.IsValid)
            return new ServiceResponse(false, product.Notifications.ToList());

        await _productRepository.Update(product);

        if (await _unitOfWork.SaveChangesAsync() <= 0)
            return new ServiceResponse(false, "Erro ao editar o produto.");

        return new ServiceResponse(true, "Produto editado com sucesso.");
    }

    public async Task<ServiceResponse> Remove(Guid id)
    {
        var product = await _productRepository.GetOneWhere(p => p.Id == id && p.IsActive);

        if (product is null)
            return new ServiceResponse(false, "O produto não foi encontrado.");

        product.Deactivate();

        if (!product.IsValid)
            return new ServiceResponse(false, "Falha na exclusão do produto.");

        await _productRepository.Update(product);

        if (await _unitOfWork.SaveChangesAsync() <= 0)
            return new ServiceResponse(false, "Falha na exclusão do produto.");

        return new ServiceResponse(true, "Produto deletado com sucesso.");
    }

    public async Task<IQueryable<ResponseProductViewModel>> GetAll()
        => _mapper.Map<IEnumerable<ResponseProductViewModel>>
            (await _productRepository.GetAll())
            .AsQueryable();

    public async Task<ResponseProductViewModel> GetById(Guid id)
        => _mapper.Map<ResponseProductViewModel>
            (await _productRepository.GetOneWhere(p => p.Id == id));
}
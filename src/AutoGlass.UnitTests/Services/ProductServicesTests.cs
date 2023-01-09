using AutoGlass.Application.AutoMapper;
using AutoGlass.Application.Interfaces;
using AutoGlass.Application.Services;
using AutoGlass.Application.ViewModels.Products;
using AutoGlass.Application.ViewModels.Providers;
using AutoGlass.Domain.Entities;
using AutoGlass.Domain.Interfaces;
using AutoGlass.Domain.ValueObjects;
using AutoGlass.Infra.Data.Transactions;
using AutoGlass.UnitTests.FakeData;
using AutoMapper;
using Flunt.Notifications;
using Moq;
using System.Linq.Expressions;

namespace AutoGlass.UnitTests.Services;

public sealed class ProductServicesTests
{
    private readonly IProductServices _productServicesValid;
    private readonly IProductServices _productServicesInvalid;
    private readonly Mock<IUnitOfWork> _unitOfWorkValid;
    private readonly Mock<IUnitOfWork> _unitOfWorkInvalid;
    private readonly Mock<IProductRepository> _productRepository;
    private readonly Mock<IProviderRepository> _providerRepository;
    private readonly IMapper _mapper;

    private readonly IReadOnlyCollection<Product> _products;

    public ProductServicesTests()
    {
        _products = new ProductData().GetFakeProducts(5);

        _unitOfWorkValid = new Mock<IUnitOfWork>();
        _unitOfWorkValid.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

        _unitOfWorkInvalid = new Mock<IUnitOfWork>();
        _unitOfWorkInvalid.Setup(x => x.SaveChangesAsync()).ReturnsAsync(0);

        _productRepository = new Mock<IProductRepository>();
        _productRepository.Setup(x => x.GetAll()).ReturnsAsync(_products);
        _productRepository.Setup(x => x.GetOneWhere(It.IsAny<Expression<Func<Product, bool>>>()))!
            .ReturnsAsync(_products.FirstOrDefault());

        _providerRepository = new Mock<IProviderRepository>();
        _providerRepository.Setup(x => x.GetOneWhere(It.IsAny<Expression<Func<Provider, bool>>>()))
            .ReturnsAsync(new ProviderData().ValidProvider);

        var mapProfile = new DomainToViewModelProfile();
        var config = new MapperConfiguration(x => x.AddProfile(mapProfile));
        _mapper = new Mapper(config);

        _productServicesValid = new ProductServices(
            _productRepository.Object,
            _providerRepository.Object,
            _mapper,
            _unitOfWorkValid.Object
            );

        _productServicesInvalid = new ProductServices(
           _productRepository.Object,
           _providerRepository.Object,
           _mapper,
           _unitOfWorkInvalid.Object
           );

        var id = _products.FirstOrDefault()!.Id;

        _productRepository.Setup(x => x.GetOneWhere(It.IsAny<Expression<Func<Product, bool>>>()))!
           .ReturnsAsync(_products.FirstOrDefault(p => p.Id == id));

        var providerCnpj = _products.FirstOrDefault()!.Provider!.CNPJ;

        _providerRepository.Setup(x => x.GetOneWhere(It.IsAny<Expression<Func<Provider, bool>>>()))!
           .ReturnsAsync(_products.FirstOrDefault(p => p.Provider!.CNPJ == providerCnpj)!.Provider!);
    }

    #region GetAll
    [Fact]
    public async Task ShouldReturn_All_Registers()
    {
        var response = await _productServicesValid.GetAll();

        Assert.Equal(5, response.Count());
        Assert.IsAssignableFrom<IQueryable<ResponseProductViewModel>>(response);
        Assert.True(response.Any());
    }
    #endregion

    #region GetById
    [Fact]
    public async Task ShouldReturn_Nothing_When_Product_NotExists()
    {
        var id = Guid.NewGuid();
        _productRepository.Setup(x => x.GetOneWhere(It.IsAny<Expression<Func<Product, bool>>>()))!
            .ReturnsAsync(_products.FirstOrDefault(p => p.Id == id));

        var response = await _productServicesValid.GetById(id);

        Assert.Null(response);
    }

    [Fact]
    public async Task ShouldReturn_Success_When_Product_Exists()
    {
        var product = _products.FirstOrDefault()!;
        var response = await _productServicesValid.GetById(product.Id);

        Assert.Equal(product.Id, response.Id);
        Assert.Equal(product.Description.Text, response.Description);
        Assert.Equal(product.ProductionDate, response.ProductionDate);
        Assert.Equal(product.IsActive, response.IsActive);
        Assert.Equal(product.Provider!.Id, response.Provider!.Id);
        Assert.Equal(product.Provider!.Description.Text, response.Provider!.Description);
        Assert.Equal(product.Provider!.CNPJ.Number, response.Provider!.CNPJ);
        Assert.IsType<ResponseProductViewModel>(response);
    }
    #endregion

    #region Create
    [Fact]
    public async Task Create_ShouldReturn_Error_When_RequestProductViewModel_IsInvalid()
    {
        var request = new RequestProductViewModel()
        {
            Description = "",
            ProductionDate = new DateTime(2023, 01, 05),
            ExpirationDate = new DateTime(2023, 01, 07),
            IsActive = false,
            Provider = new RequestProviderViewModel
            {
                Description = new DescriptionData().InvalidDescription.ToString(),
                CNPJ = new CNPJData().InvalidCNPJ.ToString()
            }
        };

        var response = await _productServicesValid.Create(request);

        Assert.Equal(typeof(List<Notification>), response.Message.GetType());
        Assert.False(response.Success);
    }

    [Fact]
    public async Task Create_ShouldReturn_Error_When_UnitOfWork_NotSaveChanges()
    {
        var request = new RequestProductViewModel()
        {
            Description = "Descrição Válida",
            ProductionDate = new DateTime(2023, 01, 07),
            ExpirationDate = new DateTime(2023, 01, 10),
            IsActive = true,
            Provider = new RequestProviderViewModel
            {
                Description = new DescriptionData().ValidDescription.ToString(),
                CNPJ = new CNPJData().ValidCNPJ.ToString()
            }
        };

        var response = await _productServicesInvalid.Create(request);

        Assert.Equal("Erro ao criar o produto.", response.Message);
        Assert.False(response.Success);
    }

    [Fact]
    public async Task Create_ShouldReturn_Success_When_All_IsCorrect()
    {
        var request = new RequestProductViewModel()
        {
            Description = "Descrição Válida",
            ProductionDate = new DateTime(2023, 01, 07),
            ExpirationDate = new DateTime(2023, 01, 10),
            IsActive = true,
            Provider = new RequestProviderViewModel
            {
                Description = new DescriptionData().ValidDescription.ToString(),
                CNPJ = new CNPJData().ValidCNPJ.ToString()
            }
        };

        var response = await _productServicesValid.Create(request);

        Assert.Equal("Produto criado com sucesso.", response.Message);
        Assert.True(response.Success);
    }
    #endregion

    #region Update
    [Fact]
    public async Task Update_ShouldReturn_Error_When_RequestProductViewModel_IsInvalid()
    {
        var request = new RequestProductViewModel()
        {
            Description = "",
            ExpirationDate = new DateTime(2023, 01, 07),
            ProductionDate = new DateTime(2023, 01, 05),
            IsActive = false,
            Provider = new RequestProviderViewModel
            {
                Description = new DescriptionData().InvalidDescription.ToString(),
                CNPJ = new CNPJData().InvalidCNPJ.ToString()
            }
        };

        var response = await _productServicesValid.Edit(_products.FirstOrDefault()!.Id, request);

        Assert.Equal(typeof(List<Notification>), response.Message.GetType());
        Assert.False(response.Success);
    }

    [Fact]
    public async Task Update_ShouldReturn_Error_When_Product_NotExists()
    {
        var id = Guid.NewGuid();

        _productRepository.Setup(x => x.GetOneWhere(It.IsAny<Expression<Func<Product, bool>>>()))!
           .ReturnsAsync(_products.FirstOrDefault(p => p.Id == id));

        var request = new RequestProductViewModel()
        {
            Description = "Descrição Válida",
            ProductionDate = new DateTime(2023, 01, 07),
            ExpirationDate = new DateTime(2023, 01, 10),
            IsActive = true,
            Provider = new RequestProviderViewModel
            {
                Description = new DescriptionData().ValidDescription.ToString(),
                CNPJ = new CNPJData().ValidCNPJ.ToString()
            }
        };

        var response = await _productServicesValid.Edit(id, request);

        Assert.Equal("O produto não foi encontrado.", response.Message);
        Assert.False(response.Success);
    }

    [Fact]
    public async Task Update_ShouldReturn_Error_When_UnitOfWork_NotSaveChanges()
    {
        var request = new RequestProductViewModel()
        {
            Description = "Descrição Válida",
            ProductionDate = new DateTime(2023, 01, 07),
            ExpirationDate = new DateTime(2023, 01, 10),
            IsActive = true,
            Provider = new RequestProviderViewModel
            {
                Description = new DescriptionData().ValidDescription.ToString(),
                CNPJ = new CNPJData().ValidCNPJ.ToString()
            }
        };

        var response = await _productServicesInvalid.Edit(_products.FirstOrDefault()!.Id, request);

        Assert.Equal("Erro ao editar o produto.", response.Message);
        Assert.False(response.Success);
    }

    [Fact]
    public async Task Update_ShouldReturn_Success_When_Provider_NotExists()
    {
        var providerCnpj = new Cnpj("70490712000111");

        _providerRepository.Setup(x => x.GetOneWhere(It.IsAny<Expression<Func<Provider, bool>>>()))!
           .ReturnsAsync(_products.FirstOrDefault(p => p.Provider?.CNPJ == providerCnpj)?.Provider);

        var request = new RequestProductViewModel()
        {
            Description = "Descrição Válida",
            ProductionDate = new DateTime(2023, 01, 07),
            ExpirationDate = new DateTime(2023, 01, 10),
            IsActive = true,
            Provider = new RequestProviderViewModel
            {
                Description = new DescriptionData().ValidDescription.ToString(),
                CNPJ = new CNPJData().ValidCNPJ.ToString()
            }
        };

        var response = await _productServicesValid.Edit(_products.FirstOrDefault()!.Id, request);

        Assert.Equal("Produto editado com sucesso.", response.Message);
        Assert.True(response.Success);
    }

    [Fact]
    public async Task Update_ShouldReturn_Success_When_Provider_Exists()
    {
        var request = new RequestProductViewModel()
        {
            Description = "Descrição Válida",
            ProductionDate = new DateTime(2023, 01, 07),
            ExpirationDate = new DateTime(2023, 01, 10),
            IsActive = true,
            Provider = new RequestProviderViewModel
            {
                Description = new DescriptionData().ValidDescription.ToString(),
                CNPJ = new CNPJData().ValidCNPJ.ToString()
            }
        };

        var response = await _productServicesValid.Edit(_products.FirstOrDefault()!.Id, request);

        Assert.Equal("Produto editado com sucesso.", response.Message);
        Assert.True(response.Success);
    }
    #endregion

    #region Delete
    [Fact]
    public async Task Delete_ShouldReturn_Error_When_Product_NotExists()
    {
        var productId = Guid.NewGuid();
        _productRepository.Setup(x => x.GetOneWhere(It.IsAny<Expression<Func<Product, bool>>>()))!
           .ReturnsAsync(_products.FirstOrDefault(p => p.Id == productId));

        var response = await _productServicesValid.Remove(productId);

        Assert.Equal("O produto não foi encontrado.", response.Message);
        Assert.False(response.Success);
    }

    [Fact]
    public async Task Delete_ShouldReturn_Error_When_UnitOfWork_NotSaveChanges()
    {
        var productId = _products.FirstOrDefault()!.Id;
        var response = await _productServicesInvalid.Remove(productId);

        Assert.Equal("Falha na exclusão do produto.", response.Message);
        Assert.False(response.Success);
    }

    [Fact]
    public async Task Delete_ShouldReturn_Success_When_All_IsCorrect()
    {
        var productId = _products.FirstOrDefault()!.Id;
        var response = await _productServicesValid.Remove(productId);

        Assert.Equal("Produto deletado com sucesso.", response.Message);
        Assert.True(response.Success);
    }
    #endregion
}
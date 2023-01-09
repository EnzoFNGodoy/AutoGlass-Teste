using AutoGlass.Application.Interfaces.Core;
using AutoGlass.Application.Services.Core;
using AutoGlass.Application.ViewModels.Products;

namespace AutoGlass.Application.Interfaces;

public interface IProductServices : IAppServices<RequestProductViewModel>
{
    Task<IQueryable<ResponseProductViewModel>> GetAll();
    Task<ResponseProductViewModel> GetById(Guid id);

    Task<ServiceResponse> Edit(Guid id, RequestProductViewModel request);
}
using AutoGlass.Application.Services.Core;

namespace AutoGlass.Application.Interfaces.Core;

public interface IAppServices<TRequest>
    where TRequest : class
{
    Task<ServiceResponse> Create(TRequest request);
    Task<ServiceResponse> Remove(Guid id);
}
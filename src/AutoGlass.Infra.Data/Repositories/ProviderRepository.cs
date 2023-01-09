using AutoGlass.Domain.Entities;
using AutoGlass.Domain.Interfaces;
using AutoGlass.Infra.Data.Context;
using AutoGlass.Infra.Data.Repositories.Core;

namespace AutoGlass.Infra.Data.Repositories;

public sealed class ProviderRepository : Repository<Provider>, IProviderRepository
{
    public ProviderRepository(AutoGlassContext context) : base(context)
    { }
}
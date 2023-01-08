using AutoGlass.Domain.Entities;
using AutoGlass.Infra.Data.Context;
using AutoGlass.Infra.Data.Repositories.Core;

namespace AutoGlass.Infra.Data.Repositories;

public sealed class ProviderRepository : Repository<Provider>
{
    public ProviderRepository(AutoGlassContext context) : base(context)
    { }
}
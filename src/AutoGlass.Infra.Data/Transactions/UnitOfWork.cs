using AutoGlass.Infra.Data.Context;

namespace AutoGlass.Infra.Data.Transactions;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly AutoGlassContext _context;

    public UnitOfWork(AutoGlassContext context)
    {
        _context = context;
    }

    public async Task<int> SaveChangesAsync()
        => await _context.SaveChangesAsync();
}

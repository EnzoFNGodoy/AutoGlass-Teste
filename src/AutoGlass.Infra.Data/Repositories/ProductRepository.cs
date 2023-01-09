using AutoGlass.Domain.Entities;
using AutoGlass.Domain.Interfaces;
using AutoGlass.Infra.Data.Context;
using AutoGlass.Infra.Data.Repositories.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AutoGlass.Infra.Data.Repositories;

public sealed class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(AutoGlassContext context) : base(context)
    { }

    public override async Task<IEnumerable<Product>> GetAll()
        => await _dbSet.Include(p => p.Provider).ToListAsync();

    public override async Task<IEnumerable<Product>> GetWhere(Expression<Func<Product, bool>> where)
        => await _dbSet.Include(p => p.Provider).Where(where).ToListAsync();

#nullable disable
    public override async Task<Product> GetOneWhere(Expression<Func<Product, bool>> where)
        => await _dbSet.Include(p => p.Provider).FirstOrDefaultAsync(where);
#nullable enable
}
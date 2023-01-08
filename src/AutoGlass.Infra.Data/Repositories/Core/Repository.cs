using AutoGlass.Domain.Core.Entities;
using AutoGlass.Domain.Core.Interfaces;
using AutoGlass.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AutoGlass.Infra.Data.Repositories.Core;

public class Repository<T> : IRepository<T> where T : Entity
{
    private readonly AutoGlassContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(AutoGlassContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAll() => await _dbSet.ToListAsync();

    public async Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> where)
        => await _dbSet.Where(where).ToListAsync();

    public async Task<T> GetOneWhere(Expression<Func<T, bool>> where)
        => await _dbSet.FirstOrDefaultAsync(where);

    public async Task Insert(T entity) => await _dbSet.AddAsync(entity);

    public async Task Update(T entity) => await Task.Run(() => _dbSet.Update(entity));

    public async Task Delete(T entity) => await Task.Run(() => _dbSet.Remove(entity));
}
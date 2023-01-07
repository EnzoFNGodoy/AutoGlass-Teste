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

    public Task<bool> Delete(T entity)
    {
        _dbSet.Remove(entity);
    }

    public Task<IEnumerable<T>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<T> GetOneWhere(Expression<Func<T, bool>> where)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> where)
    {
        throw new NotImplementedException();
    }

    public Task<T> Insert(T entity)
    {
        throw new NotImplementedException();
    }

    public Task<T> Update(T entity)
    {
        throw new NotImplementedException();
    }
}
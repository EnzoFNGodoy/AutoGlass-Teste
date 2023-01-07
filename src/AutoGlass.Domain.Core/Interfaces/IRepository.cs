﻿using AutoGlass.Domain.Core.Entities;
using System.Linq.Expressions;

namespace AutoGlass.Domain.Core.Interfaces;

public interface IRepository<T> where T : Entity
{
    Task<IEnumerable<T>> GetAll();
    Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> where);
    Task<T> GetOneWhere(Expression<Func<T, bool>> where);

    Task<T> Insert(T entity);
    Task<T> Update(T entity);
    Task<bool> Delete(T entity);
}
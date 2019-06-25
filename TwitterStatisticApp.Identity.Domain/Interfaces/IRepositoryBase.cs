using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace TwitterStatisticApp.Identity.Domain.Interfaces
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        void Add(TEntity obj);
        void Dispose();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> filter);
        IEnumerable<TEntity> GetAll();
        void Remove(Expression<Func<TEntity, bool>> filter);
        void Update(TEntity obj, Expression<Func<TEntity, bool>> filter);
    }
}
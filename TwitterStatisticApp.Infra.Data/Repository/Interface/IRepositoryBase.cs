using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace TwitterStatisticApp.Infra.Data.Repository.Interface
{
    public interface IRepositoryBase<TEntity>
    {
        void Add(TEntity obj);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> filter);
        IEnumerable<TEntity> GetAll();
        void Update(TEntity obj, Expression<Func<TEntity, bool>> filter);
        void Remove(Expression<Func<TEntity, bool>> filter);
    }
}
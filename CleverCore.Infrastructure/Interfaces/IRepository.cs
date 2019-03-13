using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CleverCore.Infrastructure.Interfaces
{
    public interface IRepository<TEntity, in TKey> where TEntity : class
    {
        Task<TEntity> FindById(TKey id, params Expression<Func<TEntity, object>>[] includeProperties);

        Task<TEntity> FindSingle(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);

        IQueryable<TEntity> FindAll(params Expression<Func<TEntity, object>>[] includeProperties);

        IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);

        Task Add(TEntity entity);

        void Update(TEntity entity);

        void Remove(TEntity entity);

        Task Remove(TKey id);

        void RemoveMultiple(List<TEntity> entities);
    }
}

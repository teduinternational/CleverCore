using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CleverCore.Infrastructure.Interfaces;
using CleverCore.Infrastructure.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace CleverCore.Data.EF
{
    public class EfRepository<TEntity, TKey> : IRepository<TEntity, TKey>, IDisposable
        where TEntity : DomainEntity<TKey>
    {
        private readonly AppDbContext _context;

        public EfRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public void Add(TEntity entity)
        {
            _context.Add(entity);
        }

        public IQueryable<TEntity> FindAll(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> items = _context.Set<TEntity>();
            if (includeProperties != null)
                foreach (var includeProperty in includeProperties)
                    items = items.Include(includeProperty);
            return items;
        }

        public IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> items = _context.Set<TEntity>();
            if (includeProperties != null)
                foreach (var includeProperty in includeProperties)
                    items = items.Include(includeProperty);
            return items.Where(predicate);
        }

        public TEntity FindById(TKey id, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return FindAll(includeProperties).SingleOrDefault(x => x.Id.Equals(id));
        }

        public TEntity FindSingle(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return FindAll(includeProperties).SingleOrDefault(predicate);
        }

        public void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public void Remove(TKey id)
        {
            var entity = FindById(id);
            Remove(entity);
        }

        public void RemoveMultiple(List<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }
    }
}
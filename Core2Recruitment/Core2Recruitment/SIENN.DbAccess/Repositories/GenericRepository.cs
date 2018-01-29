using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SIENN.DbAccess.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        internal GenericRepository(DbContext context)
        {
            Context = context;
            Entities = context.Set<TEntity>();
        }

        public virtual TEntity Get(int id)
        {
            return Entities.Find(id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return Entities.ToList();
        }

        public virtual IEnumerable<TEntity> GetRange(int start, int count)
        {
            return Entities.Skip(start).Take(count).ToList();
        }

        public virtual IEnumerable<TEntity> GetRange(int start, int count, Expression<Func<TEntity, bool>> predicate)
        {
            return Entities.Where(predicate).Skip(start).Take(count).ToList();
        }

        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Entities.Where(predicate);
        }

        public virtual int Count()
        {
            return Entities.Count();
        }

        public virtual void Add(TEntity entity)
        {
            Entities.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            Entities.AddRange(entities);
        }

        public virtual void Remove(TEntity entity)
        {
            Entities.Remove(entity);
        }

        protected DbSet<TEntity> Entities;
        protected DbContext Context;
    }
}

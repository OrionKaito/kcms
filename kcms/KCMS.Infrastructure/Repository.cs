using KCMS.Domain.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace KCMS.Infrastructure
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {

        private readonly DbFactory _dbFactory;
        private DbSet<TEntity> _dbSet;

        protected DbSet<TEntity> DbSet
        {
            get => _dbSet ?? (_dbSet = _dbFactory.DbContext.Set<TEntity>());
        }

        public Repository(DbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public virtual IQueryable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query);
            }
            else
            {
                return query;
            }
        }

        public virtual TEntity GetByID(object id)
        {
            return DbSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            if (typeof(IAuditEntity).IsAssignableFrom(typeof(TEntity)))
            {
                ((IAuditEntity)entity).CreatedDate = DateTime.UtcNow;
            }
            DbSet.Add(entity);
        }

        public void Delete(TEntity entity)
        {
            DbSet.Remove(entity);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            if (typeof(IAuditEntity).IsAssignableFrom(typeof(TEntity)))
            {
                ((IAuditEntity)entityToUpdate).UpdatedDate = DateTime.UtcNow;
            }
            DbSet.Update(entityToUpdate);
        }

        public int GetTotalPages(Expression<Func<TEntity, bool>> filter = null, int pageSize = 10)
        {
            IQueryable<TEntity> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            var count = query.Count();
            return (int)Math.Ceiling(count / (double)pageSize);
        }
    }
}

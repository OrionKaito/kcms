using System;
using System.Linq;
using System.Linq.Expressions;

namespace KCMS.Domain.Base
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, 
            string includeProperties = "");
        int GetTotalPages(Expression<Func<TEntity, bool>> filter = null, int pageSize = 10);
        TEntity GetByID(object id);
        void Insert(TEntity entity);
        void Delete(TEntity entity);
        void Update(TEntity entity);
    }
}

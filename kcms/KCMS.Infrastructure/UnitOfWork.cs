using KCMS.Domain.Base;
using System.Threading.Tasks;

namespace KCMS.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbFactory _dbFactory;

        public UnitOfWork(DbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public Task CommitAsync()
        {
            return _dbFactory.DbContext.SaveChangesAsync();
        }
    }
}
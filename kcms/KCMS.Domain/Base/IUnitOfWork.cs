using System.Threading.Tasks;

namespace KCMS.Domain.Base
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
    }
}

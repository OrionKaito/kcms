using KCMS.Domain.Advertising;
using KCMS.Domain.Team;

namespace KCMS.Infrastructure.Repositories
{
    public class AdvertisingRepository : Repository<Advertising>, IAdvertisingRepository
    {
        public AdvertisingRepository(DbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

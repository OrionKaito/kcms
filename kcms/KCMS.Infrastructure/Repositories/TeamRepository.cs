using KCMS.Domain.Advertising;
using KCMS.Domain.Team;

namespace KCMS.Infrastructure.Repositories
{
    public class TeamRepository : Repository<Team>, ITeamRepository
    {
        public TeamRepository(DbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

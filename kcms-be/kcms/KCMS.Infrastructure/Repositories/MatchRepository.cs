using KCMS.Domain.Match;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace KCMS.Infrastructure.Repositories
{
    public class MatchRepository : Repository<Match>, IMatchRepository
    {
        public MatchRepository(DbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

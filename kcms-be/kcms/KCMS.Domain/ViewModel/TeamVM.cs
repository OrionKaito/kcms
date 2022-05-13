using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace KCMS.Domain.ViewModel
{
    public class RankingListViewModel
    {
        public IEnumerable<Team.Team> Results { get; set; }
        public int TotalPages { get; set; }
    }

    public class TeamInsertModel
    {
        public string TeamName { get; set; }
        public IFormFile Image { get; set; }
        public long LeagueId { get; set; }
        public int ST { get; set; }
        public int T { get; set; }
        public int H { get; set; }
        public int B { get; set; }
        public int TG { get; set; }
        public int TH { get; set; }
        public int HS { get; set; }
        public int D { get; set; }
    }

    public class TeamUpdateModel : TeamInsertModel
    {
        public long Id { get; set; }
    }
}

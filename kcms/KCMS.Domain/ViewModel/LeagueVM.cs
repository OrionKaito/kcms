using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace KCMS.Domain.ViewModel
{
    public class LeagueInsertModel
    {
        public string Name { get; set; }
        public IFormFile Image { get; set; }
    }

    public class LeagueListViewModel
    {
        public IEnumerable<League.League> Results { get; set; }
        public int TotalPages { get; set; }
    }

    public class LeagueUpdateModel : LeagueInsertModel
    {
        public long Id { get; set; }
    }
}

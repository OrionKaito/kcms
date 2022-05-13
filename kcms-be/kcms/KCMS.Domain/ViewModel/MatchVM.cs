using KCMS.Domain.Base;
using System;
using System.Collections.Generic;

namespace KCMS.Domain.ViewModel
{
    public class MatchInsertModel
    {
        public float HomePoints { get; set; }
        public float GuestPoints { get; set; }
        public long HomeTeamId { get; set; }
        public long GuestTeamId { get; set; }
        public long LeagueId { get; set; }
        public string Video { get; set; }
        public string Slug { get; set; }
        public string Commentator { get; set; }
        public MatchType MatchType { get; set; }
        public MatchStatus Status { get; set; }
        public DateTime Time { get; set; }
    }

    public class MatchScheduleListViewModel
    {
        public IEnumerable<MatchScheduleViewModel> Results { get; set; }
        public int TotalPages { get; set; }
    }

    public class MatchScheduleViewModel
    {
        public long Id { get; set; }
        public DateTime Time { get; set; }
        public string HomeTeamName { get; set; }
        public string HomeTeamImage { get; set; }
        public string GuestTeamName { get; set; }
        public string GuestTeamImage { get; set; }
        public string LeagueName { get; set; }
        public string Commentator { get; set; }
    }

    public class MatchViewModel
    {
        public long Id { get; set; }
        public DateTime Time { get; set; }
        public string HomeTeamName { get; set; }
        public string HomeTeamImage { get; set; }
        public long HomeTeamId { get; set; }
        public string GuestTeamName { get; set; }
        public string GuestTeamImage { get; set; }
        public long GuestTeamId { get; set; }
        public string LeagueId { get; set; }
        public string LeagueName { get; set; }
        public string Video { get; set; }
        public string Commentator { get; set; }
        public MatchType MatchType { get; set; }
        public MatchStatus Status { get; set; }
        public float HomePoints { get; set; }
        public float GuestPoints { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class MatchListViewModel
    {
        public IEnumerable<MatchViewModel> Results { get; set; }
        public int TotalPages { get; set; }
    }

    public class MatchUpdateModel : MatchInsertModel
    {
        public long Id { get; set; }
    }
}

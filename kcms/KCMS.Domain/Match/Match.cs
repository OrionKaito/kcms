using KCMS.Domain.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace KCMS.Domain.Match
{
    [Table("Matchs")]
    public class Match : AuditEntity
    {
        public string Video { get; set; }
        public string Slug { get; set; }
        public MatchType MatchType { get; set; }
        public MatchStatus Status { get; set; }
        public DateTime Time { get; set; }
        public float HomePoints { get; set; }
        public float GuestPoints { get; set; }
        public string Commentator { get; set; }

        public long HomeTeamId { get; set; }
        public virtual Team.Team HomeTeam { get; set; }

        public long GuestTeamId { get; set; }
        public virtual Team.Team GuestTeam { get; set; }

        public long LeagueId { get; set; }
        public virtual League.League League { get; set; }
    }
}

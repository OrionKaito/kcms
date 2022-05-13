using KCMS.Domain.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KCMS.Domain.Team
{
    [Table("Teams")]
    public class Team : AuditEntity
    {
        [MaxLength(100)]
        public string TeamName { get; set; }
        public string Image { get; set; }
        public int ST { get; set; }
        public int T { get; set; }
        public int H { get; set; }
        public int B { get; set; }
        public int TG { get; set; }
        public int TH { get; set; }
        public int HS { get; set; }
        public int D { get; set; }
        public long LeagueId { get; set; }
        public virtual League.League League { get; set; }
    }
}

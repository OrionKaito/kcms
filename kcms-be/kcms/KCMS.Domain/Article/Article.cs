using KCMS.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KCMS.Domain.Article
{
    [Table("Articles")]
    public class Article : AuditEntity
    {
        [MaxLength(100)]
        public string Title { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public string Video { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public ArticleType Type { get; set; }
        public long LeagueId { get; set; }
        public virtual League.League League { get; set; }
    }
}

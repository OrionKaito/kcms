using KCMS.Domain.Base;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KCMS.Domain.ViewModel
{
    public class ArticleInsertModel
    {
        public long LeagueId { get; set; }
        [MaxLength(100)]
        public string Title { get; set; }
        public string Content { get; set; }
        public string Video { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public ArticleType Type { get; set; }
    }

    public class ArticleListViewModel
    {
        public IEnumerable<Article.Article> Results { get; set; }
        public int TotalPages { get; set; }
    }

    public class ArticleUpdateModel : ArticleInsertModel
    {
        public long Id { get; set; }
    }
}

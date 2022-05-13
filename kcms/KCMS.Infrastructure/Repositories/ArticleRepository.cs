using KCMS.Domain.Article;

namespace KCMS.Infrastructure.Repositories
{
    public class ArticleRepository : Repository<Article>, IArticleRepostiory
    {
        public ArticleRepository(DbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

using KCMS.Domain.Article;
using KCMS.Domain.Base;
using KCMS.Domain.ViewModel;
using KCMS.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KCMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly ArticleService _articleService;

        public ArticlesController(ArticleService articleService)
        {
            _articleService = articleService;
        }

        // GET: api/Articles
        [HttpGet]
        public ActionResult<ArticleListViewModel> GetArticles(ArticleType? type, int pageNumber = 1, int pageSize = 10, long leagueId = 0, string searchValue = "")
        {
            return Ok(_articleService.GetArticles(pageNumber, pageSize, type, leagueId, searchValue));
        }

        // GET: api/Articles/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public ActionResult<Article> GetArticle(long id)
        {
            var article = _articleService.GetArticle(id);

            if (article == null)
            {
                return NotFound();
            }

            return article;
        }

        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> PutArticle([FromForm] ArticleUpdateModel model)
        {
            await _articleService.UpdateArticle(model);

            return NoContent();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Article>> PostArticle([FromForm] ArticleInsertModel model)
        {
            var article = await _articleService.AddArticle(model);

            return CreatedAtAction("GetArticle", new { id = article.Id }, article);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Article>> DeleteArticle(long id)
        {
            var article = await _articleService.DeleteArticle(id);

            return article;
        }
    }
}

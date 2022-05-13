using AutoMapper;
using KCMS.AppConfigs;
using KCMS.Domain.Article;
using KCMS.Domain.Base;
using KCMS.Domain.ViewModel;
using KCMS.Ultitlies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace KCMS.Services
{
    public class ArticleService
    {
        private readonly IArticleRepostiory _articleRepostiory;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public ArticleService(IHostingEnvironment hostingEnvironment, IUnitOfWork unitOfWork, IArticleRepostiory articleRepostiory, IMapper mapper, IConfiguration config)
        {
            _hostingEnvironment = hostingEnvironment;
            _unitOfWork = unitOfWork;
            _articleRepostiory = articleRepostiory;
            _mapper = mapper;
            _config = config;
        }

        public ArticleListViewModel GetArticles(int pageNumber, int pageSize, ArticleType? type, long leagueId, string searchValue)
        {
            Expression<Func<Article, bool>> filter = a => (leagueId == 0 || a.LeagueId == leagueId)
            && (type == null || a.Type == type)
            && (String.IsNullOrEmpty(searchValue) || ((a.Title.Contains(searchValue)) || (a.Id.ToString() == searchValue) || (a.League.Name.Contains(searchValue))));

            var articles = _articleRepostiory.Get(filter, a => a.OrderByDescending(a => a.CreatedDate), "League").Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            var maxLenght = _config.GetValue<int>("AppSettings:MaxTitleLength");
            for (int i = 0; i < articles.Count(); i++)
            {
                articles[i].Title = articles[i].Title.Truncate(maxLenght);
            }

            return new ArticleListViewModel
            {
                TotalPages = _articleRepostiory.GetTotalPages(filter, pageSize),
                Results = articles
            };
        }

        public Article GetArticle(long id)
        {
            var article = _articleRepostiory.GetByID(id);

            if (article == null)
            {
                throw new Exception("NotFound");
            }

            return article;
        }

        public async Task<Article> AddArticle(ArticleInsertModel model)
        {
            var fileName = "";

            if (model.Image != null)
            {
                fileName = FileUlti.UploadFile(model.Image, _hostingEnvironment);
            }

            var article = _mapper.Map<Article>(model);
            article.Image = fileName;
            article.CreatedDate = DateTime.UtcNow;

            if (!String.IsNullOrEmpty(model.Slug))
            {
                article.Slug = model.Slug.Slugify();
            }
            else
            {
                article.Slug = model.Title.Slugify();
            }

            try
            {
                _articleRepostiory.Insert(article);
                await _unitOfWork.CommitAsync();
                return article;
            }
            catch (Exception ex)
            {
                throw new Exception("Insert Fail : " + ex);
            }
        }

        public async Task UpdateArticle(ArticleUpdateModel model)
        {
            var article = _articleRepostiory.GetByID(model.Id);

            if (article == null)
            {
                throw new Exception("NotFound");
            }

            if (model.Image != null)
            {
                FileUlti.DeleteFile(article.Image, _hostingEnvironment);
                var fileName = FileUlti.UploadFile(model.Image, _hostingEnvironment);
                article.Image = fileName;
            }

            if (!String.IsNullOrEmpty(model.Slug))
            {
                article.Slug = model.Slug.Slugify();
            }
            else
            {
                article.Slug = model.Title.Slugify();
            }

            article.Content = model.Content;
            article.Title = model.Title;
            article.Type = model.Type;
            article.Video = model.Video;
            article.UpdatedDate = DateTime.UtcNow;

            try
            {
                _articleRepostiory.Update(article);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Update Fail : " + ex);
            }
        }

        public async Task<Article> DeleteArticle(long id)
        {
            var article = _articleRepostiory.GetByID(id);
            var image = article.Image;

            if (article == null)
            {
                throw new Exception("NotFound");
            }

            _articleRepostiory.Delete(article);
            await _unitOfWork.CommitAsync();
            FileUlti.DeleteFile(image, _hostingEnvironment);
            return article;
        }
    }
}

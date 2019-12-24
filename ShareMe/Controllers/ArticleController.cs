using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Mvc;
using ShareMe.DAL.UnitOfWork;

namespace ShareMe.Controllers
{
    public class ArticleController : Controller
    {
        IUnitOfWork unitOfWork;

        public ArticleController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IActionResult Index(int? articleId, int? categoryId, int? tagId, int? authorId)
        {
            var articles = unitOfWork.ArticleRepository.Get(x => 
                (articleId==null || x.ArticleId==articleId)&&
                (categoryId == null || x.Category.CategoryId== categoryId) &&
                //(tagId == null || x.ArticleTags.Any(y=>y.TagId == tagId) ) &&
                (authorId == null || x.Author.AuthorId == authorId)
                );
            return View(articles);
        }

        public IActionResult Details(int? articleId)
        {
            var article = unitOfWork.ArticleRepository.GetById(articleId);
            if (article==null)
            {
                return NotFound();
            }
            return View(article);
        }
    }
}
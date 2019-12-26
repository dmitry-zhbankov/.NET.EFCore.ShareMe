using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShareMe.DAL.UnitOfWork;
using ShareMe.Models.Entity;

namespace ShareMe.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArticleController : Controller
    {
        IUnitOfWork unitOfWork;

        public ArticleController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [Area("Admin")]
        public IActionResult Index(int? categoryId, int? tagId, int? authorId)
        {
            var articles = unitOfWork.ArticleRepository.Get(x =>
                (categoryId == null || x.Category.CategoryId == categoryId) &&
                (authorId == null || x.Author.AuthorId == authorId)
            );
            return View(articles);
        }

        public IActionResult Edit(int? articleId)
        {
            if (articleId == null)
            {
                return NotFound();
            }

            var article = unitOfWork.ArticleRepository.GetById(articleId);
            return View("Update",article);
        }

        public IActionResult Update()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Update(Article article, IFormFile preview)
        {
            if (article == null)
            {
                return BadRequest();
            }

            var articleToUpdate = unitOfWork.ArticleRepository.GetById(article.ArticleId);

            if (articleToUpdate==null)
            {
                article.Date = DateTime.Now;
            }
            else
            {
                articleToUpdate.Content = article.Content;
                articleToUpdate.Annotation = article.Annotation;
                MemoryStream ms=new MemoryStream();
                preview.CopyTo(ms);
                articleToUpdate.Preview = ms.ToArray();
            }

            try
            {
                unitOfWork.ArticleRepository.Update(articleToUpdate);
                unitOfWork.Save();
            }
            catch
            {
                return View();
            }

            return RedirectToAction("Index");
        }

    }
}
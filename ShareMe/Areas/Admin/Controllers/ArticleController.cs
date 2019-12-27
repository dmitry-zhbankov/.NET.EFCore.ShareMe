using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShareMe.DAL.UnitOfWork;
using ShareMe.Models.Entity;
using ShareMe.Models.ViewModel;

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

        //public IActionResult Edit(int? articleId)
        //{
        //    if (articleId == null)
        //    {
        //        return NotFound();
        //    }

        //    var article = unitOfWork.ArticleRepository.GetById(articleId);
        //    return View("Update", article);
        //}

        public IActionResult Update(int? authorId, int? categoryId, int? articleId)
        {
            var article = unitOfWork.ArticleRepository.GetById(articleId);

            var authors = authorId == null
                ? unitOfWork.AuthorRepository.Get(x => true)
                : unitOfWork.AuthorRepository.Get(x => x.AuthorId == authorId);
            var categories = categoryId == null
                ? unitOfWork.CategoryRepository.Get(x => true)
                : unitOfWork.CategoryRepository.Get(x => x.CategoryId == categoryId);

            var tags = unitOfWork.TagRepository.Get(x => true).Select(x => new TagViewModel()
            {
                Tag = x,
                IsChecked = x.ArticleTags.Any(y => y.ArticleId == article?.ArticleId)
            });

            var articleViewModel = new ArticleViewModel()
            {
                Article = article,
                Authors = authors,
                Categories = categories,
                Tags = tags
            };

            return View(articleViewModel);
        }

        [HttpPost]
        public ActionResult Update(ArticleViewModel articleViewModel, IFormFile preview)
        {
            if (articleViewModel == null)
            {
                return BadRequest();
            }

            var articleToUpdate = unitOfWork.ArticleRepository.GetById(articleViewModel.Article.ArticleId);

            if (articleToUpdate == null)
            {
                articleViewModel.Article.Date = DateTime.Now;

                var author = unitOfWork.AuthorRepository.GetById(articleViewModel.Author.AuthorId);
                var category = unitOfWork.CategoryRepository.GetById(articleViewModel.Category.CategoryId);

                articleToUpdate = new Article()
                {
                    Author = author,
                    Category = category
                };
            }

            articleToUpdate.Content = articleViewModel.Article.Content;
            articleToUpdate.Annotation = articleViewModel.Article.Annotation;
            var ms = new MemoryStream();
            preview.CopyTo(ms);
            articleToUpdate.Preview = ms.ToArray();

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
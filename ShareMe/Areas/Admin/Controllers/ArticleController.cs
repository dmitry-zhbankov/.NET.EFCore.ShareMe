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

        public IActionResult Index(int? categoryId, int? tagId, int? authorId)
        {
            var articles = unitOfWork.ArticleRepository.Get(x =>
                (categoryId == null || x.Category.CategoryId == categoryId) &&
                (authorId == null || x.Author.AuthorId == authorId)
            );
            return View(articles);
        }

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
                Tags = new List<TagViewModel>(tags)
            };

            return View(articleViewModel);
        }

        [HttpPost]
        public IActionResult Update(ArticleViewModel articleViewModel, IFormFile preview)
        {
            if (articleViewModel == null)
            {
                return BadRequest();
            }

            var articleToUpdate = unitOfWork.ArticleRepository.GetById(articleViewModel.Article.ArticleId);

            if (articleToUpdate == null)
            {
                var author = unitOfWork.AuthorRepository.GetById(articleViewModel.Author.AuthorId);
                var category = unitOfWork.CategoryRepository.GetById(articleViewModel.Category.CategoryId);

                articleToUpdate = new Article()
                {
                    Author = author,
                    Category = category,
                    ArticleTags = new List<ArticleTag>(),
                    Date = DateTime.Now
                };
            }

            articleToUpdate.Content = articleViewModel.Article.Content;
            articleToUpdate.Annotation = articleViewModel.Article.Annotation;

            if (preview != null)
            {
                var ms = new MemoryStream();
                preview.CopyTo(ms);
                articleToUpdate.Preview = ms.ToArray();
            }
            else
            {
                articleToUpdate.Preview = articleViewModel.Article.Preview;
            }

            var tags = articleViewModel.Tags.Where(x => x.IsChecked).Select(x => x.Tag);

            articleToUpdate.ArticleTags = tags.Select(x => new ArticleTag()
            {
                TagId = x.TagId,
                ArticleId = articleToUpdate.ArticleId
            });

            try
            {
                unitOfWork.ArticleRepository.Update(articleToUpdate);
                unitOfWork.Save();
            }
            catch
            {
                return View(articleViewModel);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? articleId)
        {            
            if (articleId == null)
            {
                return BadRequest();
            }
            try
            {
                unitOfWork.ArticleRepository.Delete((int)articleId);
                unitOfWork.Save();
                ViewBag.Message = "Delete succeeded";
            }
            catch
            {
                ViewBag.Message = "Delete failed";
            }
            return RedirectToAction("Index");
        }
    }
}
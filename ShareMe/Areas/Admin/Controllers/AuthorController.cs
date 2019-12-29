using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShareMe.DAL.UnitOfWork;
using ShareMe.Models.Entity;

namespace ShareMe.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthorController : Controller
    {
        IUnitOfWork unitOfWork;
        public AuthorController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var authors = unitOfWork.AuthorRepository.Get(x => true);
            return View(authors);
        }
        public IActionResult Update(int? authorId)
        {
            var author = unitOfWork.AuthorRepository.GetById(authorId);
            if (author == null)
            {
                author = new Author();
            }
            return View(author);
        }

        [HttpPost]
        public IActionResult Update(Author author)
        {
            if (author == null)
            {
                return BadRequest();
            }

            var authorToUpdate = unitOfWork.AuthorRepository.GetById(author.AuthorId);
            if (authorToUpdate == null)
            {
                return BadRequest();
            }

            authorToUpdate.Name = author.Name;

            try
            {
                unitOfWork.AuthorRepository.Update(authorToUpdate);
                unitOfWork.Save();
                ViewBag.Message = "Update succeeded";
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Message = "Update failed";
                return View(author);
            }
        }

        public IActionResult Delete(int? authorId)
        {
            if (authorId == null)
            {
                return BadRequest();
            }
            try
            {
                unitOfWork.AuthorRepository.Delete((int)authorId);
                unitOfWork.Save();
                ViewBag.Message = "Delete succeeded";
            }
            catch
            {
                ViewBag.Message = "Update failed";
            }
            return RedirectToAction("Index");
        }
    }
}
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
    public class CategoryController : Controller
    {
        IUnitOfWork unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var categories = unitOfWork.CategoryRepository.Get(x => true);
            return View(categories);
        }
        public IActionResult Update(int? categoryId)
        {
            var category = unitOfWork.CategoryRepository.GetById(categoryId);
            if (category == null)
            {
                category = new Category();
            }
            return View(category);
        }

        [HttpPost]
        public IActionResult Update(Category category)
        {
            if (category == null)
            {
                return BadRequest();
            }

            var categoryToUpdate = unitOfWork.CategoryRepository.GetById(category.CategoryId);
            if (categoryToUpdate == null)
            {
                return BadRequest();
            }

            categoryToUpdate.Title = category.Title;

            try
            {
                unitOfWork.CategoryRepository.Update(categoryToUpdate);
                unitOfWork.Save();
                ViewBag.Message = "Update succeeded";
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Message = "Update failed";
                return View(category);
            }
        }

        public IActionResult Delete(int? CategoryId)
        {
            if (CategoryId == null)
            {
                return BadRequest();
            }
            try
            {
                unitOfWork.CategoryRepository.Delete((int)CategoryId);
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
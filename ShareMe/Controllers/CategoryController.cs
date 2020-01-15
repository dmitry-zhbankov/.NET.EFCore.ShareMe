using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShareMe.DAL.UnitOfWork;

namespace ShareMe.Controllers
{
    public class CategoryController : Controller
    {
        private IUnitOfWork unitOfWork;

        public IActionResult Index(IUnitOfWork unitOfWork, int? categoryId)
        {
            this.unitOfWork = unitOfWork;
            var categories = unitOfWork.CategoryRepository.Get(x => true);
            ViewBag.CategoryId = categoryId;
            return PartialView(categories);
        }
    }
}
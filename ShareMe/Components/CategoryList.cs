using Microsoft.AspNetCore.Mvc;
using ShareMe.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShareMe.ViewComponents
{    
    public class CategoryList : ViewComponent
    {
        IUnitOfWork unitOfWork;
        public CategoryList(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? categoryId)
        {
            var categories = unitOfWork.CategoryRepository.Get(x => true);
            return await Task.Run(()=>View("_CategoryList", categories));
        }
    }
}

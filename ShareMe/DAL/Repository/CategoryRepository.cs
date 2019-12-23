using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShareMe.DAL.Context;
using ShareMe.Models.Entity;

namespace ShareMe.DAL.Repository
{
    public class CategoryRepository:GenericRepository<Category>,ICategoryRepository
    {
        public CategoryRepository(ShareMeContext context) : base(context)
        {
        }
    }
}

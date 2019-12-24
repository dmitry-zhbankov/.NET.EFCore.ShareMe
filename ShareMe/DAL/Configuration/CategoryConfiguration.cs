using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShareMe.Models.Entity;

namespace ShareMe.DAL.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(CreateCategories());
        }

        private IEnumerable<Category> CreateCategories()
        {
            var list = new List<Category>();
            list.Add(new Category()
            {
                CategoryId=1,
                Title = "Category 1"
            });
            list.Add(new Category()
            {
                CategoryId = 2,
                Title = "Category 2"
            });
            list.Add(new Category()
            {
                CategoryId = 3,
                Title = "Category 3"
            });
            list.Add(new Category()
            {
                CategoryId = 4,
                Title = "Category 4"
            });
            return list;
        }
    }
}

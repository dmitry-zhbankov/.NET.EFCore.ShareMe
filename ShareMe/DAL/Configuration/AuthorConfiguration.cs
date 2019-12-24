using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShareMe.Models.Entity;

namespace ShareMe.DAL.Configuration
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasData(CreateAuthors());
        }

        private IEnumerable<Author> CreateAuthors()
        {
            var list = new List<Author>();
            list.Add(new Author()
            {
                AuthorId=1,
                Name="Author 1"
            });
            list.Add(new Author()
            {
                AuthorId = 2,
                Name = "Author 2"
            });
            list.Add(new Author()
            {
                AuthorId = 3,
                Name = "Author 3"
            });
            return list;
        }
    }
}

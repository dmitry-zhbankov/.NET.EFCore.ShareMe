using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShareMe.Models.Entity;

namespace ShareMe.DAL.Configuration
{
    public class ArticleConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            //builder.HasData(CreateArticles());
        }

        private IEnumerable<object> CreateArticles()
        {
            var list = new List<object>();
            using (var stream = new FileStream("Migrations\\Data\\preview 1.jpg", FileMode.Open))
            {
                using var reader = new BinaryReader(stream);
                var bytes = reader.ReadBytes((int)stream.Length);
                list.Add(new
                {
                    ArticleId = 1,
                    Title = "Article 1 Title",
                    Annotation = "Article 1 annotation",
                    Content = "Article 1 content",
                    Date = new DateTime(2019, 1, 1),
                    AuthorId = 1,
                    CategoryId = 1,
                    Views = 0,
                    Preview = bytes
                });
            }

            using (var stream = new FileStream("Migrations\\Data\\preview 2.jpg", FileMode.Open))
            {
                using var reader = new BinaryReader(stream);
                var bytes = reader.ReadBytes((int)stream.Length);
                list.Add(new
                {
                    ArticleId = 2,
                    Title = "Article 2 Title",
                    Annotation = "Article 2 annotation",
                    Content = "Article 2 content",
                    Date = new DateTime(2019, 2, 1),
                    AuthorId = 1,
                    CategoryId = 2,
                    Views = 0,
                    Preview = bytes
                });
            }

            using (var stream = new FileStream("Migrations\\Data\\preview 3.jpg", FileMode.Open))
            {
                using var reader = new BinaryReader(stream);
                var bytes = reader.ReadBytes((int)stream.Length);
                list.Add(new
                {
                    ArticleId = 3,
                    Title = "Article 3 Title",
                    Annotation = "Article 3 annotation",
                    Content = "Article 3 content",
                    Date = new DateTime(2019, 3, 1),
                    AuthorId = 2,
                    CategoryId = 1,
                    Views = 0,
                    Preview = bytes
                });
            }

            using (var stream = new FileStream("Migrations\\Data\\preview 4.jpg", FileMode.Open))
            {
                using var reader = new BinaryReader(stream);
                var bytes = reader.ReadBytes((int)stream.Length);
                list.Add(new
                {
                    ArticleId = 4,
                    Title = "Article 4 Title",
                    Annotation = "Article 4 annotation",
                    Content = "Article 4 content",
                    Date = new DateTime(2019, 4, 1),
                    AuthorId = 2,
                    CategoryId = 3,
                    Views = 0,
                    Preview = bytes
                });
            }

            using (var stream = new FileStream("Migrations\\Data\\preview 5.jpg", FileMode.Open))
            {
                using var reader = new BinaryReader(stream);
                var bytes = reader.ReadBytes((int)stream.Length);
                list.Add(new
                {
                    ArticleId = 5,
                    Title = "Article 5 Title",
                    Annotation = "Article 5 annotation",
                    Content = "Article 5 content",
                    Date = new DateTime(2019, 5, 1),
                    AuthorId = 3,
                    CategoryId = 4,
                    Views = 0,
                    Preview = bytes
                });
            }

            return list;
        }
    }
}

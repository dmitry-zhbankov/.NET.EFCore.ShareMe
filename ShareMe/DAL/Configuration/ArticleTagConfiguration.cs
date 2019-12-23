using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShareMe.Models.Entity;

namespace ShareMe.DAL.Configuration
{
    public class ArticleTagConfiguration : IEntityTypeConfiguration<ArticleTag>
    {
        public void Configure(EntityTypeBuilder<ArticleTag> builder)
        {
            builder.HasKey(t => new { t.ArticleId, t.TagId });

            builder.HasOne(pt => pt.Article)
                .WithMany(p => p.ArticleTags)
                .HasForeignKey(pt => pt.ArticleId);

            builder.HasOne(pt => pt.Tag)
                .WithMany(t => t.ArticleTags)
                .HasForeignKey(pt => pt.TagId);
        }
    }
}

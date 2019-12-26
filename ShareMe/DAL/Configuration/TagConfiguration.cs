using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShareMe.Models.Entity;

namespace ShareMe.DAL.Configuration
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            //builder.HasData(CreateTags());
        }

        private IEnumerable<Tag> CreateTags()
        {
            var list = new List<Tag>();
            list.Add(new Tag()
            {
                TagId=1,
                Name="Tag 1",
            });
            list.Add(new Tag()
            {
                TagId = 2,
                Name = "Tag 2",
            });
            list.Add(new Tag()
            {
                TagId = 3,
                Name = "Tag 3",
            });
            return list;
        }
    }
}

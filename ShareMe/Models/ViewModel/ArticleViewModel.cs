using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShareMe.Models.Entity;

namespace ShareMe.Models.ViewModel
{
    public class ArticleViewModel
    {
        public Article Article { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public Category Category { get; set; }
        public IEnumerable<TagViewModel> Tags { get; set; }
        public IEnumerable<Author> Authors { get; set; }
        public Author Author { get; set; }
    }
}

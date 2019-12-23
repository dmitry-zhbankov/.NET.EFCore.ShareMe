using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShareMe.Models.Entity
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }
        private ICollection<Article> Articles { get; set; }
    }
}
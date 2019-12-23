using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShareMe.Models.Entity
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Title { get; set; }
        private ICollection<Article> Articles { get; set; }
    }
}
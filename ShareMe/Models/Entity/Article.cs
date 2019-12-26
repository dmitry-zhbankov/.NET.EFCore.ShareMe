using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShareMe.Models.Entity
{
    public class Article
    {
        public int ArticleId { get; set; }
        public string Title { get; set; }
        public string Annotation { get; set; }
        public string Content { get; set; }
        [Required] public Category Category { get; set; }
        [Required] public Author Author { get; set; }
        public ICollection<ArticleTag> ArticleTags { get; set; }
        [Required] public DateTime Date { get; set; }
        public int Views { get; set; }
        public byte[] Preview { get; set; }
    }
}
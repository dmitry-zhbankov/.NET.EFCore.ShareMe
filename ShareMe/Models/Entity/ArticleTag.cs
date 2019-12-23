using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShareMe.Models.Entity
{
    public class ArticleTag
    {
        public int ArticleId { get; set; }
        [Required] public Article Article { get; set; }

        public int TagId { get; set; }
        [Required] public Tag Tag { get; set; }
    }
}

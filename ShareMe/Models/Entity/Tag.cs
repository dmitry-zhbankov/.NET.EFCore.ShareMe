﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShareMe.Models.Entity
{
    public class Tag
    {
        public int TagId { get; set; }

        public string Name { get; set; }

        public ICollection<ArticleTag> ArticleTags { get; set; }
    }
}
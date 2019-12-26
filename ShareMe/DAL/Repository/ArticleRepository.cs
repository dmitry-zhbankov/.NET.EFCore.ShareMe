using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShareMe.DAL.Context;
using ShareMe.Models.Entity;

namespace ShareMe.DAL.Repository
{
    public class ArticleRepository:GenericRepository<Article>,IArticleRepository
    {
        public ArticleRepository(ShareMeContext context) : base(context)
        {
        }

        public override Article GetById(int? id)
        {
            var query = dbSet.Where(x=>x.ArticleId==id)
                .Include(x => x.Author)
                .Include(x => x.Category)
                .Include(x => x.ArticleTags)
                .ThenInclude(x => x.Tag);
            return query.FirstOrDefault();
        }

        public override IEnumerable<Article> Get(Expression<Func<Article, bool>> filter)
        {
            var query = dbSet.Where(filter)
                .Include(x => x.Author)
                .Include(x => x.Category)
                .Include(x => x.ArticleTags)
                .ThenInclude(x => x.Tag);
            return query.ToList();
        }
    }
}

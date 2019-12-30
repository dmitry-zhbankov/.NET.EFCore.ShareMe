using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShareMe.DAL.Context;
using ShareMe.DAL.Repository;

namespace ShareMe.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        ShareMeContext context;
        bool disposed;

        public DbContext Context => context;
        public IArticleRepository ArticleRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public IAuthorRepository AuthorRepository { get; }
        public ITagRepository TagRepository { get; }

        public UnitOfWork(ShareMeContext context, IArticleRepository articleRepository, ICategoryRepository categoryRepository, IAuthorRepository authorRepository, ITagRepository tagRepository)
        {
            this.context = context;
            ArticleRepository = articleRepository;
            CategoryRepository = categoryRepository;
            AuthorRepository = authorRepository;
            TagRepository = tagRepository;
        }

        public int Save()
        {
            return context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                context.Dispose();
            }

            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}

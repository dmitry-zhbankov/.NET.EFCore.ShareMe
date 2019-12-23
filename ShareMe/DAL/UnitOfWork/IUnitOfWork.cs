using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShareMe.DAL.Repository;

namespace ShareMe.DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        DbContext Context { get; }
        IArticleRepository ArticleRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IAuthorRepository AuthorRepository { get;  }
        ITagRepository TagRepository { get; }
        int Save();
    }
}

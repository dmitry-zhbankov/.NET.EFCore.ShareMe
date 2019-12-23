using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShareMe.DAL.Context;
using ShareMe.Models.Entity;

namespace ShareMe.DAL.Repository
{
    public class AuthorRepository:GenericRepository<Author>,IAuthorRepository
    {
        public AuthorRepository(ShareMeContext context) : base(context)
        {
        }
    }
}

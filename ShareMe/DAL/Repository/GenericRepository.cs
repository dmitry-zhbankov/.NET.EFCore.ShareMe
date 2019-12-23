using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShareMe.DAL.Context;

namespace ShareMe.DAL.Repository
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        protected ShareMeContext context;
        protected DbSet<T> dbSet;

        public GenericRepository(ShareMeContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }

        public virtual void Create(T entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Delete(int id)
        {
            var entity = dbSet.Find(id);
            dbSet.Remove(entity);
        }

        public virtual IEnumerable<T> Get(Expression<Func<T, bool>> filter)
        {
            var query = dbSet.Where(filter);
            return query.ToList();
        }

        public virtual T GetById(int id)
        {
            return dbSet.Find(id);
        }

        public virtual int Save()
        {
            return context.SaveChanges();
        }

        public virtual void Update(T entity)
        {
            dbSet.Update(entity);
        }
    }
}

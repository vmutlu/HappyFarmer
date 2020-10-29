using HappyFarmer.DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace HappyFarmer.DataAccess.Concrete.EntityFrameWork
{
    public class EfCoreGenericRepository<T, TContext> : IRepository<T> where T : class where TContext : DbContext, new()
    {
        public virtual void Create(T entity)
        {
            using (var context = new TContext())
            {
                context.Set<T>().Add(entity);
                context.SaveChanges();
            }
        }

        public void Delete(T entity)
        {
            using (var context = new TContext())
            {
                context.Set<T>().Remove(entity);
                context.SaveChanges();
            }
        }

        public virtual List<T> GetAll(Expression<Func<T, bool>> filter = null, bool? authority = false)
        {
            using (var context = new TContext())
            {
                return filter == null ? context.Set<T>().ToList() : context.Set<T>().Where(filter).ToList();
            }
        }

        public virtual T GetById(int id)
        {
            using (var context = new TContext())
            {
               return context.Set<T>().Find(id);
            }
        }

        public virtual void Update(T entity)
        {
            using (var context = new TContext())
            {
                context.Entry(entity).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace HappyFarmer.DataAccess.Abstract
{
    public interface IRepository<T>
    {
        T GetById(int id);
        List<T> GetAll(Expression<Func<T,bool>> filter = null, bool? authority = false);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}

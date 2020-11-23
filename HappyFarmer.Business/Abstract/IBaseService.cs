using System.Collections.Generic;

namespace HappyFarmer.Business.Abstract
{
    public interface IBaseService<T> where T : class
    {
        T GetById(int id);
        List<T> GetAll();
        void Create(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}

using HappyFarmer.Entities;
using System.Collections.Generic;

namespace HappyFarmer.Business.Abstract
{
    public interface IBlogService
    {
        FarmerBlog GetById(int id);
        List<FarmerBlog> GetAll(int? page = 0, int? pageSize = 0);
        void Create(FarmerBlog entity);
        void Delete(FarmerBlog entity);
        void Update(FarmerBlog entity);
        void CreateComment(FarmerComment entity);
    }
}

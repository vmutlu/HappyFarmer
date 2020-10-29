using HappyFarmer.Entities;
using System.Collections.Generic;

namespace HappyFarmer.DataAccess.Abstract
{
    public interface IMultipleProductImagesRepository : IRepository<FarmerMultipleProductImages>
    {
        void Create(int productId, List<string> multipleImages);
        List<FarmerMultipleProductImages> GetByIdMultiImages(int id);
    }
}

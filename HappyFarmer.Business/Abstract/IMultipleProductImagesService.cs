using HappyFarmer.Entities;
using System.Collections.Generic;

namespace HappyFarmer.Business.Abstract
{
    public interface IMultipleProductImagesService : IBaseService<FarmerMultipleProductImages>
    {
        void Create(int productId, List<string> multipleImages);
        List<FarmerMultipleProductImages> GetByIdMultiImages(int id);
    }
}

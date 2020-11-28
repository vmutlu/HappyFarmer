using HappyFarmer.DataAccess.Abstract;
using HappyFarmer.Entities;

namespace HappyFarmer.DataAccess.Concrete.EntityFrameWork
{
    public class EfCoreProductCommentRepository : EfCoreGenericRepository<ProductComment, FarmerContext>, IProductCommentRepository
    {
    }
}

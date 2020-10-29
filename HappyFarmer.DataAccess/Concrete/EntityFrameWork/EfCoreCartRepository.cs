using HappyFarmer.DataAccess.Abstract;
using HappyFarmer.Entities;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace HappyFarmer.DataAccess.Concrete.EntityFrameWork
{
    public class EfCoreCartRepository : EfCoreGenericRepository<FarmerCart, FarmerContext>, ICartRepository
    {
        public override void Update(FarmerCart entity)
        {
            using (var context = new FarmerContext())
            {
                context.Carts.Update(entity);
                context.SaveChanges();
            }
        }
        public void ClearCart(string cartId)
        {
            using (var context = new FarmerContext())
            {
                var cmd = "DELETE FROM farmercartitems where CartId=@p0";
                context.Database.ExecuteSqlCommand(cmd,cartId);
            }
        }

        public void DeleteFromCard(int cardId, int productId)
        {
            using (var context = new FarmerContext())
            {
                var sql = @"DELETE FROM farmercartitems where CartId=@p0 And ProductId=@p1";
                context.Database.ExecuteSqlCommand(sql,cardId,productId);
            }
        }

        public FarmerCart GetByUserId(string userId)
        {
            using (var context = new FarmerContext())
            {
                var cart = context.Carts
                    .Include(a => a.CartItems)
                    .ThenInclude(a => a.Product).AsNoTracking()
                    .FirstOrDefault(a => a.UserId == Convert.ToInt32(userId));
                return cart;
            }
        }
    }
}

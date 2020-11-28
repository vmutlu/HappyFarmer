using HappyFarmer.DataAccess.Abstract;
using HappyFarmer.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace HappyFarmer.DataAccess.Concrete.EntityFrameWork
{
    public class EfCoreMessageRepository : EfCoreGenericRepository<FarmerMessage, FarmerContext>, IMessageRepository
    {
        public List<FarmerMessage> GetByReceiverId(int id)
        {
            using (var context = new FarmerContext())
            {
                var response = context.Messages
                    .Where(i => i.UserReceiverId == id)
                    .Where(i=>i.MessageReceiverDelete == true)
                    .OrderByDescending(i=>i.MessageDate)
                    .AsNoTracking()
                    .ToList();

                return response;
            }
        }

        public List<FarmerMessage> GetBySenderId(int id)
        {
            using (var context = new FarmerContext())
            {
                var response = context.Messages
                    .Where(i => i.UserSenderId == id)
                    .Where(i => i.MessageSendDelete == true)
                    .OrderByDescending(i => i.MessageDate)
                    .AsNoTracking()
                    .ToList();

                return response;
            }
        }
    }
}

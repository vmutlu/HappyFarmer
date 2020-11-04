using HappyFarmer.DataAccess.Abstract;
using HappyFarmer.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyFarmer.DataAccess.Concrete.EntityFrameWork
{
    public class EfCoreGlobalMessageRepository : EfCoreGenericRepository<FarmerGlobalMessage, FarmerContext>, IGlobalMessageRepository
    {
        public async Task<List<FarmerGlobalMessage>> GetCarrierGlobalMessages(bool? authority = false)
        {
            using (var context = new FarmerContext())
            {
                if (authority == true)
                {
                    var response = context.GlobalMessages
                         .Where(i => i.FarmerOrCarrier == false)
                         .AsNoTracking()
                         .ToList();

                    return response;
                }

                else
                {
                    var response = context.GlobalMessages
                         .Where(i => i.CheckStatus == true)
                         .Where(i => i.FarmerOrCarrier == false)
                         .AsNoTracking()
                         .ToList();

                    return response;
                }
            }
        }

        public List<FarmerGlobalMessage> GetCityWithMessages(int cityId, string type)
        {
            using (var context = new FarmerContext())
            {
                var response = context.GlobalMessages
                    .Where(i => i.CityId == cityId)
                    .Where(i => i.CheckStatus == true)
                    .Where(i => i.Subject == type)
                    .AsNoTracking()
                    .ToList();

                return response;
            }
        }

        public FarmerGlobalMessage GetNameById(int id)
        {
            using (var context = new FarmerContext())
            {
                var response = (from i in context.GlobalMessages
                                from a in i.User.Name
                                where i.Id == id
                                select new FarmerGlobalMessage()
                                {
                                    Id = i.Id,
                                    User = new FarmerUser()
                                    {
                                        Id = i.Id,
                                        Name = a.ToString()
                                    }
                                }).AsNoTracking().FirstOrDefault<FarmerGlobalMessage>();

                return response;
            }
        }

        public List<FarmerGlobalMessage> GetTypeGlobalMessages(string type)
        {
            using (var context = new FarmerContext())
            {
                var response = context.GlobalMessages
                    .Where(i => i.Subject == type)
                    .Where(i => i.CheckStatus == true)
                    .Where(i => i.FarmerOrCarrier == true)
                    .AsNoTracking()
                    .ToList();

                return response;
            }
        }
    }
}

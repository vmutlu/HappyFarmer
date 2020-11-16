using HappyFarmer.Business.Abstract;
using HappyFarmer.DataAccess.Abstract;
using HappyFarmer.DataAccess.Concrete;
using HappyFarmer.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyFarmer.Business.Concrate
{
    public class GlobalMessageService : IGlobalMessageService
    {
        private readonly IGlobalMessageRepository _globalMessageRepository;
        public GlobalMessageService(IGlobalMessageRepository globalMessageRepository)
        {
            _globalMessageRepository = globalMessageRepository;
        }
        public void Create(FarmerGlobalMessage entity)
        {
            _globalMessageRepository.Create(entity);
        }

        public void Delete(FarmerGlobalMessage entity)
        {
            _globalMessageRepository.Delete(entity);
        }

        public List<FarmerGlobalMessage> GetAll(bool? authority = false)
        {
            if (authority == true)
            {
                    return (from a in _globalMessageRepository.GetAll()
                            where a.FarmerOrCarrier != false
                            select new FarmerGlobalMessage
                            {
                                Id = a.Id,
                                MessageContent = a.MessageContent,
                                SenderId = a.SenderId,
                                CheckStatus = a.CheckStatus,
                                Subject = a.Subject,
                                MessageDate = a.MessageDate,
                                FarmerOrCarrier = a.FarmerOrCarrier
                            }).ToList();
            }

            else
            {
                return (from i in _globalMessageRepository.GetAll()
                        where i.CheckStatus == true
                        where i.FarmerOrCarrier == true
                        select new FarmerGlobalMessage
                        {
                            Id = i.Id,
                            MessageContent = i.MessageContent,
                            SenderId = i.SenderId,
                            CheckStatus = i.CheckStatus,
                            Subject = i.Subject,
                            MessageDate = i.MessageDate
                        }).ToList();
            }
        }

        public FarmerGlobalMessage GetById(int id)
        {
            return _globalMessageRepository.GetById(id);
        }

        public async Task<List<FarmerGlobalMessage>> GetCarrierGlobalMessages(bool? authority = false)
        {
            return await _globalMessageRepository.GetCarrierGlobalMessages(authority);
        }

        public List<FarmerGlobalMessage> GetCityWithMessages(int cityId, string type)
        {
            return _globalMessageRepository.GetCityWithMessages(cityId, type);
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
                                }).FirstOrDefault<FarmerGlobalMessage>();

                return response;
            }
        }

        public List<FarmerGlobalMessage> GetTypeGlobalMessages(string type)
        {
            return _globalMessageRepository.GetTypeGlobalMessages(type);
        }

        public void Update(FarmerGlobalMessage entity)
        {
            _globalMessageRepository.Update(entity);
        }
    }
}

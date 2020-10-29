using HappyFarmer.Business.Abstract;
using HappyFarmer.DataAccess.Abstract;
using HappyFarmer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HappyFarmer.Business.Concrate
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        public MessageService(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }
        public void Create(FarmerMessage entity)
        {
            _messageRepository.Create(entity);
        }

        public void Delete(FarmerMessage entity)
        {
            _messageRepository.Delete(entity);
        }

        public List<FarmerMessage> GetAll()
        {
            return _messageRepository.GetAll();
        }

        public FarmerMessage GetById(int id)
        {
            return _messageRepository.GetById(id);
        }

        public List<FarmerMessage> GetByReceiverId(int id)
        {
            return _messageRepository.GetByReceiverId(id);
        }

        public List<FarmerMessage> GetBySenderId(int id)
        {
            return _messageRepository.GetBySenderId(id);
        }

        public void Update(FarmerMessage entity)
        {
            _messageRepository.Update(entity);
        }
    }
}

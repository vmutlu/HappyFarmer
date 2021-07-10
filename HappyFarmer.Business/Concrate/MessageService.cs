using HappyFarmer.Business.Abstract;
using HappyFarmer.DataAccess.Abstract;
using HappyFarmer.Entities;
using System.Collections.Generic;

namespace HappyFarmer.Business.Concrate
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        public MessageService(IMessageRepository messageRepository) =>
            _messageRepository = messageRepository;
        public void Create(FarmerMessage entity) =>
            _messageRepository.Create(entity);

        public void Delete(FarmerMessage entity) =>
            _messageRepository.Delete(entity);

        public List<FarmerMessage> GetAll() =>
             _messageRepository.GetAll();

        public FarmerMessage GetById(int id) =>
             _messageRepository.GetById(id);

        public List<FarmerMessage> GetByReceiverId(int id) =>
             _messageRepository.GetByReceiverId(id);

        public List<FarmerMessage> GetBySenderId(int id) =>
             _messageRepository.GetBySenderId(id);

        public void Update(FarmerMessage entity) =>
            _messageRepository.Update(entity);
    }
}

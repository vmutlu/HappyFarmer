﻿using HappyFarmer.Business.Abstract;
using HappyFarmer.DataAccess.Abstract;
using HappyFarmer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HappyFarmer.Business.Concrate
{
    public class AdminMessageService : IAdminMessageService
    {
        private readonly IAdminMessageRepository _adminMessageRepository;
        public AdminMessageService(IAdminMessageRepository adminMessageRepository)
        {
            _adminMessageRepository = adminMessageRepository;
        }
        public void Create(FarmerAdminMessage entity)
        {
            _adminMessageRepository.Create(entity);
        }

        public void Delete(FarmerAdminMessage entity)
        {
            _adminMessageRepository.Delete(entity);
        }

        public List<FarmerAdminMessage> GetAll()
        {
            return _adminMessageRepository.GetAll();
        }

        public FarmerAdminMessage GetById(int id)
        {
            return _adminMessageRepository.GetById(id);
        }

        public void Update(FarmerAdminMessage entity)
        {
            _adminMessageRepository.Update(entity);
        }
    }
}

using HappyFarmer.Business.Abstract;
using HappyFarmer.DataAccess.Abstract;
using HappyFarmer.DataAccess.Concrete;
using HappyFarmer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HappyFarmer.Business.Concrate
{
    public class MultipleProductImagesService : IMultipleProductImagesService
    {
        private readonly IMultipleProductImagesRepository _multipleProductImagesRepository;
        public MultipleProductImagesService(IMultipleProductImagesRepository multipleProductImagesRepository)
        {
            _multipleProductImagesRepository = multipleProductImagesRepository;
        }
        public void Create(FarmerMultipleProductImages entity)
        {
            _multipleProductImagesRepository.Create(entity);
        }

        public void Create(int productId, List<string> multipleImages)
        {
            _multipleProductImagesRepository.Create(productId, multipleImages);
        }

        public void Delete(FarmerMultipleProductImages entity)
        {
            using (var context = new FarmerContext())
            {
                var sqlRequest = "Delete from multipleproductimages where Id=@p0";
                context.Database.ExecuteSqlCommand(sqlRequest, entity.Id);
            }
        }

        public List<FarmerMultipleProductImages> GetAll()
        {
            return _multipleProductImagesRepository.GetAll();
        }

        public FarmerMultipleProductImages GetById(int id)
        {
            return _multipleProductImagesRepository.GetById(id);
        }

        public List<FarmerMultipleProductImages> GetByIdMultiImages(int id)
        {
            return _multipleProductImagesRepository.GetByIdMultiImages(id);
        }

        public void Update(FarmerMultipleProductImages entity)
        {
            _multipleProductImagesRepository.Update(entity);
        }
    }
}

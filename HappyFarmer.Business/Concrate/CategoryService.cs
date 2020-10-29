using HappyFarmer.Business.Abstract;
using HappyFarmer.DataAccess.Abstract;
using HappyFarmer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HappyFarmer.Business.Concrate
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public void Create(FarmerCategory entity)
        {
            _categoryRepository.Create(entity);
        }

        public void Delete(FarmerCategory entity)
        {
            _categoryRepository.Delete(entity);
        }

        public void DeleteFromCategory(int categoryId, int productId)
        {
            _categoryRepository.DeleteFromCategory(categoryId, productId);
        }

        public List<FarmerCategory> GetAll()
        {
            return _categoryRepository.GetAll();
        }

        public FarmerCategory GetById(int id)
        {
            return _categoryRepository.GetById(id);
        }

        public FarmerCategory GetByIdWithProducts(int id)
        {
            return _categoryRepository.GetByIdWithProduct(id);
        }

        public void Update(FarmerCategory entity)
        {
            _categoryRepository.Update(entity);
        }
    }
}

using HappyFarmer.Business.Abstract;
using HappyFarmer.DataAccess.Abstract;
using HappyFarmer.Entities;
using System.Collections.Generic;

namespace HappyFarmer.Business.Concrate
{
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _menuRepository;
        public MenuService(IMenuRepository menuRepository) =>
            _menuRepository = menuRepository;
        public void Create(FarmerMenu entity) =>
            _menuRepository.Create(entity);

        public void Delete(FarmerMenu entity) =>
            _menuRepository.Delete(entity);

        public List<FarmerMenu> GetAll() =>
             _menuRepository.GetAll();

        public FarmerMenu GetById(int id) =>
             _menuRepository.GetById(id);

        public void Update(FarmerMenu entity) =>
            _menuRepository.Update(entity);
    }
}

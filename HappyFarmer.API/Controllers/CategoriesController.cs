using AutoMapper;
using HappyFarmer.API.DTOs;
using HappyFarmer.Business.Abstract;
using HappyFarmer.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HappyFarmer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        /// <summary>
        /// Tüm Categorileri listeleyen endpointim
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = _categoryService.GetAll();
            return Ok(categories);
        }

        //İd'ye göre kategorileri getiren endpointim
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = _categoryService.GetById(id);
            return Ok(_mapper.Map<FarmerCategoryDTO>(category));
        }

        /// <summary>
        /// Yeni Kategori Ekleme endpointim
        /// </summary>
        /// <param name="categoryDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(FarmerCategoryDTO categoryDTO)
        {
            _categoryService.Create(_mapper.Map<FarmerCategory>(categoryDTO));
            return Ok();
        }

        /// <summary>
        /// İd'ye göre kategori getiren endpointim
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Products/{id}")]
        public async Task<IActionResult> GetByIdWithProducts(int id)
        {
            var category = _categoryService.GetByIdWithProducts(id);
            return Ok(category);
        }

        /// <summary>
        /// Kategorileri güncelleyen endpointim
        /// </summary>
        /// <param name="categoryDTO"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update(FarmerCategoryDTO categoryDTO)
        {
            _categoryService.Update(_mapper.Map<FarmerCategory>(categoryDTO));
            return Ok();
        }

        /// <summary>
        /// Kategori silme işlemi yapan endpointim
        /// </summary>
        /// <param name="categoryDTO"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(FarmerCategoryDTO categoryDTO)
        {
            _categoryService.Delete(_mapper.Map<FarmerCategory>(categoryDTO));
            return Ok();
        }

        /// <summary>
        /// belli bir id ye sahip kategoriden ürün silme endpointim
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpDelete("Product/Category")]
        public async Task<IActionResult> Delete(int categoryId,int productId)
        {
            _categoryService.DeleteFromCategory(categoryId,productId);
            return Ok();
        }
    }
}

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
        public async Task<IActionResult> GetAll() => Ok(_categoryService.GetAll());

        //İd'ye göre kategorileri getiren endpointim
        [HttpGet("{id}")]
        public IActionResult GetById(int id) => Ok(_mapper.Map<FarmerCategoryDTO>(_categoryService.GetById(id)));

        /// <summary>
        /// Yeni Kategori Ekleme endpointim
        /// </summary>
        /// <param name="categoryDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Add(FarmerCategoryDTO categoryDTO)
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
        public IActionResult GetByIdWithProducts(int id) => Ok(_categoryService.GetByIdWithProducts(id));

        /// <summary>
        /// Kategorileri güncelleyen endpointim
        /// </summary>
        /// <param name="categoryDTO"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Update(FarmerCategoryDTO categoryDTO)
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
        public IActionResult Delete(FarmerCategoryDTO categoryDTO)
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
        public IActionResult Delete(int categoryId, int productId)
        {
            _categoryService.DeleteFromCategory(categoryId, productId);
            return Ok();
        }
    }
}

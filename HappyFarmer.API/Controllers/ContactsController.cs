using AutoMapper;
using HappyFarmer.API.DTOs;
using HappyFarmer.Business.Abstract;
using HappyFarmer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HappyFarmer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IAdminMessageService _adminMessageService;
        private readonly IMapper _mapper;
        public ContactsController(IAdminMessageService adminMessageService, IMapper mapper)
        {
            _adminMessageService = adminMessageService;
            _mapper = mapper;
        }

        /// <summary>
        /// Mail gönderme endpointi
        /// </summary>
        /// <param name="farmerAdminMessageDTO"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult SendAdminMessage(FarmerAdminMessageDTO farmerAdminMessageDTO)
        {
            _adminMessageService.Create(_mapper.Map<FarmerAdminMessage>(farmerAdminMessageDTO));

            return Ok();
        }
    }
}

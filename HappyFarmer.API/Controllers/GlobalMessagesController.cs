using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HappyFarmer.Business.Abstract;
using HappyFarmer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HappyFarmer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GlobalMessagesController : ControllerBase
    {
        private readonly IGlobalMessageService _globalMessageService;
        public GlobalMessagesController(IGlobalMessageService globalMessageService)
        {
            _globalMessageService = globalMessageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = _globalMessageService.GetAll();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = _globalMessageService.GetById(id);
            return Ok(response);
        }

        [HttpPost("User/{id}")]
        public async Task<IActionResult> GetNameById(int id)
        {
            var response = _globalMessageService.GetNameById(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Add(FarmerGlobalMessage globalMessage)
        {
            _globalMessageService.Create(globalMessage);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(FarmerGlobalMessage globalMessage)
        {
            _globalMessageService.Update(globalMessage);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(FarmerGlobalMessage globalMessage)
        {
            _globalMessageService.Delete(globalMessage);
            return Ok();
        }
    }
}

using HappyFarmer.Business.Abstract;
using HappyFarmer.Entities;
using HappyFarmer.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace HappyFarmer.UI.Controllers
{
    public class ContactController : Controller
    {
        private readonly IAdminMessageService _adminMessageService;
        private readonly IGeneralSettingsService _generalSettingService;
        public ContactController(IAdminMessageService adminMessageService, IGeneralSettingsService generalSettingService)
        {
            _adminMessageService = adminMessageService;
            _generalSettingService = generalSettingService;
        }
        public IActionResult Index()=> View(_generalSettingService.GetAll());

        [HttpPost]
        public IActionResult AdminMessageSend(AdminMessageModel adminMessageModel)
        {
            if(ModelState.IsValid)
            {
                var entity = new FarmerAdminMessage()
                {
                    Name = adminMessageModel.Name,
                    Message = adminMessageModel.Message,
                    Email = adminMessageModel.Email,
                    Subject = adminMessageModel.Subject,
                    Surname = adminMessageModel.Surname
                };

                _adminMessageService.Create(entity);
            }
            return Redirect("/Contact/Index");
        }
    }
}

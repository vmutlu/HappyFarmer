using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HappyFarmer.Business.Abstract;
using HappyFarmer.Entities;
using HappyFarmer.UI.Logging;
using HappyFarmer.UI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HappyFarmer.UI.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IProductService _productService;
        private readonly IMessageService _messageService;
        private readonly IGlobalMessageService _globalMessageService;
        private readonly IMultipleProductImagesService _multipleProductImagesService;
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;
        public UserController(IUserService userService, IProductService productService, IMessageService messageService, IMultipleProductImagesService multipleProductImagesService, IGlobalMessageService globalMessageService, ICartService cartService, IOrderService orderService)
        {
            _userService = userService;
            _productService = productService;
            _messageService = messageService;
            _multipleProductImagesService = multipleProductImagesService;
            _globalMessageService = globalMessageService;
            _cartService = cartService;
            _orderService = orderService;
        }
        public IActionResult Index()
        {
            return View();
        }
        #region User Messages Methods

        [HttpGet]
        public IActionResult UserNewMessage(string id)
        {
            if (id != null)
                ViewBag.Answer = id;

            ViewBag.NowActiveUser = HttpContext.Session.GetString("ActiveCustomerId");
            ViewBag.GetAllUser = _userService.GetAllCustomer();
            return View();
        }

        [HttpPost]
        public IActionResult UserNewMessage(UsersMessageModel usersMessageModel)
        {
            var activeSendUserId = HttpContext.Session.GetString("ActiveCustomerId");
            if (usersMessageModel != null)
            {
                var entity = new FarmerMessage()
                {
                    UserSenderId = Convert.ToInt32(activeSendUserId),
                    UserReceiverId = usersMessageModel.UserReceiverId,
                    Subject = usersMessageModel.Subject,
                    MessageContent = usersMessageModel.MessageContent,
                    MessageDate = usersMessageModel.MessageDate,
                    MessageReceiverDelete = true,
                    MessageSendDelete = true
                };

                _messageService.Create(entity);

                return Redirect("/User/UserAccount");
            }
            return View();
        }

        public IActionResult UserDeleteMessage(int Id)
        {
            var entity = _messageService.GetById(Id);
            if (entity == null)
            {
                return NotFound();
            }

            entity.MessageReceiverDelete = false;

            _messageService.Update(entity);
            return Redirect("/User/UserAccount");
        }

        public IActionResult UserSendDeleteMessage(int Id)
        {
            var entity = _messageService.GetById(Id);
            if (entity == null)
            {
                return NotFound();
            }

            entity.MessageSendDelete = false;

            _messageService.Update(entity);
            return Redirect("/User/UserAccount");
        }

        #endregion

        #region User Account Setting & User Profile Information

        [HttpGet]
        public IActionResult UserAccount()
        {
            ViewBag.ActiveCustomer = HttpContext.Session.GetString("ActiveCustomer"); //aktif giriş yapan müşteri
            var activeUserId = HttpContext.Session.GetString("ActiveCustomerId"); //idsi

            var activeCustomer = _userService.GetById(Convert.ToInt32(activeUserId));
            var userDeclares = _productService.GetByIdUser(Convert.ToInt32(activeUserId));

            ViewBag.UserDeclare = userDeclares; // giriş yapan kullanıcının verdiği ilanlar
            ViewBag.GetByIdMessage = _userService.GetAllCustomer(); //tüm müşterileri attım

            ViewBag.SendGetMessage = _messageService.GetByReceiverId(Convert.ToInt32(activeUserId)); //aktif olan kullanıcıya gelen mesajları getir


            List<LoginModel> log = new List<LoginModel>
            {
                new LoginModel()
                {
                 Name = activeCustomer.Name,
                 Surname = activeCustomer.Surname,
                 Address = activeCustomer.Address,
                 Authority = activeCustomer.Authority,
                 Email = activeCustomer.Email,
                 Id = activeCustomer.Id,
                 City = activeCustomer.City,
                 ImageURL = activeCustomer.ImageURL,
                 Password = activeCustomer.Password,
                 RecordData = activeCustomer.RecordData,
                 PhoneNumber = activeCustomer.PhoneNumber,
                 UserType = activeCustomer.UserType,
                 TcNo = activeCustomer.TcNo
                }
            };
            return View(log);
        }

        [HttpPost]
        public async Task<IActionResult> UserUpdateProfil(IFormFile file)
        {
            var activeUserId = HttpContext.Session.GetString("ActiveCustomerId");
            var activeCustomer = _userService.GetById(Convert.ToInt32(activeUserId));

            if (file != null)
            {
                if (activeCustomer.ImageURL != null)
                {
                    //güncelleme yaparken önce resmi sil
                    var deletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\CustomerImages", activeCustomer.ImageURL);
                    FileInfo fileInfo = new FileInfo(deletePath);
                    if (fileInfo != null)
                    {
                        System.IO.File.Delete(deletePath);
                        fileInfo.Delete();
                    }
                }

                //daha sonra resim ekle
                Random rastgele = new Random();
                string harfler = "ABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZabcçdefgğhıijklmnoöprsştuüvyz";
                string uret = "";
                for (int i = 0; i < 6; i++)
                {
                    uret += harfler[rastgele.Next(harfler.Length)];
                }

                activeCustomer.ImageURL = uret + ".jpg";
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\CustomerImages", uret + ".jpg");

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                _userService.Update(activeCustomer);
            }
            return Redirect("/User/UserAccount");
        }

        [HttpPost]
        public IActionResult UserAccountUpdate(LoginModel model)
        {
            var entity = _userService.GetById(model.Id);
            if (entity == null)
            {
                return NotFound();
            }

            entity.Name = model.Name;
            entity.Surname = model.Surname;
            entity.TcNo = model.TcNo;
            entity.PhoneNumber = model.PhoneNumber;
            entity.UserName = model.UserName;
            entity.City = model.City;
            entity.Address = model.Address;

            _userService.Update(entity);
            return Redirect("/User/UserAccount");
        }

        [HttpPost]
        public IActionResult UserChangedPassword(LoginModel model)
        {
            var entity = _userService.GetById(model.Id);
            if (entity == null)
            {
                return NotFound();
            }

            if (model.Password.Length >= 6 && model.Password.ToLower() == model.RePassword.ToLower())
            {
                entity.Password = model.Password;
                entity.RePassword = model.RePassword;

                _userService.Update(entity);

                return Redirect("/User/UserAccount");
            }

            return View();
        }

        [HttpGet]
        public IActionResult UserOldMessage()
        {
            var activeUserId = HttpContext.Session.GetString("ActiveCustomerId");
            var response = _messageService.GetBySenderId(Convert.ToInt32(activeUserId));
            ViewBag.GetByIdMessage = _userService.GetAllCustomer();
            return View(response);
        }

        [HttpGet]
        public IActionResult UserSoldProducts()
        {
            var activeUserId = HttpContext.Session.GetString("ActiveCustomerId");
            
            List<OrderModel> orderListModels = new List<OrderModel>();

            if(activeUserId != null)
            {
                var userSoldProducts = _userService.GetUserSoldProduct(Convert.ToInt32(activeUserId));
                foreach (var orderDetails in userSoldProducts)
                {
                    var Id = orderDetails.Id.ToString();
                    var list = _orderService.GetById(Id);
                    var products = _productService.GetById(orderDetails.ProductId);

                    orderListModels = new List<OrderModel>()
                    {
                        new OrderModel()
                        {
                            Quantity = orderDetails.Quantity,
                            ImageURL = products.ImageUrl,
                            ProductName = products.Name,
                            City = list.City,
                            FirstName = list.FirstName,
                            LastName = list.LastName,
                            OrderDate = list.OrderDate,
                            Phone = list.Phone,
                            Email = list.Email,
                        }
                    };
                }
                return View(orderListModels);
            }
            return View();
        }

        #endregion

        #region Kullanıcı Kaydı 

        [HttpGet]
        public IActionResult UserRegister()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UserRegister(LoginModel loginModel, IFormFile file)
        {
            if (loginModel != null)
            {
                var newUser = new FarmerUser()
                {
                    Name = loginModel.Name,
                    Surname = loginModel.Surname,
                    Email = loginModel.Email,
                    Address = loginModel.Address,
                    Password = loginModel.Password,
                    PhoneNumber = loginModel.PhoneNumber,
                    RecordData = DateTime.Now.ToShortDateString().ToString(),
                    UserType = loginModel.UserType,
                    UserState = true //kullanıcı kayıt oldugunda durumu true olsun
                };

                if (file != null)
                {
                    Random rastgele = new Random();
                    string harfler = "ABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZabcçdefgğhıijklmnoöprsştuüvyz";
                    string uret = "";
                    for (int i = 0; i < 6; i++)
                    {
                        uret += harfler[rastgele.Next(harfler.Length)];
                    }

                    newUser.ImageURL = uret + ".jpg";
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\CustomerImages", uret + ".jpg");

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream).ConfigureAwait(false);
                    }
                }
                _userService.Create(newUser);

                return View("UserLogin");
            }

            return View();
        }

        #endregion

        #region Kullanıcı Giriş

        [HttpGet]
        public IActionResult UserLogin()
        {
            return View();
        }

        #region ReCapctcha

        private async Task<bool> CheckCaptcha()
        {
            var postData = new List<KeyValuePair<string, string>>()
                 {
                  new KeyValuePair<string, string>("secret", "6LdauNsZAAAAANMIdH2x27vHca7X87PjhlHgGGUk"),
                  new KeyValuePair<string, string>("remoteip", HttpContext.Connection.RemoteIpAddress.ToString()),
                  new KeyValuePair<string, string>("response", HttpContext.Request.Form["g-recaptcha-response"])
                 };

            var client = new HttpClient();
            var response = await client.PostAsync("https://www.google.com/recaptcha/api/siteverify", new FormUrlEncodedContent(postData));

            var o = (JObject)JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());

            return (bool)o["success"];
        }

        #endregion

        [HttpPost]
        public async Task<IActionResult> UserLogin(LoginModel loginModel, int userType)
        {

            #region Log Records

            var message = new { FirstName = "Veysel", LastName = "MUTLU", Message = "Giriş Log Mesajıdır." };
            LoggerFactory.FileLogManager().Information("{test}" + message);

            #endregion

            var captchaImage = HttpContext.Request.Form["g-recaptcha-response"];
            if (string.IsNullOrEmpty(captchaImage))
            {
                ViewBag.Captcha = "Captcha doğrulanmamış";
                return View();
            }

            var verified = await CheckCaptcha();
            if (!verified)
            {
                ViewBag.Captcha = "Captcha yanlış doğrulanmış";
                return View();
            }

            if (ModelState.IsValid)
            {
                var user = _userService.FindByEmail(loginModel.Email);
                var activeUserId = 0;
                var activeUserType = 0;
                foreach (var item in user)
                {
                    activeUserId = item.Id;
                    activeUserType = item.UserType;
                }

                if (user == null)
                {
                    ModelState.AddModelError("", "Bu Email adresi ile eşleşen bir Email Adresi Bulunamadı");
                }

                #region Kullanıcı siteye girdiğinde mesajı varsa sayısı gösterilsin

                var aktifMesajSayi = _messageService.GetByReceiverId(Convert.ToInt32(activeUserId));  //kullanıcı siteye girdiğinde gelen mesajlar _layoutta görülsün.
                HttpContext.Session.SetInt32("GirisMessages", aktifMesajSayi.Count());

                #endregion

                var userLogin = _userService.PasswordSignIn(loginModel.Email, loginModel.Password, userType);

                if (userLogin)
                {
                    HttpContext.Session.SetString("ActiveCustomer", loginModel.Email);
                    HttpContext.Session.SetString("ActiveCustomerId", activeUserId.ToString());
                    HttpContext.Session.SetString("ActiveUserType", activeUserType.ToString());

                    _cartService.InitializeCart(HttpContext.Session.GetString("ActiveCustomerId"));
                    return RedirectToAction("Index", "Home");
                }

                else
                {
                    ModelState.AddModelError("", "Email veya Şifre Hatalı !");
                    ViewBag.Message = "Email veya Şifre Hatalı !";
                    return View();
                }
            }

            return View();
        }

        #endregion

        #region Kullanıcı Çıkış

        public IActionResult UserLogout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("ActiveCustomer");
            HttpContext.Session.Remove("ActiveCustomerId");

            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }

            return Redirect("/User/UserLogin");
        }

        #endregion

        #region Kullanıcı İlan Endpointleri

        [HttpGet]
        public IActionResult UserCreateDeclare()
        {
            var activeUserId = HttpContext.Session.GetString("ActiveCustomerId");
            var activeCustomer = _userService.GetById(Convert.ToInt32(activeUserId));

            List<DeclareModel> customerInformation = new List<DeclareModel>()
            {
                new DeclareModel()
                {
                Id = activeCustomer.Id,
                UserId = activeCustomer.Id,
                Name = activeCustomer.Name,
                Surname = activeCustomer.Surname,
                EMail = activeCustomer.Email,
                PhoneNumber = activeCustomer.PhoneNumber
                }
            };

            return View(customerInformation);
        }

        [HttpPost]
        public async Task<IActionResult> UserCreateDeclare(ProductModel model, IFormFile file)
        {
            var declare = new FarmerProduct()
            {
                UserName = model.UserName,
                UserSurname = model.UserSurname,
                UserEMail = model.UserEMail,
                UserPhoneNumber = model.UserPhoneNumber,
                Name = model.Name,
                Gender = model.Gender,
                Neighborhood = model.Neighborhood,
                Price = model.Price,
                PermissionToSell = false,
                Guarantee = model.Guarantee,
                Description = model.Description,
                Country = model.Country,
                City = model.City,
                Situation = model.Situation,
                AnimalAge = model.AnimalAge,
                AnnouncementDate = model.AnnouncementDate,
                FarmerDeclareType = model.FarmerDeclareType,
                StockQty = model.StockQty,
                Swap = model.Swap,
                Title = model.Title,
                UserId = model.UserId
            };

            if (file != null)
            {
                Random rastgele = new Random();
                string harfler = "ABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZabcçdefgğhıijklmnoöprsştuüvyz";
                string uret = "";
                for (int i = 0; i < 6; i++)
                {
                    uret += harfler[rastgele.Next(harfler.Length)];
                }

                declare.ImageUrl = uret + ".jpg";
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\ProductImages", uret + ".jpg");

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream).ConfigureAwait(false);
                }

                _productService.Create(declare);
                return Redirect("/User/UserAccount");
            }
            return View();
        }

        [HttpGet]
        public IActionResult UserEditDeclare(int Id)
        {
            var entity = _productService.GetById(Id);

            if (entity == null)
            {
                return NotFound();
            }

            List<ProductModel> model = new List<ProductModel>()
            {
                new ProductModel()
                {
                Name = entity.Name,
                AnimalAge= entity.AnimalAge,
                AnnouncementDate = entity.AnnouncementDate,
                City = entity.City,
                Country = entity.Country,
                Description = entity.Description,
                FarmerDeclareType = entity.FarmerDeclareType,
                Gender = entity.Gender,
                Guarantee = entity.Guarantee,
                ImageURL = entity.ImageUrl,
                Neighborhood = entity.Neighborhood,
                PermissionToSell= entity.PermissionToSell,
               Price = entity.Price,
               Situation = entity.Situation,
               StockQty = entity.StockQty,
               Swap = entity.Swap,
               Title = entity.Title,
               UserEMail = entity.UserEMail,
               UserId = entity.UserId,
               UserName = entity.UserName,
               UserSurname = entity.UserSurname,
               UserPhoneNumber = entity.UserPhoneNumber,
               Id = entity.Id
                }
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UserEditDeclare(ProductModel productModel, IFormFile file)
        {
            var entity = _productService.GetById(productModel.Id);
            if (entity == null)
            {
                return NotFound();
            }

            entity.Name = productModel.Name;
            entity.AnimalAge = productModel.AnimalAge;
            entity.AnnouncementDate = productModel.AnnouncementDate;
            entity.City = productModel.City;
            entity.Country = productModel.Country;
            entity.Description = productModel.Description;
            entity.FarmerDeclareType = productModel.FarmerDeclareType;
            entity.Gender = productModel.Gender;
            entity.Guarantee = productModel.Guarantee;
            entity.Neighborhood = productModel.Neighborhood;
            entity.PermissionToSell = productModel.PermissionToSell;
            entity.Price = productModel.Price;
            entity.Situation = productModel.Situation;
            entity.StockQty = productModel.StockQty;
            entity.Swap = productModel.Swap;
            entity.Title = productModel.Title;
            entity.UserEMail = productModel.UserEMail;
            entity.UserId = productModel.UserId;
            entity.UserName = productModel.UserName;
            entity.UserSurname = productModel.UserSurname;
            entity.UserPhoneNumber = productModel.UserPhoneNumber;

            if (file != null)
            {
                if (entity.ImageUrl != null)
                {
                    //güncelleme yaparken önce resmi sil
                    var deletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\ProductImages", entity.ImageUrl);
                    FileInfo fileInfo = new FileInfo(deletePath);
                    if (fileInfo != null)
                    {
                        System.IO.File.Delete(deletePath);
                        fileInfo.Delete();
                    }
                }

                //daha sonra resim ekle
                Random rastgele = new Random();
                string harfler = "ABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZabcçdefgğhıijklmnoöprsştuüvyz";
                string uret = "";
                for (int i = 0; i < 6; i++)
                {
                    uret += harfler[rastgele.Next(harfler.Length)];
                }

                entity.ImageUrl = uret + ".jpg";
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\ProductImages", uret + ".jpg");

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            _productService.Update(entity);
            return Redirect("/User/UserAccount");
        }

        public IActionResult DeleteDeclare(int Id)
        {
            var entity = _productService.GetById(Id);
            if (entity == null)
            {
                return NotFound();
            }

            if (entity.ImageUrl != null)
            {
                //Slideri vt sil ve klasörde yer alan resmini de sil
                var sliderOldImages = entity.ImageUrl; //resim yolunu al
                var deletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\ProductImages", sliderOldImages);
                FileInfo fileInfo = new FileInfo(deletePath);
                if (fileInfo != null)
                {
                    System.IO.File.Delete(deletePath);
                    fileInfo.Delete();
                }
            }

            _productService.Delete(entity);
            return Redirect("/User/UserAccount");
        }

        #endregion

        #region Kullanıcı ilan çoklu foto

        List<string> list = new List<string>();
        [HttpPost]
        public async Task<IActionResult> UserAddMultipleProducts(int id, IFormFile[] file)
        {
            foreach (var item in file)
            {
                Random rastgele = new Random();
                string harfler = "ABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZabcçdefgğhıijklmnoöprsştuüvyz";
                string uret = "";
                for (int i = 0; i < 6; i++)
                {
                    uret += harfler[rastgele.Next(harfler.Length)];
                }

                list.Add(uret + ".jpg");
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\ProductMultipleImages", uret + ".jpg");

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await item.CopyToAsync(stream).ConfigureAwait(false);
                }
            }

            _multipleProductImagesService.Create(id, list);
            return Redirect("/User/UserAccount");
        }

        [HttpGet]
        public IActionResult UserGetMultipleProductImages(int id)
        {
            var ımages = _multipleProductImagesService.GetByIdMultiImages(id);
            return View(ımages);
        }

        [HttpGet]
        public async Task<IActionResult> UserDeleteMultiImages(int id)
        {
            var images = _multipleProductImagesService.GetById(id);
            if (images == null)
            {
                return NotFound();
            }

            var imagesURL = images.ImageURL;

            var deletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\ProductMultipleImages", imagesURL);
            FileInfo fileInfo = new FileInfo(deletePath);
            if (fileInfo != null)
            {
                System.IO.File.Delete(deletePath);
                fileInfo.Delete();
            }

            _multipleProductImagesService.Delete(images);

            return Redirect("/User/UserGetMultipleProductImages/" + images.ProductId);
        }

        #endregion

        #region Users Chat Methods

        [HttpGet]
        public async Task<IActionResult> UserChat(string type, int? CityId)
        {
            if (type == null && CityId <= 0)
                return NotFound();


            ViewBag.Type = type;
            ViewBag.UsersNameSurname = _userService.GetAll();

            if (type == "Nakliyeci")
            {
                var entity = await _globalMessageService.GetCarrierGlobalMessages(false).ConfigureAwait(false);
                if (CityId > 0)
                {
                    entity = _globalMessageService.GetCityWithMessages((int)CityId, type);
                }
                return View(entity);
            }

            else
            {
                var entity = _globalMessageService.GetTypeGlobalMessages(type);

                if (CityId > 0)
                {
                    entity = _globalMessageService.GetCityWithMessages((int)CityId, type);
                }
                return View(entity);
            }
        }

        [HttpPost]
        public IActionResult UserChat(int messageSubject, string messageContent, int City,string type)
        {
            if (messageContent != null && messageSubject != 0)
            {
                messageContent = RemoveHtml(messageContent);

                var activeUserId = HttpContext.Session.GetString("ActiveCustomerId");
                var message = new FarmerGlobalMessage()
                {
                    MessageContent = messageContent,
                    Subject = messageSubject.ToString(),
                    CheckStatus = false,
                    MessageDate = DateTime.Now.ToShortDateString(),
                    SenderId = Convert.ToInt32(activeUserId),
                    CityId = City                    
                };
                if (type == "Nakliyeci")
                    message.FarmerOrCarrier = false;
                else
                    message.FarmerOrCarrier = true;

                _globalMessageService.Create(message);
                ViewBag.SuccessMessage = "Mesajınız Başarılıyla Gönderildi.";
            }
            var entity = _globalMessageService.GetAll();
            ViewBag.UsersNameSurname = _userService.GetAll();
            return View(entity);
        }

        public static string RemoveHtml(string text)
        {
            return Regex.Replace(text, @"<(.|\n)*?>", string.Empty);
        }

        #endregion
    }
}

using HappyFarmer.Business.Abstract;
using HappyFarmer.Entities;
using HappyFarmer.Entities.Enums;
using HappyFarmer.UI.Models;
using IyzipayCore;
using IyzipayCore.Model;
using IyzipayCore.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HappyFarmer.UI.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;
        public CartController(ICartService cartService, IOrderService orderService)
        {
            _cartService = cartService;
            _orderService = orderService;
        }

        #region Cart Methods

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("ActiveCustomerId") == null)
            {
                return RedirectToAction("UserLogin", "User");
            }

            var cart = _cartService.GetCartByUserId(HttpContext.Session.GetString("ActiveCustomerId"));

            var response = new CartModel()
            {
                CartId = cart.Id,
                CartItems = cart.CartItems.Select(i => new CartItemModel()
                {
                    CartItemId = i.Id,
                    ProductId = i.Product.Id,
                    Name = i.Product.Name,
                    Price = (decimal)i.Product.Price,
                    ImageUrl = i.Product.ImageUrl,
                    Quantity = i.Quantity
                }).ToList()
            };

            return View(response);
        }

        [HttpPost]
        public IActionResult AddToCart(int productId, int quantity)
        {
            if (HttpContext.Session.GetString("ActiveCustomerId") == null)
            {
                return RedirectToAction("UserLogin", "User");
            }
            _cartService.AddToCart(HttpContext.Session.GetString("ActiveCustomerId"), productId, quantity);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult DeleteFromCart(int productId)
        {
            if (HttpContext.Session.GetString("ActiveCustomerId") == null)
            {
                return RedirectToAction("UserLogin", "User");
            }
            _cartService.DeleteFromCart(HttpContext.Session.GetString("ActiveCustomerId"), productId);
            return RedirectToAction("Index");
        }

        #endregion

        #region Order Methods

        [HttpGet]
        public IActionResult CheckOut()
        {
            if (HttpContext.Session.GetString("ActiveCustomerId") == null)
            {
                return RedirectToAction("UserLogin", "User");
            }

            var cart = _cartService.GetCartByUserId(HttpContext.Session.GetString("ActiveCustomerId"));
            var orderModel = new OrderModel();
            orderModel.CartModel = new CartModel()
            {
                CartId = cart.Id,
                CartItems = cart.CartItems.Select(i => new CartItemModel()
                {
                    CartItemId = i.Id,
                    ProductId = i.Product.Id,
                    Name = i.Product.Name,
                    Price = (decimal)i.Product.Price,
                    ImageUrl = i.Product.ImageUrl,
                    Quantity = i.Quantity
                }).ToList()
            }; 
            return View(orderModel); 
        }

        [HttpPost]
        public IActionResult CheckOut(OrderModel orderModel)
        {
            if(orderModel == null)
            {
                ViewBag.Message = "Lütfen Zorunlu Alanları Doldurmadan Geçmeyiniz";
                return View();
            }
                var userId = HttpContext.Session.GetString("ActiveCustomerId");
                var cart = _cartService.GetCartByUserId(userId);

                orderModel.CartModel = new CartModel()
                {
                    CartId = cart.Id,
                    CartItems = cart.CartItems.Select(i => new CartItemModel()
                    {
                        CartItemId = i.Id,
                        ProductId = i.Product.Id,
                        Name = i.Product.Name,
                        Price = (decimal)i.Product.Price,
                        ImageUrl = i.Product.ImageUrl,
                        Quantity = i.Quantity
                    }).ToList()
                };

                //odeme işlemim
                var payment = PaymentProcess(orderModel);

                if (payment.Status == "success")
                {
                    SaveOrder(orderModel, payment, userId);
                    ClearCart(cart.Id.ToString());
                    TempData["Success"] = "Ödeme İşlemi Başarı bir şekilde tamamlandı.Siparişleriniz Siparişlerim kısmından kontrol edebilirsiniz...";
                    return RedirectToAction("Index","Home");
                }

            //sipariş
            return View(orderModel);
        }

        private void SaveOrder(OrderModel model, Payment payment, string userId)
        {
            var order = new FarmerOrder();
            order.OrderNumber = new Random().Next(111111, 999999).ToString();
            order.OrderState = EnumOrderState.Completed;
            order.PaymentTypes = EnumPaymentTypes.CreditCart;
            order.PaymentId = payment.PaymentId;
            order.ConversationId = payment.ConversationId;
            order.OrderDate = DateTime.Now.ToLongDateString();
            order.FirstName = model.FirstName;
            order.LastName = model.LastName;
            order.Email = model.Email;
            order.Phone = model.Phone;
            order.UserId = Convert.ToInt32(userId);
            order.City = model.City;
            order.OrderNote = model.OrderNote;

            foreach (var item in model.CartModel.CartItems)
            {
                var orderItem = new FarmerOrderItem()
                {
                    Price = item.Price,
                    Quantity = item.Quantity,
                    ProductId = item.ProductId
                };
                order.FarmerOrderItems.Add(orderItem);
            }
            _orderService.Create(order);
        }

        private void ClearCart(string cartId)
        {
            _cartService.ClearCart(cartId);
        }

        private Payment PaymentProcess(OrderModel model)
        {
            Options options = new Options();
            options.ApiKey = "sandbox-eSUe4mWUN7UWoMxg1by5m96a6nzy4c5R";
            options.SecretKey = "sandbox-sWoi2Hye0B5cQYlOZ63oQFE4yidMJ45H";
            options.BaseUrl = "https://sandbox-api.iyzipay.com";

            CreatePaymentRequest request = new CreatePaymentRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = Guid.NewGuid().ToString();
            request.Price = model.CartModel.TotalPrice().ToString().Split(",")[0];
            request.PaidPrice = model.CartModel.TotalPrice().ToString().Split(",")[0];
            request.Currency = Currency.TRY.ToString();
            request.Installment = 1;
            request.BasketId = model.CartModel.CartId.ToString();
            request.PaymentChannel = PaymentChannel.WEB.ToString();
            request.PaymentGroup = PaymentGroup.PRODUCT.ToString();

            PaymentCard paymentCard = new PaymentCard();
            paymentCard.CardHolderName = model.CardName;
            paymentCard.CardNumber = model.CardNumber;
            paymentCard.ExpireMonth = model.ExpirationMonth;
            paymentCard.ExpireYear = model.ExpirationYear;
            paymentCard.Cvc = model.CVV;
            paymentCard.RegisterCard = 0;
            request.PaymentCard = paymentCard;

            Buyer buyer = new Buyer();
            buyer.Id = "BY789";
            buyer.Name = "John";
            buyer.Surname = "Doe";
            buyer.GsmNumber = "+905350000000";
            buyer.Email = "email@email.com";
            buyer.IdentityNumber = "74300864791";
            buyer.LastLoginDate = "2015-10-05 12:43:35";
            buyer.RegistrationDate = "2013-04-21 15:12:09";
            buyer.RegistrationAddress = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            buyer.Ip = "85.34.78.112";
            buyer.City = "Istanbul";
            buyer.Country = "Turkey";
            buyer.ZipCode = "34732";
            request.Buyer = buyer;

            Address shippingAddress = new Address();
            shippingAddress.ContactName = "Jane Doe";
            shippingAddress.City = "Istanbul";
            shippingAddress.Country = "Turkey";
            shippingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            shippingAddress.ZipCode = "34742";
            request.ShippingAddress = shippingAddress;

            Address billingAddress = new Address();
            billingAddress.ContactName = "Jane Doe";
            billingAddress.City = "Istanbul";
            billingAddress.Country = "Turkey";
            billingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            billingAddress.ZipCode = "34742";
            request.BillingAddress = billingAddress;

            List<BasketItem> basketItems = new List<BasketItem>();
            BasketItem basketItem;

            foreach (var item in model.CartModel.CartItems)
            {
                basketItem = new BasketItem();
                basketItem.Id = item.ProductId.ToString();
                basketItem.Name = item.Name;
                basketItem.Category1 = "Collectibles";
                basketItem.ItemType = BasketItemType.PHYSICAL.ToString();
                basketItem.Price = item.Price.ToString().Split(",")[0];

                basketItems.Add(basketItem);
            }

            request.BasketItems = basketItems;

            return Payment.Create(request, options);

        }

        #endregion

        #region User Order Methods

        [HttpGet]
        public IActionResult GetOrders()
        {
            var userId = HttpContext.Session.GetString("ActiveCustomerId");
            var orders = _orderService.GetOrders(userId);
            var orderListModel = new List<OrderListModel>();

            OrderListModel orderModel;

            foreach (var item in orders)
            {
                orderModel = new OrderListModel();
                orderModel.Id = item.Id;
                orderModel.OrderNumber = item.OrderNumber;
                orderModel.OrderDate = item.OrderDate;
                orderModel.OrderNote = item.OrderNote;
                orderModel.Phone = item.Phone;
                orderModel.FirstName = item.FirstName;
                orderModel.LastName = item.LastName;
                orderModel.Email = item.Email;
                orderModel.City = item.City;
                orderModel.Email = item.Email;

                orderModel.OrderItemModels = item.FarmerOrderItems.Select(i => new OrderItemModel()
                {
                    OrderItemId = i.Id,
                    Name = i.FarmerProduct.Name,
                    Price = i.Price,
                    Quantity = i.Quantity,
                    ImageURL = i.FarmerProduct.ImageUrl
                }).ToList();

                orderListModel.Add(orderModel);
            }
            return View(orderListModel);
        }

        #endregion
    }
}

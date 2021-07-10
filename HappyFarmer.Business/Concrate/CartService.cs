using HappyFarmer.Business.Abstract;
using HappyFarmer.DataAccess.Abstract;
using HappyFarmer.Entities;
using System;

namespace HappyFarmer.Business.Concrate
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        public CartService(ICartRepository cartRepository) =>
            _cartRepository = cartRepository;

        public void AddToCart(string userId, int productId, int quantity)
        {
            var cart = GetCartByUserId(userId);

            if (cart != null) //liste içinde eleman yoksa yani eklenen ürün sepette yoksa
            {
                var index = cart.CartItems.FindIndex(i => i.ProductId == productId);

                if (index < 0)
                    cart.CartItems.Add(new FarmerCartItem()
                    {
                        ProductId = productId,
                        Quantity = quantity,
                        CartId = cart.Id
                    });

                else
                    cart.CartItems[index].Quantity += quantity;

                _cartRepository.Update(cart);
            }
        }

        public void ClearCart(string cartId) =>
            _cartRepository.ClearCart(cartId);

        public void DeleteFromCart(string userId, int productId)
        {
            var cart = GetCartByUserId(userId);
            if (cart != null)
            {
                var cardId = cart.Id;

                _cartRepository.DeleteFromCard(cardId, productId);
            }
        }

        public FarmerCart GetCartByUserId(string userId) =>
            _cartRepository.GetByUserId(userId);

        public void InitializeCart(string userId) =>
            _cartRepository.Create(new FarmerCart
            {
                UserId = Convert.ToInt32(userId)
            });
    }
}

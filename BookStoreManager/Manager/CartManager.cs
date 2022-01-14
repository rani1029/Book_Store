using BookStoreModel;
using BookStoreRepository.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreManager.Manager
{
    public class CartManager : ICartManager
    {
        private readonly ICartRepository repository;

        public CartManager(ICartRepository repository)
        {
            this.repository = repository;
        }

        public bool AddToCart(CartModel cart)
        {
            try
            {
                return this.repository.AddToCart(cart);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int UpdateCart(int cartId, int bookQuantity)
        {
            try
            {
                return this.repository.UpdateCart(cartId, bookQuantity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int DeleteCart(int cartId)
        {
            try
            {
                return this.repository.DeleteCart(cartId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public List<CartModel> GetCart(int userId)
        {
            try
            {
                return this.repository.GetCart(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

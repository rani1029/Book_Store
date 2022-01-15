using BookStoreModel;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace BookStoreRepository.Repository
{
    public interface ICartRepository
    {
        IConfiguration Configuration { get; }

        bool AddToCart(CartModel cart);
        int UpdateCart(int cartId, int bookQuantity);
        int DeleteCart(int cartId);
        List<CartModel> GetCart(int userId);
    }
}
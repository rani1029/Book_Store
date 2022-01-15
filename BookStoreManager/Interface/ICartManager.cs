using BookStoreModel;
using System.Collections.Generic;

namespace BookStoreManager.Manager
{
    public interface ICartManager
    {
        bool AddToCart(CartModel cart);
        int UpdateCart(int cartId, int bookQuantity);
        int DeleteCart(int cartId);
        List<CartModel> GetCart(int userId);
    }
}
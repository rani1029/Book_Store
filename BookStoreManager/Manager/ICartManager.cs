using BookStoreModel;

namespace BookStoreManager.Manager
{
    public interface ICartManager
    {
        bool AddToCart(CartModel cart);
        int UpdateCart(int cartId, int bookQuantity);
    }
}
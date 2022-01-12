using BookStoreModel;

namespace BookStoreManager.Manager
{
    public interface IWishlistManager
    {
        bool AddToWishList(WishlistModel wishListmodel);
    }
}
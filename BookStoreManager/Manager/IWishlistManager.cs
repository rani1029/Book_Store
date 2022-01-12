using BookStoreModel;
using System.Collections.Generic;

namespace BookStoreManager.Manager
{
    public interface IWishlistManager
    {
        bool AddToWishList(WishlistModel wishListmodel);
        bool DeleteBookFromWishList(int wishListId);
        List<WishlistModel> GetWishList(int userId);
    }
}
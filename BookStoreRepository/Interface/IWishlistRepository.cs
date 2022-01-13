using BookStoreModel;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace BookStoreRepository
{
    public interface IWishlistRepository
    {

        bool AddToWishList(WishlistModel wishListmodel);
        bool DeleteBookFromWishList(int wishListId);
        List<WishlistModel> GetWishList(int userId);
    }
}
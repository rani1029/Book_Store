using BookStoreModel;
using Microsoft.Extensions.Configuration;

namespace BookStoreRepository
{
    public interface IWishlistRepository
    {

        bool AddToWishList(WishlistModel wishListmodel);
    }
}
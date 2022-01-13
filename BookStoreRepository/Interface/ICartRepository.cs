using BookStoreModel;
using Microsoft.Extensions.Configuration;

namespace BookStoreRepository.Repository
{
    public interface ICartRepository
    {
        IConfiguration Configuration { get; }

        bool AddToCart(CartModel cart);
    }
}
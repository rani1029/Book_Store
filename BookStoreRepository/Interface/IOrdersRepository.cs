using BookStoreModel;
using Microsoft.Extensions.Configuration;

namespace BookStoreRepository.Repository
{
    public interface IOrdersRepository
    {
        IConfiguration Configuration { get; }

        int AddOrder(OrdersModel orders);
    }
}
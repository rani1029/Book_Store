using BookStoreModel;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace BookStoreRepository.Repository
{
    public interface IOrdersRepository
    {
        IConfiguration Configuration { get; }

        int AddOrder(OrdersModel orders);
        List<OrdersModel> GetOrderDetails(int userId);
    }
}
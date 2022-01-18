using BookStoreModel;
using System.Collections.Generic;

namespace BookStoreManager.Manager
{
    public interface IOrdersManager
    {
        int AddOrder(OrdersModel orders);
        List<OrdersModel> GetOrderDetails(int userId);
    }
}
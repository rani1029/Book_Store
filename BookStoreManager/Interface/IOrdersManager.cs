using BookStoreModel;

namespace BookStoreManager.Manager
{
    public interface IOrdersManager
    {
        int AddOrder(OrdersModel orders);
    }
}
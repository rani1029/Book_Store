using BookStoreModel;
using BookStoreRepository.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreManager.Manager
{
    public class OrdersManager : IOrdersManager
    {
        private readonly IOrdersRepository repository;

        public OrdersManager(IOrdersRepository repository)
        {
            this.repository = repository;
        }

        public int AddOrder(OrdersModel orders)
        {
            try
            {
                return this.repository.AddOrder(orders);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<OrdersModel> GetOrderDetails(int userId)
        {
            try
            {
                return this.repository.GetOrderDetails(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

using BookStoreModel;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace BookStoreRepository.Repository
{
    public class OrdersRepository : IOrdersRepository
    {
        public IConfiguration Configuration { get; }

        public OrdersRepository(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        SqlConnection sqlConnection;

        public int AddOrder(OrdersModel orders)
        {
            try
            {
                sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDb"));
                SqlCommand sqlCommand = new SqlCommand("Sp_PlaceOrder", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@UserId", orders.UserId);
                sqlCommand.Parameters.AddWithValue("@AddressId", orders.AddressId);
                sqlCommand.Parameters.AddWithValue("@BookId", orders.BookId);
                sqlCommand.Parameters.AddWithValue("@CartId", orders.CartId);
                sqlCommand.Parameters.AddWithValue("@quantity", orders.BookQuantity);
                sqlConnection.Open();
                int result = sqlCommand.ExecuteNonQuery();
                if (result > 0)
                    return 1;
                else
                    return 0;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }


}

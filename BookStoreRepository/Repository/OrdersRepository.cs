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

        public List<OrdersModel> GetOrderDetails(int userId)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDb"));

            try
            {

                SqlCommand sqlCommand = new SqlCommand("spGetOrders", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@UserId", userId);
                sqlConnection.Open();
                SqlDataReader sqlData = sqlCommand.ExecuteReader();
                List<OrdersModel> order = new List<OrdersModel>();
                if (sqlData.HasRows)
                {
                    while (sqlData.Read())
                    {
                        OrdersModel orderModel = new OrdersModel();
                        BookModel bookModel = new BookModel();
                        bookModel.BookName = sqlData["BookName"].ToString();
                        bookModel.AuthorName = sqlData["AuthorName"].ToString();
                        bookModel.BookDescription = sqlData["BookDescription"].ToString();
                        bookModel.Price = Convert.ToInt32(sqlData["Price"]);
                        bookModel.OriginalPrice = Convert.ToInt32(sqlData["OriginalPrice"]);
                        bookModel.Image = sqlData["Image"].ToString();
                        orderModel.OrderId = Convert.ToInt32(sqlData["OrderId"]);
                        orderModel.GetBook = bookModel;
                        order.Add(orderModel);
                    }
                    return order;
                }
                else
                {
                    return null;
                }
            }

            catch (ArgumentNullException ex)
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

using BookStoreModel;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace BookStoreRepository.Repository
{
    public class CartRepository : ICartRepository
    {
        public CartRepository(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        SqlConnection sqlConnection;

        public bool AddToCart(CartModel cart)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDb"));
            using (sqlConnection)
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("Sp_AddToCart", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@BookId", cart.BookId);
                    sqlCommand.Parameters.AddWithValue("@UserId", cart.UserId);
                    sqlCommand.Parameters.AddWithValue("@Quantity", cart.BookQuantity);
                    var result = sqlCommand.ExecuteNonQuery();
                    if (result > 0)
                        return true;
                    else
                        return false;

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
}

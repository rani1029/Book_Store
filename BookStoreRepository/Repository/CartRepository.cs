using BookStoreModel;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
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
                    sqlCommand.Parameters.AddWithValue("@BookId", cart.BookId);
                    sqlCommand.Parameters.AddWithValue("@UserId", cart.UserId);
                    sqlCommand.Parameters.AddWithValue("@Quantity", cart.BookQuantity);
                    sqlCommand.Parameters.Add("@cart", SqlDbType.Int).Direction = ParameterDirection.Output;
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                    var result = sqlCommand.Parameters["@cart"].Value;
                    if (result.Equals(2))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

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

        public int UpdateCart(int cartId, int bookQuantity)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDb"));
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand("Sp_UpdateQuantity", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@CartId", cartId);
                    sqlCommand.Parameters.AddWithValue("@BookQuantity", bookQuantity);
                    sqlConnection.Open();
                    int result = sqlCommand.ExecuteNonQuery();
                    return result;
                }
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

        public int DeleteCart(int cartId)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDb"));
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand("Sp_DeleteCart", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@CartId", cartId);
                    sqlConnection.Open();
                    int result = sqlCommand.ExecuteNonQuery();
                    return result;
                }
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

        public List<CartModel> GetCart(int userId)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDb"));
            using (sqlConnection)
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("spGetCart", sqlConnection);

                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@userId", userId);
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    List<CartModel> cartItems = new List<CartModel>();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            CartModel cart = new CartModel();
                            BookModel book = new BookModel();
                            cart.BookId = Convert.ToInt32(reader[0]);
                            book.BookName = reader[1].ToString();
                            book.AuthorName = reader[2].ToString();
                            book.Price = Convert.ToInt32(reader[3]);
                            book.Image = reader[8].ToString();
                            book.OriginalPrice = Convert.ToInt32(reader[4]);
                            book.BookCount = Convert.ToInt32(reader[7]);
                            cart.CartId = Convert.ToInt32(reader[5]);
                            cart.BookQuantity = Convert.ToInt32(reader[6]);
                            cart.UserId = Convert.ToInt32(reader[9]);
                            cartItems.Add(cart);
                        }

                    }
                    return cartItems;
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



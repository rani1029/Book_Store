using BookStoreModel;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace BookStoreRepository
{
    public class WishlistRepository : IWishlistRepository
    {
        public IConfiguration Configuration { get; }

        public WishlistRepository(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        SqlConnection sqlConnection;

        public bool AddToWishList(WishlistModel wishListmodel)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDb"));
            using (sqlConnection)
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("SpAddToWishlist", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@BookId", wishListmodel.BookId);
                    sqlCommand.Parameters.AddWithValue("@UserId", wishListmodel.UserId);
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

        public bool DeleteBookFromWishList(int WishListId)
        {
            try
            {

                sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDb"));
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand("SpDeleteFromWishlist", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@WishlistId", WishListId);
                    sqlConnection.Open();
                    var result = sqlCommand.ExecuteNonQuery();
                    if (result > 0)
                        return true;
                    else
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

        public List<WishlistModel> GetWishList(int userId)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
            using (sqlConnection)
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("SpGetBooksFromWishList", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@UserId", userId);
                    SqlDataReader read = sqlCommand.ExecuteReader();
                    if (read.HasRows)
                    {
                        List<WishlistModel> wishList = new List<WishlistModel>();
                        while (read.Read())
                        {
                            BookModel booksModel = new BookModel();
                            WishlistModel wishListModel = new WishlistModel();

                            wishListModel.WishlistId = Convert.ToInt32(read["WishlistId"]);
                            wishListModel.UserId = Convert.ToInt32(read["UserId"]);
                            wishListModel.BookId = Convert.ToInt32(read["BookId"]);
                            booksModel.AuthorName = read["AuthorName"].ToString();
                            booksModel.BookName = read["BookName"].ToString();
                            booksModel.BookDescription = read["BookDescription"].ToString();
                            booksModel.Price = Convert.ToInt32(read["Price"]);
                            booksModel.BookCount = Convert.ToInt32(read["BookCount"]);
                            booksModel.Rating = Convert.ToInt32(read["Rating"]);
                            booksModel.RatingCount = Convert.ToInt32(read["RatingCount"]);
                            booksModel.Image = read["Image"].ToString();
                            booksModel.OriginalPrice = Convert.ToInt32(read["OriginalPrice"]);
                            wishListModel.getBook = booksModel;
                            wishList.Add(wishListModel);
                        }
                        return wishList;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    sqlConnection.Close();
                }
        }

    }
}

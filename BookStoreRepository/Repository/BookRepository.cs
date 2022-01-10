using BookStoreModel;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace BookStoreRepository.Repository
{
    public class BookRepository : IBookRepository
    {
        public IConfiguration Configuration { get; }
        public BookRepository(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        SqlConnection sqlConnection;
        public int AddBook(BookModel bookmodel)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDb"));
            using (sqlConnection)
                try
                {

                    SqlCommand sqlCommand = new SqlCommand("[dbo].[SpAddBook]", sqlConnection);

                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlConnection.Open();

                    sqlCommand.Parameters.AddWithValue("@BookName", bookmodel.BookName);
                    sqlCommand.Parameters.AddWithValue("@AuthorName", bookmodel.AuthorName);
                    sqlCommand.Parameters.AddWithValue("@Price", bookmodel.Price);
                    sqlCommand.Parameters.AddWithValue("@originalPrice", bookmodel.OriginalPrice);
                    sqlCommand.Parameters.AddWithValue("@BookDescription", bookmodel.BookDescription);
                    sqlCommand.Parameters.AddWithValue("@BookImage", bookmodel.Image);
                    sqlCommand.Parameters.AddWithValue("@Rating", bookmodel.Rating);
                    sqlCommand.Parameters.AddWithValue("@RatingCount", bookmodel.RatingCount);
                    sqlCommand.Parameters.AddWithValue("@Count", bookmodel.BookCount);
                    int result = sqlCommand.ExecuteNonQuery();
                    if (result > 0)
                        return 1;
                    else
                        return 0;
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
        public BookModel GetBook(int bookId)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
            using (sqlConnection)
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("spGetParticularBook", sqlConnection);

                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@BookId", bookId);
                    BookModel booksModel = new BookModel();
                    SqlDataReader read = sqlCommand.ExecuteReader();
                    if (read.Read())
                    {
                        booksModel.AuthorName = read["AuthorName"].ToString();
                        booksModel.BookName = read["BookName"].ToString();
                        booksModel.BookDescription = read["BookDescription"].ToString();
                        booksModel.Price = Convert.ToInt32(read["Price"]);
                        booksModel.BookCount = Convert.ToInt32(read["BookCount"]);
                        booksModel.Rating = Convert.ToInt32(read["Rating"]);
                        booksModel.RatingCount = Convert.ToInt32(read["RatingCount"]);
                        booksModel.Image = read["Image"].ToString();
                        booksModel.OriginalPrice = Convert.ToInt32(read["OriginalPrice"]);
                    }
                    return booksModel;
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

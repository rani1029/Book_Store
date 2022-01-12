using BookStoreModel;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
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

        public bool UpdateBook(BookModel bookmodel)
        {
            var book = GetBook(bookmodel.BookId);
            var price = bookmodel.Price;
            var originalPrice = bookmodel.OriginalPrice;
            var bcount = bookmodel.BookCount;
            if (bookmodel.Price == 0)
            {
                price = book.Price;
            };
            if (bookmodel.OriginalPrice == 0)
            {
                originalPrice = book.OriginalPrice;
            };
            if (bookmodel.BookCount == 0)
            {
                bcount = book.BookCount;
            };
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
            using (sqlConnection)
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("spUpdateBook", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@BookId", bookmodel.BookId);
                    sqlCommand.Parameters.AddWithValue("@BookName", bookmodel.BookName ??= book.BookName);
                    sqlCommand.Parameters.AddWithValue("@AuthorName", bookmodel.AuthorName ??= book.AuthorName);
                    sqlCommand.Parameters.AddWithValue("@Price", price);
                    sqlCommand.Parameters.AddWithValue("@originalPrice", originalPrice);
                    sqlCommand.Parameters.AddWithValue("@BookDescription", bookmodel.BookDescription ??= book.BookDescription);
                    sqlCommand.Parameters.AddWithValue("@BookImage", bookmodel.Image ??= book.Image);
                    sqlCommand.Parameters.AddWithValue("@BookCount", bcount);
                    sqlCommand.Parameters.Add("@book", SqlDbType.Int);
                    sqlCommand.Parameters["@book"].Direction = ParameterDirection.Output;
                    sqlCommand.ExecuteNonQuery();
                    var result = sqlCommand.Parameters["@book"].Value;
                    if (result.Equals(bookmodel.BookId))
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

        public List<BookModel> GetAllBooks()
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
            using (sqlConnection)
                try
                {

                    SqlCommand sqlCommand = new SqlCommand("[dbo].[SpGetAllBooks]", sqlConnection);

                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlConnection.Open();

                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    if (reader.HasRows)
                    {
                        List<BookModel> bookList = new List<BookModel>();
                        while (reader.Read())
                        {
                            BookModel booksModel = new BookModel();
                            booksModel.BookId = Convert.ToInt32(reader["BookId"]);
                            booksModel.AuthorName = reader["AuthorName"].ToString();
                            booksModel.BookName = reader["BookName"].ToString();
                            booksModel.BookDescription = reader["BookDescription"].ToString();
                            booksModel.Price = Convert.ToInt32(reader["Price"]);
                            booksModel.Image = reader["Image"].ToString();
                            booksModel.OriginalPrice = Convert.ToInt32(reader["OriginalPrice"]);
                            booksModel.BookCount = Convert.ToInt32(reader["BookCount"]);
                            booksModel.Rating = Convert.ToInt32(reader["Rating"]);
                            booksModel.RatingCount = Convert.ToInt32(reader["RatingCount"]);

                            bookList.Add(booksModel);
                        }
                        return bookList;
                    }
                    else
                    {
                        return null;
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

        public int DeleteBook(int bookId)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand("SpDeleteBook", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@BookId", bookId);

                    sqlConnection.Open();
                    int result = sqlCommand.ExecuteNonQuery();
                    return result;
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

using BookStore_App.BookStoreModel;
using BookStoreModel;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace BookStoreRepository.Repository
{
    public class FeedBackRepository : IFeedBackRepository
    {
        public FeedBackRepository(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        SqlConnection sqlConnection;

        public int AddFeedBack(FeedBackModel feedback)
        {
            try
            {
                sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDb"));
                SqlCommand sqlCommand = new SqlCommand("Sp_AddFeedback", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@UserId", feedback.UserId);
                sqlCommand.Parameters.AddWithValue("@BookId", feedback.BookId);
                sqlCommand.Parameters.AddWithValue("@Comments", feedback.Comments);
                sqlCommand.Parameters.AddWithValue("@Ratings", feedback.Ratings);
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

        public List<FeedBackModel> GetfeedBacks(int bookId)
        {
            try
            {
                sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDb"));
                SqlCommand sqlCommand = new SqlCommand("Sp_AddFeedback", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@BookId", bookId);
                sqlConnection.Open();
                SqlDataReader sqlData = sqlCommand.ExecuteReader();
                List<FeedBackModel> feedback = new List<FeedBackModel>();
                if (sqlData.HasRows)
                {
                    while (sqlData.Read())
                    {
                        FeedBackModel feedbackModel = new FeedBackModel();
                        SignUpModel user = new SignUpModel();
                        user.CustomerName = sqlData["CustomerName"].ToString();
                        feedbackModel.Comments = sqlData["Comments"].ToString();
                        feedbackModel.Ratings = Convert.ToInt32(sqlData["Ratings"]);
                        feedbackModel.UserId = Convert.ToInt32(sqlData["UserId"]);
                        feedbackModel.UserReference = user;
                        feedback.Add(feedbackModel);
                    }
                    return feedback;
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
    }
}

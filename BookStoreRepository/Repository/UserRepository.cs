using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace BookStoreRepository
{
    public class UserRepository : IUserRepository
    {
        public IConfiguration Configuration { get; }
        public UserRepository(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        //declare the Ado classes
        SqlConnection sqlConnection;

        public bool Register(SignUpModel UserSignUp)
        {
            // instantiate connection with connection string  
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDb"));
            try
            {
                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("sp_AddCustomer", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Name", UserSignUp.CustomerName);
                    cmd.Parameters.AddWithValue("@Email", UserSignUp.Email);
                    cmd.Parameters.AddWithValue("@Password", UserSignUp.Password);
                    cmd.Parameters.AddWithValue("@Phone", UserSignUp.PhoneNumber);
                    sqlConnection.Open();
                    cmd.ExecuteNonQuery();
                    sqlConnection.Close();
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

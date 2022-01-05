using BookStore_App.BookStoreModel;
using BookStoreModel;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace BookStore_App.BookStoreRepository
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

        public SignUpModel Register(SignUpModel UserSignUp)
        {
            SignUpModel UserModel = new SignUpModel();
            // instantiate connection with connection string  
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDb"));
            try
            {
                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("sp_AddCustomer", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    UserSignUp.Password = EncryptPassword(UserSignUp.Password);
                    cmd.Parameters.AddWithValue("@Name", UserSignUp.CustomerName);
                    cmd.Parameters.AddWithValue("@Email", UserSignUp.Email);
                    cmd.Parameters.AddWithValue("@Password", UserSignUp.Password);
                    cmd.Parameters.AddWithValue("@Phone", UserSignUp.PhoneNumber);
                    sqlConnection.Open();
                    cmd.ExecuteNonQuery();
                    sqlConnection.Close();
                }
                return UserModel;
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
        //Password encryption
        public string EncryptPassword(string password)
        {
            SHA256 sha256Hash = SHA256.Create();
            byte[] bytesRepresentation = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(bytesRepresentation);
        }
    }
}

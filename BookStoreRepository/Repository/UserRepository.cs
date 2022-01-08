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

        public int Register(SignUpModel UserSignUp)
        {
            //var result = 0;
            // instantiate connection with connection string  
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDb"));
            try
            {
                if (UserSignUp != null)
                {
                    using (sqlConnection)
                    {
                        SqlCommand cmd = new SqlCommand("sp_AddCustomer", sqlConnection);
                        // SqlCommand.CommandType = CommandType.StoredProcedure;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        //UserSignUp.Password = EncryptPassword(UserSignUp.Password);
                        cmd.Parameters.AddWithValue("@Name", UserSignUp.CustomerName);
                        cmd.Parameters.AddWithValue("@Email", UserSignUp.Email);
                        cmd.Parameters.AddWithValue("@Password", UserSignUp.Password);
                        cmd.Parameters.AddWithValue("@Phone", UserSignUp.PhoneNumber);
                        sqlConnection.Open();
                        int result = cmd.ExecuteNonQuery();
                        //result = cmd.ExecuteScalar();

                        sqlConnection.Close();
                        return result;
                    }
                }
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

        public string Login(LoginModel login)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDb"));
            try
            {
                using (sqlConnection)
                {
                    string storeprocedure = "spLoginDetails";
                    SqlCommand sqlCommand = new SqlCommand(storeprocedure, sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@Email", login.Email);
                    sqlCommand.Parameters.AddWithValue("@Password", EncryptPassword(login.Password));
                    sqlConnection.Open();
                    SignUpModel signUpModel = new SignUpModel();
                    SqlDataReader sqlData = sqlCommand.ExecuteReader();
                    while (sqlData.Read())
                    {
                        signUpModel.CustomerId = Convert.ToInt32(sqlData["UserId"]);
                        signUpModel.CustomerName = sqlData["Name"].ToString();
                        signUpModel.Email = sqlData["Email"].ToString();
                        //signUpModel.PhoneNumber = Convert.ToInt64(sqlData["Phone"]);
                        if (signUpModel != null)
                        {
                            return "Login Successful";
                        }


                    }
                }
                return "Login Failed";
            }


            catch (Exception e)
            {
                throw new Exception(e.Message);
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

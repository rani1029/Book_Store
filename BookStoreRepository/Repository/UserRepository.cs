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
                        SqlCommand cmd = new SqlCommand("sp_SignUp", sqlConnection);

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

        public int Login(LoginModel login)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDb"));
            try
            {
                using (sqlConnection)
                {
                    string storeprocedure = "spLogin";
                    SqlCommand sqlCommand = new SqlCommand(storeprocedure, sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@EmailId", login.Email);
                    sqlCommand.Parameters.AddWithValue("@Password", login.Password);
                    //sqlCommand.Parameters.AddWithValue("@Password", EncryptPassword(login.Password));
                    sqlCommand.Parameters.Add("@User", SqlDbType.Int).Direction = ParameterDirection.Output;
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                    int UserValue = (int)sqlCommand.Parameters["@user"].Value;
                    if (UserValue == 2)
                    {
                        return 2;
                    }
                    else if (UserValue == 1)
                    {
                        return 1;
                    }
                    return 0;
                }
                return -1;

            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool ResetPassword(ResetModel resetPassword)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDb"));
            using (sqlConnection)
            {
                try
                {
                    //passing query in terms of stored procedure
                    SqlCommand sqlCommand = new SqlCommand("Sp_ResetPassword", sqlConnection);
                    //passing command type as stored procedure
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlConnection.Open();
                    //adding the parameter to the strored procedure
                    sqlCommand.Parameters.AddWithValue("@EmailId", resetPassword.Email);
                    sqlCommand.Parameters.AddWithValue("@NewPassword", resetPassword.NewPassword);
                    sqlCommand.Parameters.Add("@result", SqlDbType.Int);
                    sqlCommand.Parameters["@result"].Direction = ParameterDirection.Output;
                    //checking the result 
                    sqlCommand.ExecuteNonQuery();

                    var result = sqlCommand.Parameters["@result"].Value;
                    if (!(result is DBNull))
                        return true;
                    else
                        return false;
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

}

////Password encryption
//public string EncryptPassword(string password)
//{
//    SHA256 sha256Hash = SHA256.Create();
//    byte[] bytesRepresentation = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
//    return BitConverter.ToString(bytesRepresentation);
//}



using BookStore_App.BookStoreModel;
using BookStoreModel;
using Experimental.System.Messaging;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
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
                        UserSignUp.Password = EncryptPassword(UserSignUp.Password);
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

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
                    login.Password = EncryptPassword(login.Password);
                    sqlCommand.Parameters.AddWithValue("@EmailId", login.Email);
                    //sqlCommand.Parameters.AddWithValue("@Password", login.Password);
                    sqlCommand.Parameters.AddWithValue("@Password", login.Password);
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

        public string ForgotPassword(string email)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
            try
            {
                using (sqlConnection)
                {
                    string storeprocedure = "Sp_ForgetPassword";
                    SqlCommand sqlCommand = new SqlCommand(storeprocedure, sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@EmailId", email);
                    sqlCommand.Parameters.Add("@result", SqlDbType.Int);
                    sqlCommand.Parameters["@result"].Direction = ParameterDirection.Output;
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();

                    var result = sqlCommand.Parameters["@result"].Value;
                    if (!(result is DBNull))
                    {
                        this.SMTPmail(email);
                        return "Email sent to user";
                    }
                    else
                    {
                        return "Email does not Exists";
                    }
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

        public void SMTPmail(string email)
        {
            MailMessage mailId = new MailMessage();
            SmtpClient smtpServer = new SmtpClient("smtp.gmail.com"); ////allow App to sent email using SMTP 
            mailId.From = new MailAddress(this.Configuration["Credentials:Email"]); ////contain mail id from where maill will send
            mailId.To.Add(email); //// the user mail to which maill will be send
            mailId.Subject = "forgot password issue";
            this.SendMSMQ();
            mailId.Body = this.ReceiveMSMQ();
            smtpServer.Port = 587; ////Port no 
            smtpServer.Credentials = new System.Net.NetworkCredential(this.Configuration["Credentials:Email"], this.Configuration["Credentials:Password"]);
            smtpServer.EnableSsl = true;  ////specify smtpserver use ssl or not, default setting is false
            smtpServer.Send(mailId);
        }

        /// <summary>
        /// sets data to the queue
        /// </summary>
        public void SendMSMQ()
        {
            MessageQueue msgQueue; ////provide access to a queue in MSMQ
                                   ////checking this private queue exists or not
            if (MessageQueue.Exists(@".\Private$\BookStore"))
            {
                msgQueue = new MessageQueue(@".\Private$\BookStore"); ////Path for queue
            }
            else
            {
                msgQueue = MessageQueue.Create(@".\Private$\BookStore");
            }

            string body = "Please checkout the below url to create your new password";
            msgQueue.Label = "MailBody"; ////Adding label to queue
                                         ////Sending msg
            msgQueue.Send(body);
        }

        /// <summary>
        /// receives mail
        /// </summary>
        /// <returns>receives msg in queue</returns>
        public string ReceiveMSMQ()
        {
            var receivequeue = new MessageQueue(@".\Private$\BookStore");
            var receivemsg = receivequeue.Receive();
            receivemsg.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            return receivemsg.Body.ToString();
        }
    }

}





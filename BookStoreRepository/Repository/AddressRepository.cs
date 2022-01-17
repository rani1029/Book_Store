using BookStoreModel;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;

namespace BookStoreRepository.Repository
{
    public class AddressRepository : IAddressRepository
    {
        public AddressRepository(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        //public string connectionString { get; set; } = "BookStoreDb";
        SqlConnection sqlConnection;

        public int AddAddress(AddressModel address)
        {
            try
            {
                sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDb"));
                SqlCommand sqlCommand = new SqlCommand("Sp_AddAddress", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@UserId", address.UserId);
                sqlCommand.Parameters.AddWithValue("@Address", address.Address);
                sqlCommand.Parameters.AddWithValue("@City", address.City);
                sqlCommand.Parameters.AddWithValue("@State", address.State);
                sqlCommand.Parameters.AddWithValue("@TypeId", address.AddressTypeId);
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

        public int UpdateAddress(AddressModel address)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
            try
            {
                SqlCommand sqlCommand = new SqlCommand("Sp_UpdateAddress", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@AddressId", address.AddressId);
                sqlCommand.Parameters.AddWithValue("@Address", address.Address);
                sqlCommand.Parameters.AddWithValue("@City", address.City);
                sqlCommand.Parameters.AddWithValue("@State", address.State);
                sqlCommand.Parameters.AddWithValue("@TypeId", address.AddressTypeId);
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

        public List<AddressModel> GetAddressesOfUser(int userId)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
            try
            {
                SqlCommand sqlCommand = new SqlCommand("Sp_GetAddressesOfUser", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@UserId", userId);
                sqlConnection.Open();
                SqlDataReader sqlData = sqlCommand.ExecuteReader();
                if (sqlData.HasRows)
                {
                    List<AddressModel> UserAddresses = new List<AddressModel>();
                    while (sqlData.Read())
                    {
                        AddressModel address = new AddressModel();
                        address.AddressId = Convert.ToInt32(sqlData["AddressId"]);
                        address.Address = sqlData["Address"].ToString();
                        address.City = sqlData["City"].ToString();
                        address.State = sqlData["State"].ToString();
                        address.AddressTypeId = Convert.ToInt32(sqlData["AddressTypeId"]);
                        UserAddresses.Add(address);
                    }
                    return UserAddresses;
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

        public List<AddressModel> GetAllRegisteredAddresses()
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
            try
            {
                SqlCommand sqlCommand = new SqlCommand("Sp_GetAllAddresses", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlConnection.Open();
                SqlDataReader sqlData = sqlCommand.ExecuteReader();
                if (sqlData.HasRows)
                {
                    List<AddressModel> UserAddresses = new List<AddressModel>();
                    while (sqlData.Read())
                    {
                        AddressModel address = new AddressModel();
                        address.AddressId = Convert.ToInt32(sqlData["AddressId"]);
                        address.Address = sqlData["Address"].ToString();
                        address.City = sqlData["City"].ToString();
                        address.State = sqlData["State"].ToString();
                        address.AddressTypeId = Convert.ToInt32(sqlData["AddressTypeId"]);
                        address.UserId = Convert.ToInt32(sqlData["UserId"]);
                        UserAddresses.Add(address);
                    }
                    return UserAddresses;
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


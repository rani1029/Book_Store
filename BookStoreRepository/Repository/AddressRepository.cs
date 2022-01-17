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

    }
}

using BookStoreModel;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace BookStoreRepository.Repository
{
    public interface IAddressRepository
    {
        IConfiguration Configuration { get; }

        int AddAddress(AddressModel address);
        int UpdateAddress(AddressModel address);
        List<AddressModel> GetAddressesOfUser(int userId);
    }
}
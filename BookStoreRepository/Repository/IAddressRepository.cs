using BookStoreModel;
using Microsoft.Extensions.Configuration;

namespace BookStoreRepository.Repository
{
    public interface IAddressRepository
    {
        IConfiguration Configuration { get; }

        int AddAddress(AddressModel address);
    }
}
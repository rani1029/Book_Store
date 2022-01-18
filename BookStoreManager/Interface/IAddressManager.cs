using BookStoreModel;
using System.Collections.Generic;

namespace BookStoreManager.Manager
{
    public interface IAddressManager
    {
        int AddAddress(AddressModel address);
        int UpdateAddress(AddressModel addressModel);
        List<AddressModel> GetAddressesOfUser(int userId);
        List<AddressModel> GetAllRegisteredAddresses();

    }
}
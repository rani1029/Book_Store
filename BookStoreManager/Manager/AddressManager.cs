using BookStoreModel;
using BookStoreRepository.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreManager.Manager
{
    public class AddressManager : IAddressManager
    {
        private IAddressRepository repository;

        public AddressManager(IAddressRepository repository)
        {
            this.repository = repository;
        }

        public int AddAddress(AddressModel address)
        {
            try
            {
                return this.repository.AddAddress(address);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}

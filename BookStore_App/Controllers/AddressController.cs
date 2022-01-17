using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreManager.Manager;
using BookStoreModel;
using Microsoft.AspNetCore.Mvc;

namespace BookStore_App.Controllers
{
    public class AddressController : Controller
    {
        private readonly IAddressManager manager;

        public AddressController(IAddressManager manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        [Route("api/AddAddress")]
        public IActionResult AddAddress([FromBody] AddressModel addressModel)
        {
            try
            {
                var result = this.manager.AddAddress(addressModel);
                if (result == 1)
                {

                    return this.Ok(new { Status = true, Message = " New Address Added Successfully !", data = result });
                }
                else
                {

                    return this.BadRequest(new { Status = false, Message = "Failed to add new Address" });
                }
            }
            catch (Exception ex)
            {

                return this.NotFound(new { Status = false, Message = ex.Message });

            }
        }

        [HttpPut]
        [Route("api/UpdateAddress")]
        public IActionResult EditAddress([FromBody] AddressModel addressModel)
        {
            try
            {
                var result = this.manager.UpdateAddress(addressModel);
                if (result == 1)
                {

                    return this.Ok(new { Status = true, Message = "  Address Updated Successfully !", data = result });
                }
                else
                {

                    return this.BadRequest(new { Status = false, Message = "Failed to Update  Address" });
                }
            }
            catch (Exception ex)
            {

                return this.NotFound(new { Status = false, Message = ex.Message });

            }
        }

        [HttpGet]
        [Route("api/GetUserAddresses")]
        public IActionResult GetUserAddresses(int userId)
        {
            try
            {
                var result = this.manager.GetAddressesOfUser(userId);
                if (result != null)
                {

                    return this.Ok(new { Status = true, Message = "  Address Retrived Successfully !", data = result });
                }
                else
                {

                    return this.BadRequest(new { Status = false, Message = "Failed to retrieve Addresses" });
                }
            }
            catch (Exception ex)
            {

                return this.NotFound(new { Status = false, Message = ex.Message });

            }
        }

        [HttpGet]
        [Route("api/GetAllRegisteredAddresses")]
        public IActionResult GetAllAddresses()
        {
            try
            {
                var result = this.manager.GetAllRegisteredAddresses();
                if (result != null)
                {

                    return this.Ok(new { Status = true, Message = "  Address Retrived Successfully !", data = result });
                }
                else
                {

                    return this.BadRequest(new { Status = false, Message = "Failed to retrieve Addresses" });
                }
            }
            catch (Exception ex)
            {

                return this.NotFound(new { Status = false, Message = ex.Message });

            }
        }

    }
}

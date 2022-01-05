using System;
using BookStore_App.BookStoreModel;
using BookStore_App.Manager;
using Microsoft.AspNetCore.Mvc;

namespace BookStore_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManager manager;

        public UserController(IUserManager manager)
        {
            this.manager = manager;

        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody] SignUpModel userDetails)
        {
            try
            {
                var result = this.manager.Register(userDetails);
                if (result)
                {

                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Added New User Successfully !" });
                }
                else
                {

                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Failed to add new user, Try again" });
                }
                return SignUpModel;
            }

            catch (Exception ex)
            {

                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });

            }
        }
    }
}

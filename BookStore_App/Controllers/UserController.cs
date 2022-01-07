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
                if (result != 0)
                {

                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Added New User Successfully !" });
                }
                else
                {

                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Failed to add new user, Try again" });
                }

            }

            catch (Exception ex)
            {

                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });

            }
        }

        [HttpPost]
        [Route("api/login")]
        public IActionResult Login([FromBody] LoginModel userlogin)
        {
            try
            {
                string result = this.manager.Login(userlogin);
                if (result.Equals("Login Successful"))
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
    }
}

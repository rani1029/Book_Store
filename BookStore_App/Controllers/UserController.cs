using System;
using BookStore_App.BookStoreModel;
using BookStore_App.Manager;
using BookStoreModel;
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
        [Route("SignUp")]
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
                int result = this.manager.Login(userlogin);
                if (result == 2)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Login is successful" });
                }
                else if (result == 1)
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Password is incorrect and Login is Unsuccessful" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Email is incorrect and Login is Unsuccessful" });
                }

            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("resetpassword")]
        public IActionResult ResetPassword([FromBody] ResetModel resetPassword)
        {
            var result = this.manager.ResetPassword(resetPassword);
            try
            {
                if (result)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Successfully changed password !" });

                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Try again !" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = e.Message });
            }

        }
    }
}

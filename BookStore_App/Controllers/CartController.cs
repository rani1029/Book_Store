using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore_App.BookStoreModel;
using BookStoreManager.Manager;
using BookStoreModel;
using Microsoft.AspNetCore.Mvc;

namespace BookStore_App.Controllers
{
    public class CartController : Controller
    {
        public readonly ICartManager manager;

        public CartController(ICartManager manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        [Route("AddToCart")]
        public IActionResult AddToCart(CartModel cartModel)
        {
            try
            {
                var result = this.manager.AddToCart(cartModel);
                if (result)
                {
                    return this.Ok(new { Status = true, Message = "Book Added To Cart Successfully !" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Book can not be added To Cart !" });
                }

            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }

        }

        [HttpPost]
        [Route("UpdateCart")]
        public IActionResult UpdateCart(int cartId, int bookQuantity)
        {
            try
            {
                var result = this.manager.UpdateCart(cartId, bookQuantity);
                if (result > 0)
                {
                    return this.Ok(new { Status = true, Message = " Cart updated Successfully !" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = " cart updation failed !" });
                }

            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }

        }

        [HttpDelete]
        [Route("DeleteCart")]
        public IActionResult DeleteCart(int cartId)
        {
            try
            {
                var result = this.manager.DeleteCart(cartId);
                if (result > 0)
                {
                    return this.Ok(new { Status = true, Message = " Cart deleted Successfully !" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = " cart updation failed !" });
                }

            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }

        }
        [HttpGet]
        [Route("GetCart")]
        public IActionResult GetCart(int userId)
        {
            try
            {
                var result = this.manager.GetCart(userId);
                if (result != null)
                {

                    return this.Ok(new { Status = true, Message = "Cart item is Retrieved", Data = result });
                }
                else
                {

                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "No Item in cart" });
                }
            }
            catch (Exception ex)
            {

                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }



    }
}

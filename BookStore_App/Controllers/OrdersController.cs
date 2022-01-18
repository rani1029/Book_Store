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
    public class OrdersController : Controller
    {
        private readonly IOrdersManager manager;

        public OrdersController(IOrdersManager manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        [Route("api/AddOrders")]
        public IActionResult AddOrder([FromBody] OrdersModel orders)
        {
            try
            {
                var result = this.manager.AddOrder(orders);
                if (result == 1)
                {

                    return this.Ok(new { Status = true, Message = " New Orders Added Successfully !", data = result });
                }
                else
                {

                    return this.BadRequest(new { Status = false, Message = "Failed to add new Order" });
                }
            }
            catch (Exception ex)
            {

                return this.NotFound(new { Status = false, Message = ex.Message });

            }
        }

        [HttpGet]
        [Route("GetOrders")]
        public IActionResult GetOrderDetails(int userId)
        {
            try
            {
                var result = this.manager.GetOrderDetails(userId);

                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Retrieved successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Retrieval unsuccessful" });
                }
            }
            catch (Exception ex)
            {

                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
    }
}

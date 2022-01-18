using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
}

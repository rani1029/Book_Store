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
    public class FeedBackController : Controller
    {
        private readonly IFeedBackManager manager;

        public FeedBackController(IFeedBackManager manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        [Route("addFeedback")]
        public IActionResult AddFeedback([FromBody] FeedBackModel feedback)
        {
            try
            {
                int result = this.manager.AddFeedBack(feedback);

                if (result == 1)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "FeedBack Added" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Feedback addition failed" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("getFeedbacks")]
        public IActionResult GetfeedBacks(int bookId)
        {
            try
            {
                var result = this.manager.GetfeedBacks(bookId);

                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Retrival successful", Data = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Retrival unsuccessful" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

    }
}

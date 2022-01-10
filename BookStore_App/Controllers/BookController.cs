using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreManager.Manager;
using BookStoreModel;
using Microsoft.AspNetCore.Mvc;

namespace BookStore_App.Controllers
{
    public class BookController : ControllerBase
    {
        private readonly IBookManager manager;

        public BookController(IBookManager manager)
        {
            this.manager = manager;

        }

        [HttpPost]
        [Route("AddBook")]
        public IActionResult AddBook([FromBody] BookModel bookmodel)
        {
            try
            {
                var result = this.manager.AddBook(bookmodel);
                if (result == 1)
                {

                    return this.Ok(new { Status = true, Message = "Added New Book Successfully !", data = result });
                }
                else
                {

                    return this.BadRequest(new { Status = false, Message = "Failed to add new book" });
                }
            }
            catch (Exception ex)
            {

                return this.NotFound(new { Status = false, Message = ex.Message });

            }
        }
        [HttpGet]
        [Route("GetBook")]
        public IActionResult GetBook(int bookId)
        {
            var result = this.manager.GetBook(bookId);
            try
            {
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Book is retrived", data = result });

                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Try again" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }

        [HttpPut]
        [Route("api/UpdateBook")]
        public IActionResult UpdateBook(BookModel Bookmodel)
        {
            try
            {
                var result = this.manager.UpdateBook(Bookmodel);
                if (result)
                {

                    return this.Ok(new { Status = true, Message = "Book updated Successfully !" });
                }
                else
                {

                    return this.BadRequest(new { Status = false, Message = "Failed to updated Book" });
                }
            }
            catch (Exception ex)
            {

                return this.NotFound(new { Status = false, Message = ex.Message });

            }
        }

    }
}

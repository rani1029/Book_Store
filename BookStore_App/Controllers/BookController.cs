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

                    return this.Ok(new { Status = true, Message = " New Book Added Successfully !", data = result });
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
                    return this.Ok(new { Status = true, Message = "Book has been retrived", data = result });

                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Book retrieval failed Try again" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
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

                    return this.Ok(new { Status = true, Message = "Book Details updated Successfully !" });
                }
                else
                {

                    return this.BadRequest(new { Status = false, Message = "Failed to update Book Details" });
                }
            }
            catch (Exception ex)
            {

                return this.NotFound(new { Status = false, Message = ex.Message });

            }
        }

        [HttpPost]
        [Route("GetAllBooks")]
        public IActionResult GetAllBooks()
        {
            var result = this.manager.GetAllBooks();
            try
            {
                if (result != null)
                {
                    return this.Ok(new ResponseModel<object>() { Status = true, Message = "all book details Retrieved succssfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "No book exists" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpDelete]
        [Route("api/deleteBook")]
        public IActionResult DeleteBook(int bookId)
        {
            try
            {
                int result = this.manager.DeleteBook(bookId);
                if (result == 1)
                {
                    return this.Ok(new { Status = true, Message = "Book details deleted successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Bookid does not exists", Data = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

    }
}

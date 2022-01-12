using BookStoreModel;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace BookStoreRepository.Repository
{
    public interface IBookRepository
    {
        IConfiguration Configuration { get; }

        int AddBook(BookModel bookmodel);
        BookModel GetBook(int bookId);
        bool UpdateBook(BookModel bookmodel);
        List<BookModel> GetAllBooks();
        int DeleteBook(int bookId);
    }
}
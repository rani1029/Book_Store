using BookStoreModel;
using Microsoft.Extensions.Configuration;

namespace BookStoreRepository.Repository
{
    public interface IBookRepository
    {
        IConfiguration Configuration { get; }

        int AddBook(BookModel bookmodel);
        BookModel GetBook(int bookId);
        bool UpdateBook(BookModel bookmodel);
    }
}
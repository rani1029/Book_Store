using BookStoreModel;

namespace BookStoreManager.Manager
{
    public interface IBookManager
    {
        int AddBook(BookModel bookmodel);
        BookModel GetBook(int bookId);
        bool UpdateBook(BookModel bookmodel);
    }
}
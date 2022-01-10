using BookStoreModel;

namespace BookStoreManager.Manager
{
    public interface IBookManager
    {
        int AddBook(BookModel bookmodel);
        public BookModel GetBook(int bookId);
    }
}
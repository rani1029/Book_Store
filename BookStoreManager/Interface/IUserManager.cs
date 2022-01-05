using BookStore_App.BookStoreModel;
using BookStoreModel;

namespace BookStore_App.Manager
{
    public interface IUserManager
    {
        public SignUpModel Register(SignUpModel UserSignUp);
    }
}
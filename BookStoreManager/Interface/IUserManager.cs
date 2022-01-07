using BookStore_App.BookStoreModel;
using BookStoreModel;

namespace BookStore_App.Manager
{
    public interface IUserManager
    {
        public int Register(SignUpModel UserSignUp);
        string Login(LoginModel userlogin);
    }
}
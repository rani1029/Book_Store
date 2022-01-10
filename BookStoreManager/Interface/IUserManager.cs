using BookStore_App.BookStoreModel;
using BookStoreModel;

namespace BookStore_App.Manager
{
    public interface IUserManager
    {
        public int Register(SignUpModel UserSignUp);
        int Login(LoginModel userlogin);
        bool ResetPassword(ResetModel resetPassword);
        string ForgotPassword(string email);
    }
}
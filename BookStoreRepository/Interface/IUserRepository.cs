using BookStore_App.BookStoreModel;
using BookStoreModel;
using Microsoft.Extensions.Configuration;

namespace BookStore_App.BookStoreRepository
{
    public interface IUserRepository
    {
        public int Register(SignUpModel UserSignUp);
        int Login(LoginModel login);
        bool ResetPassword(ResetModel resetPassword);
        string ForgotPassword(string email);

    }
}
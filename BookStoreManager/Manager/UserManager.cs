using BookStore_App.BookStoreModel;
using BookStore_App.BookStoreRepository;
using BookStoreModel;
using BookStoreRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore_App.Manager
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository repository;
        public UserManager(IUserRepository repository)
        {
            this.repository = repository;
        }
        public int Register(SignUpModel UserSignUp)
        {
            try
            {
                return this.repository.Register(UserSignUp);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int Login(LoginModel login)
        {
            try
            {
                return this.repository.Login(login);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool ResetPassword(ResetModel resetPassword)
        {
            try
            {
                return this.repository.ResetPassword(resetPassword);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string ForgotPassword(string email)
        {
            try
            {
                return this.repository.ForgotPassword(email);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


    }
}

﻿using BookStore_App.BookStoreModel;
using BookStoreModel;
using Microsoft.Extensions.Configuration;

namespace BookStore_App.BookStoreRepository
{
    public interface IUserRepository
    {
        public SignUpModel Register(SignUpModel UserSignUp);

    }
}
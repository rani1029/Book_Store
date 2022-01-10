using BookStoreModel;
using BookStoreRepository.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreManager.Manager
{
    public class BookManager : IBookManager
    {
        private readonly IBookRepository repository;
        public BookManager(IBookRepository repository)
        {
            this.repository = repository;
        }
        public int AddBook(BookModel bookmodel)
        {
            try
            {
                return this.repository.AddBook(bookmodel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public BookModel GetBook(int bookId)
        {
            try
            {
                return this.repository.GetBook(bookId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public bool UpdateBook(BookModel bookmodel)
        {
            try
            {
                return this.repository.UpdateBook(bookmodel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}


using BookStoreModel;
using BookStoreRepository.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreManager.Manager
{
    public class FeedBackManager : IFeedBackManager
    {
        private readonly IFeedBackRepository repository;

        public int AddFeedBack(FeedBackModel feedback)
        {
            try
            {
                return this.repository.AddFeedBack(feedback);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

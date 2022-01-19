using BookStoreModel;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace BookStoreRepository.Repository
{
    public interface IFeedBackRepository
    {
        IConfiguration Configuration { get; }

        int AddFeedBack(FeedBackModel feedback);
        List<FeedBackModel> GetfeedBacks(int bookId);
    }
}
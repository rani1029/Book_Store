using BookStoreModel;
using System.Collections.Generic;

namespace BookStoreManager.Manager
{
    public interface IFeedBackManager
    {
        int AddFeedBack(FeedBackModel feedback);
        List<FeedBackModel> GetfeedBacks(int bookId);
    }
}
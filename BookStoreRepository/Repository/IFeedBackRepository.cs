using BookStoreModel;
using Microsoft.Extensions.Configuration;

namespace BookStoreRepository.Repository
{
    public interface IFeedBackRepository
    {
        IConfiguration Configuration { get; }

        int AddFeedBack(FeedBackModel feedback);
    }
}
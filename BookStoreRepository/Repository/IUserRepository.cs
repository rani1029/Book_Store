using Microsoft.Extensions.Configuration;

namespace BookStoreRepository
{
    public interface IUserRepository
    {
        IConfiguration Configuration { get; }
    }
}
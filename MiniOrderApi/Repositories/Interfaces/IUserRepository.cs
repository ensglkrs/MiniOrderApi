using MiniOrderApi.Entities;

namespace MiniOrderApi.Repositories.Interfaces
{
    public interface IUserRepository
    {
        void Add(User user);
        User? GetByUsername(string username);
    }
}
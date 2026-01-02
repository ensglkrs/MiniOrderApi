using MiniOrderApi.Entities;

namespace MiniOrderApi.Services.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
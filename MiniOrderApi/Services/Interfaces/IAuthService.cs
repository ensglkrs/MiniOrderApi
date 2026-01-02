using MiniOrderApi.DTOs.Auth;

namespace MiniOrderApi.Services.Interfaces
{
    public interface IAuthService
    {
        string Register(RegisterRequest request);
        string Login(LoginRequest request);
    }
}

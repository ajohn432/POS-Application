using POS_Application.Server.Models;

namespace POS_Application.Server.Services.Interfaces
{
    public interface IAuthenticationService
    {
        public Task<LoginResponse> LoginAsync(LoginRequest loginRequest);
        public Task LogoutAsync(string token);
        public Task<SessionInfo> CheckSessionAsync(string token);
        public Task<bool> JwtCheck(string token);
    }
}

using POS_Application.Server.Models;

namespace POS_Application.Server.Services.Interfaces
{
    public interface IAuthenticationService
    {
        public Task<bool> VerifyUserAsync(Authentication authentication);
    }
}

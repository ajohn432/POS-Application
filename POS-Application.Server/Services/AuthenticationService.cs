using Microsoft.EntityFrameworkCore;

using POS_Application.Server.db;
using POS_Application.Server.Models;
using POS_Application.Server.Services.Interfaces;

namespace POS_Application.Server.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly AppDbContext _dbContext;

        public AuthenticationService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> VerifyUserAsync(Authentication authentication)
        {
            var user = await _dbContext.authentications.FirstOrDefaultAsync(u => u.username == authentication.username && u.pass == authentication.pass);

            return user != null;
        }
    }
}

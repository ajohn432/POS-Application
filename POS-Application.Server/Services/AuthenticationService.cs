using Microsoft.EntityFrameworkCore;

using POS_Application.Server.db;
using POS_Application.Server.Models;
using POS_Application.Server.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using System;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;

namespace POS_Application.Server.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly AppDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public AuthenticationService(AppDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest loginRequest)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.EmployeeId == loginRequest.EmployeeId && u.Password == loginRequest.Password);

            if (user == null)
            {
                return null;
            }

            var token = GenerateJwtToken(user);

            var tokenInfo = new TokenInfo
            {
                Token = token,
                EmployeeId = user.EmployeeId,
                IsValid = true
            };

            await _dbContext.Tokens.AddAsync(tokenInfo);
            await _dbContext.SaveChangesAsync();

            return new LoginResponse
            {
                Token = token,
                EmployeeId = user.EmployeeId,
                Role = user.Role
            };
        }

        public async Task LogoutAsync(string token)
        {
            var tokenInfo = await _dbContext.Tokens.FirstOrDefaultAsync(t => t.Token == token);
            // Update token validity
            tokenInfo.IsValid = false;
            await _dbContext.SaveChangesAsync();
            return;
        }

        public async Task<SessionInfo> CheckSessionAsync(string token)
        {
            var tokenInfo = await _dbContext.Tokens.FirstOrDefaultAsync(t => t.Token == token);

            if (tokenInfo == null)
            {
                return null; //Token not found in DB
            }

            var userInfo = await _dbContext.Users.FirstOrDefaultAsync(u => u.EmployeeId == tokenInfo.EmployeeId);

            return new SessionInfo
            {
                IsValid = tokenInfo.IsValid,
                EmployeeId = tokenInfo.EmployeeId,
                Role = userInfo.Role
            };
        }

        public async Task<bool> JwtCheck(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return false;
            }
            var tokenInfo = await _dbContext.Tokens.FirstOrDefaultAsync(t => t.Token == token);

            if (tokenInfo == null || !tokenInfo.IsValid)
            {
                return false;
            }
            return true;
        }

        private string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.EmployeeId),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

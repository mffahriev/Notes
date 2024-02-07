using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services
{
    public class JwtAuthorizationService : IAuthorization
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;

        public JwtAuthorizationService(
            IConfiguration configuration, 
            UserManager<User> userManager
        )
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<TokenDTO> GetAccessToken(LoginUserDTO dto)
        {
            User user = await _userManager.FindByEmailAsync(dto.Email) ?? throw new Exception();

            string jwt = GetJwtToken(
                user.NormalizedEmail ?? throw new ArgumentNullException(), 
                user.Id
            );

            return new TokenDTO(jwt);
        }

        private string GetJwtToken(string email, string userId)
        {
            string secretKeyValue = _configuration["JWT:SecretKey"] ?? throw new ArgumentNullException();
            byte[] secretKeyValueBytes = Encoding.UTF8.GetBytes(secretKeyValue);

            SymmetricSecurityKey secretKey = new SymmetricSecurityKey(secretKeyValueBytes);
            SigningCredentials signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: new List<Claim>() 
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.NameIdentifier, userId)
                },
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: signinCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }
    }
}

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
            IEnumerable<Claim> claims = await GetClaims(dto.Email);
            string jwt = GetJwtToken(claims);

            return new TokenDTO(jwt);
        }

        private string GetJwtToken(IEnumerable<Claim> claims)
        {
            string secretKeyValue = _configuration["JWT:SecretKey"] ?? throw new ArgumentNullException();
            byte[] secretKeyValueBytes = Encoding.UTF8.GetBytes(secretKeyValue);

            SymmetricSecurityKey secretKey = new SymmetricSecurityKey(secretKeyValueBytes);
            SigningCredentials signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["JWT:LifeTimeMinutes"])),
                signingCredentials: signinCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }

        private async Task<IEnumerable<Claim>> GetClaims(string email)
        {
            User user = await _userManager.FindByEmailAsync(email) ?? throw new Exception();
            IEnumerable<string> roles = await _userManager.GetRolesAsync(user);

            List<Claim> claims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();
            claims.AddRange(new Claim[]
            {
                new Claim(ClaimTypes.Email, user.Email ?? throw new NullReferenceException()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.Id)
            });

            return claims;
        }
    }
}

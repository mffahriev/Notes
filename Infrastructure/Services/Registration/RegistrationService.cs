using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services
{
    public class RegistrationService : IRegistration
    {
        private readonly UserManager<User> _userManager;
        private readonly IAuthorization _authorization;

        public RegistrationService(
            UserManager<User> userManager,
            IAuthorization authorization
        )
        {
            _userManager = userManager;
            _authorization = authorization;
        }

        public async Task<TokenDTO> Registration(RegisterUserDTO dto)
        {
            IdentityResult result = await _userManager.CreateAsync(
                new User 
                {
                    Email = dto.Email,
                    UserName = dto.Name
                },
                dto.Password
            );

            if (result.Succeeded)
            {
                return await _authorization.GetAccessToken(new LoginUserDTO(dto.Email, dto.Password));
            }

            throw new Exception("Ошибка регистрации.");
        }
    }
}

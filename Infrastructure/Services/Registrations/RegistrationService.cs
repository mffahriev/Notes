using Core.DTOs;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces;
using Core.Options;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services
{
    public class RegistrationService : IRegistration
    {
        private readonly UserManager<User> _userManager;
        private readonly IFactory<User, UserInitOptions> _userFactory;

        public RegistrationService(
            UserManager<User> userManager,
            IFactory<User, UserInitOptions> userFactory
        )
        {
            _userManager = userManager;
            _userFactory = userFactory;
        }

        public async Task Registration(RegisterUserDTO dto)
        {
            User user = _userFactory.Create(new UserInitOptions(dto.Name, dto.Email));

            IdentityResult result = await _userManager.CreateAsync(
                user,
                dto.Password
            );

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "user");
                return;
            }

            throw new RegistrationException(
                $"Registration error. \n{string.Join("\n", result.Errors.Select(x => x.Description))}"
            );
        }
    }
}

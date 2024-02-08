using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services
{
    public class RegistrationDecorator : IRegistration
    {
        private readonly IRegistration _registrationService;
        private readonly IValidator<RegisterUserDTO> _validator;

        public RegistrationDecorator(
            IRegistration registrationService,
            IValidator<RegisterUserDTO> validator,
            IRepository<Catalog> catalogRepository,
            IConfiguration configuration,
            UserManager<User> userManager
        )
        {
            _registrationService = registrationService;
            _validator = validator;
        }

        public async Task Registration(RegisterUserDTO dto)
        {
            await _validator.ValidateAndThrowAsync(dto);

            await _registrationService.Registration(dto);
        }
    }
}

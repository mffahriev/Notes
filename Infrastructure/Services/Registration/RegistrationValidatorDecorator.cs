using Core.DTOs;
using Core.Interfaces;
using FluentValidation;

namespace Infrastructure.Services
{
    public class RegistrationValidatorDecorator : IRegistration
    {
        private readonly IRegistration _registrationService;
        private readonly IValidator<RegisterUserDTO> _validator;

        public RegistrationValidatorDecorator(
            IRegistration registrationService,
            IValidator<RegisterUserDTO> validator
        )
        {
            _registrationService = registrationService;
            _validator = validator;
        }

        public async Task<TokenDTO> Registration(RegisterUserDTO dto)
        {
            await _validator.ValidateAndThrowAsync(dto);

            return await _registrationService.Registration(dto);
        }
    }
}

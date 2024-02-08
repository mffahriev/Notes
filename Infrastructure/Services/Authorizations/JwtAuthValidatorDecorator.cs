using Core.DTOs;
using Core.Interfaces;
using FluentValidation;

namespace Infrastructure.Services
{
    public class JwtAuthValidatorDecorator : IAuthorization
    {
        private readonly IAuthorization _authorizationService;
        private readonly IValidator<LoginUserDTO> _validator;

        public JwtAuthValidatorDecorator(
            IAuthorization authorizationService,
            IValidator<LoginUserDTO> validator
        )
        {
            _authorizationService = authorizationService;
            _validator = validator;
        }

        public async Task<TokenDTO> GetAccessToken(LoginUserDTO dto)
        {
            await _validator.ValidateAndThrowAsync(dto);

            return await _authorizationService.GetAccessToken(dto);
        }
    }
}

using Core.DTOs;
using Core.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Validators
{
    public class LoginUserDTOValidator : AbstractValidator<LoginUserDTO>
    {
        private readonly UserManager<User> _userManager;

        public LoginUserDTOValidator(UserManager<User> userManager)
        {
            _userManager = userManager;

            RuleFor(x => x.Email).NotNull().NotEmpty();
            RuleFor(x => x.Password).NotEmpty().NotEmpty();
            RuleFor(x => x).MustAsync(CheckPassword);

        }

        private async Task<bool> CheckPassword(LoginUserDTO dto, CancellationToken token)
        {
            User user = await _userManager.FindByEmailAsync(dto.Email) ?? throw new NullReferenceException();
            return await _userManager.CheckPasswordAsync(user, dto.Password);
        }
    }
}

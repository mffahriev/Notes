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

            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty()
                .WithErrorCode("Поле Email не было введено");

            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty()
                .WithErrorCode("Поле Password не было введено");

            RuleFor(x => x)
                .MustAsync(CheckPassword)
                .WithErrorCode("Неправильно введён пароль");

        }

        private async Task<bool> CheckPassword(LoginUserDTO dto, CancellationToken token)
        {
            User user = await _userManager.FindByEmailAsync(dto.Email) ?? throw new NullReferenceException();
            return await _userManager.CheckPasswordAsync(user, dto.Password);
        }
    }
}

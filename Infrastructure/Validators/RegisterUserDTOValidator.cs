using Core.DTOs;
using Core.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Validators
{
    public class RegisterUserDTOValidator : AbstractValidator<RegisterUserDTO>
    {
        private readonly UserManager<User> _userManager;

        public RegisterUserDTOValidator(UserManager<User> userManager)
        {
            _userManager = userManager;

            RuleFor(x => x.Email)
                .MustAsync(CheckNotExistEmail)
                .WithErrorCode("Пользователь с указанным email уже существует");

            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty()
                .WithErrorCode("Поле Email не было введено");

            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty()
                .WithErrorCode("Поле Password не было введено");

            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .WithErrorCode("Поле Name не было введено");

            RuleFor(x => x.ConfirmPassword)
                .NotNull()
                .NotEmpty()
                .WithErrorCode("Поле ConfirmPassword не было введено");

            RuleFor(x => x)
                .Must(x => x.Password == x.ConfirmPassword)
                .WithErrorCode("Пароли не совпадают");

        }

        private async Task<bool> CheckNotExistEmail(string Email, CancellationToken cancellationToken)
        {
            return (await _userManager.FindByEmailAsync(Email)) == null;
        }
    }
}

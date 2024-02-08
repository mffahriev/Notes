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
                .NotNull()
                .NotEmpty()
                .MustAsync(CheckNotExistEmail);

            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.ConfirmPassword)
                .NotNull()
                .NotEmpty()
                .Equal(x => x.Password);

        }

        private async Task<bool> CheckNotExistEmail(string Email, CancellationToken cancellationToken)
        {
            return (await _userManager.FindByEmailAsync(Email)) == null;
        }
    }
}

using Core.DTOs;
using FluentValidation;

namespace Infrastructure.Validators
{
    public class LoginUserDTOValidator : AbstractValidator<LoginUserDTO>
    {
        public LoginUserDTOValidator() 
        {
            RuleFor(x => x.Email).NotNull().NotEmpty();
            RuleFor(x => x.Password).NotEmpty().NotEmpty();
        }
    }
}

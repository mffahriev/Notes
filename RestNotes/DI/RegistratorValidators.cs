using Core.DTOs;
using FluentValidation;
using Infrastructure.Validators;

namespace RestNodes.DI
{
    public static class RegistratorValidators
    {
        public static void AddValidators(this IServiceCollection services, IServiceProvider provider)
        {
            services.AddScoped<IValidator<RegisterUserDTO>, RegisterUserDTOValidator>();
            services.AddScoped<IValidator<LoginUserDTO>, LoginUserDTOValidator>();
        }
    }
}

using Core.DTOs;
using FluentValidation;
using Infrastructure.Validators;

namespace RestNotes.DI
{
    public static class RegistratorValidators
    {
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<RegisterUserDTO>, RegisterUserDTOValidator>();
            services.AddScoped<IValidator<LoginUserDTO>, LoginUserDTOValidator>();

            return services;
        }
    }
}

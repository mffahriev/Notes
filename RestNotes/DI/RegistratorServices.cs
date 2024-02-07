using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using FluentValidation;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;

namespace RestNodes.DI
{
    public static class RegistratorServices
    {
        public static void AddServices(this IServiceCollection services, IServiceProvider provider)
        {
            services.AddAuthorization();
            services.AddRegistration();
        }

        private static void AddAuthorization(this IServiceCollection services)
        {
            services.AddTransient<IAuthorization, JwtAuthValidatorDecorator>(
                x => new JwtAuthValidatorDecorator(
                    new JwtAuthorizationService(
                        x.GetRequiredService<IConfiguration>(),
                        x.GetRequiredService<UserManager<User>>()
                    ),
                    x.GetRequiredService<IValidator<LoginUserDTO>>()
                )
            );
        }

        private static void AddRegistration(this IServiceCollection services)
        {
            services.AddTransient<IRegistration, RegistrationValidatorDecorator>(
                x => new RegistrationValidatorDecorator(
                    new RegistrationService(
                        x.GetRequiredService<UserManager<User>>(),
                        x.GetRequiredService<IAuthorization>()
                    ),
                    x.GetRequiredService<IValidator<RegisterUserDTO>>()
                )
            );
        }
    }
}

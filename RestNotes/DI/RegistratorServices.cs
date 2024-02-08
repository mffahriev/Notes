using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Core.Options;
using FluentValidation;
using Infrastructure.Services;
using Infrastructure.Services.CatalogRepositories;
using Infrastructure.Services.Factories;
using Microsoft.AspNetCore.Identity;

namespace RestNotes.DI
{
    public static class RegistratorServices
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IFactory<User, UserInitOptions>, UserFactory>();
            services.AddTransient<IRepository<Catalog>, CatalogRepository>();
            services.AddAuthorization();
            services.AddRegistration();

            return services;
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
            services.AddTransient<IRegistration, RegistrationDecorator>(
                x => new RegistrationDecorator(
                    new RegistrationService(
                        x.GetRequiredService<UserManager<User>>(),
                        x.GetRequiredService<IFactory<User, UserInitOptions>>()
                    ),
                    x.GetRequiredService<IValidator<RegisterUserDTO>>(),
                    x.GetRequiredService<IRepository<Catalog>>(),
                    x.GetRequiredService<IConfiguration>(),
                    x.GetRequiredService<UserManager<User>>()
                )
            );
        }
    }
}

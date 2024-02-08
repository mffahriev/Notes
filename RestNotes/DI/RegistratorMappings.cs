using Core.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace RestNotes.DI
{
    public static class RegistratorMappings
    {
        public static IServiceCollection AddMappings(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Profiles));

            return services;
        }
    }
}

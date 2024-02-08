using Core.Entities;
using Core.Interfaces;
using Core.Options;
using Infrastructure.Helpers;
using Microsoft.AspNetCore.Identity;

namespace RestNotes.Helpers
{
    public static class Utils
    {
        public static async Task RegisterData(this IServiceProvider provider)
        {
            try
            {
                UserManager<User> userManager = provider.GetRequiredService<UserManager<User>>();
                RoleManager<IdentityRole> rolesManager = provider.GetRequiredService<RoleManager<IdentityRole>>();
                IConfiguration configuration = provider.GetRequiredService<IConfiguration>();
                IFactory<User, UserInitOptions> userFactory = provider.GetRequiredService<IFactory<User, UserInitOptions>>();

                await Initializator.InitializeRoles(rolesManager);
                await Initializator.InitializeAdmin(userManager, configuration, userFactory);
            }
            catch (Exception ex)
            {
                var logger = provider.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "Ошибка при регистрации данных.");
            }
        }
    }
}

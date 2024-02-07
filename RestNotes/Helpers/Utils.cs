using Core.Entities;
using Infrastructure.Helpers;
using Microsoft.AspNetCore.Identity;

namespace RestNodes.Helpers
{
    public static class Utils
    {
        public static async Task RegisterData(this IServiceProvider provider)
        {
            try
            {
                UserManager<User> userManager = provider.GetRequiredService<UserManager<User>>();
                RoleManager<IdentityRole> rolesManager = provider.GetRequiredService<RoleManager<IdentityRole>>();

                await Initializator.InitializeRoles(rolesManager);
                await Initializator.InitializeAdmin(userManager);
            }
            catch (Exception ex)
            {
                var logger = provider.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "Ошибка при регистрации данных.");
            }
        }
    }
}

using Core.Entities;
using Core.Interfaces;
using Core.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Helpers
{
    public static class Initializator
    {
        private const string _adminRole = "admin";
        private const string _userRole = "user";

        public static async Task InitializeRoles(RoleManager<IdentityRole> roleManager)
        {

            if (await roleManager.FindByNameAsync(_adminRole) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(_adminRole));
            }
            if (await roleManager.FindByNameAsync(_userRole) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(_userRole));
            }

        }

        public static async Task InitializeAdmin(
            UserManager<User> userManager,
            IConfiguration configuration,
            IFactory<User, UserInitOptions> userFactory
        )
        {
            if (await userManager.FindByNameAsync(
                configuration["AdminAccess:Email"] ?? throw new NullReferenceException()) == null
            )
            {
                User admin = userFactory.Create(
                    new UserInitOptions(
                        configuration["AdminAccess:Name"] ?? throw new NullReferenceException(),
                        configuration["AdminAccess:Email"] ?? throw new NullReferenceException()
                    )
                );

                IdentityResult result = await userManager.CreateAsync(
                    admin, 
                    configuration["AdminAccess:Password"] ?? throw new NullReferenceException()
                );

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, _adminRole);
                }
            }
        }
    }
}

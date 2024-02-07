using Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Helpers
{
    public static class Initializator
    {
        private const string _adminRole = "admin";
        private const string _userRole = "user";
        private const string _adminEmail = "admin@admin.com";
        private const string _adminPassword = "Aa123456!";

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
            UserManager<User> userManager
        )
        {
            if (await userManager.FindByNameAsync(_adminEmail) == null)
            {
                User admin = new User 
                { 
                    Email = _adminEmail, 
                    UserName = _adminEmail 
                };

                IdentityResult result = await userManager.CreateAsync(admin, _adminPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, _adminRole);
                }
            }
        }
    }
}

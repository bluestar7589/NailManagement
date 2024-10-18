using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
#nullable disable
namespace NailManagement.Models
{
    public static class IdentityHelper
    {
        /// <summary>
        /// This property is used to define the role of Admin
        /// </summary>
        public const String Admin = "Admin";

        /// <summary>
        /// This property is used to define the role of User
        /// </summary>
        public const String User = "User";

        /// <summary>
        /// Create the initial roles for the application
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="roles">The arrays of role that needed for the application</param>
        /// <returns></returns>
        public static async Task CreateRoles(IServiceProvider serviceProvider, params String[] roles) { 
            RoleManager<IdentityRole> roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();
            foreach (String role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        /// <summary>
        /// Assign the role for the user's account
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="role">The role that want to assign to user's account </param>
        /// <returns></returns>
        public static async Task CreateUserRole(IServiceProvider serviceProvider, String role) { 
            UserManager<IdentityUser> userManager = serviceProvider.GetService<UserManager<IdentityUser>>();
            var adminUserConfig = serviceProvider.GetService<IOptions<AdminUserConfig>>().Value;
            // Here you could create a super user who will maintain the web app
            var powerUser = new IdentityUser
            {
                UserName = adminUserConfig.Username,
                Email = adminUserConfig.Email,
                EmailConfirmed = true // Set Emailconfirmed to true to avoid email confirmation when initial creating account
            };

            string userPassword = adminUserConfig.Password;
            var user = await userManager.FindByEmailAsync(adminUserConfig.Email);

            if (user == null)
            {
                var createPowerUser = await userManager.CreateAsync(powerUser, userPassword);
                if (createPowerUser.Succeeded)
                {
                    // Assign the new user the "Admin" role 
                    await userManager.AddToRoleAsync(powerUser, role);
                }
            }
            else
            {
                // Ensure the existing user's email is confirmed
                if (!user.EmailConfirmed)
                {
                    user.EmailConfirmed = true;
                    await userManager.UpdateAsync(user);
                }
            }
        }
    }
}

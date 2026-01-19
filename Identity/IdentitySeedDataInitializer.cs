using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Identity
{
    public class IdentitySeedDataInitializer
    {
        public static void SeedData
        (UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        private static void SeedRoles
            (RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync
                ("SuperAdmin").Result)
            {
                var role = new IdentityRole() { Name = "SuperAdmin" };

                var roleResult = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync
                ("Admin").Result)
            {
                var role = new IdentityRole { Name = "Admin" };

                var roleResult = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync
                    ("ServiceProvider").Result)
            {
                var role = new IdentityRole { Name = "ServiceProvider" };

                var roleResult = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync
                    ("CompanyOwner").Result)
            {
                var role = new IdentityRole { Name = "CompanyOwner" };

                var roleResult = roleManager.CreateAsync(role).Result;
            }
            if (!roleManager.RoleExistsAsync
                    ("User").Result)
            {
                var role = new IdentityRole { Name = "User" };

                var roleResult = roleManager.CreateAsync(role).Result;
            }
        }

        public static void SeedUsers
            (UserManager<AppUser> userManager)
        {
            if (userManager.FindByEmailAsync
                ("Superadmin@gmail.com").Result == null)
            {
                var user = new AppUser
                {
                    UserName = "Superadmin@gmail.com",
                    Email = "Superadmin@gmail.com",
                    Name = "Super Admin",
                    EmailConfirmed = true
                };

                var result = userManager.CreateAsync
                    (user, "Abc@123").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "SuperAdmin").Wait();

                    //List<Claim> claimList = new List<Claim>();
                    //claimList.Add(new Claim("Name", user.Name));
                    //userManager.AddClaimsAsync(user, claimList).Wait();
                    //var userClaims = userManager.GetClaimsAsync(user).Result;
                }
            }
        }
    }
}

using Crm.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

public static class SeedData
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        string[] roleNames = { "Admin", "User" };

        foreach (var roleName in roleNames)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                // Create the roles and seed them to the database
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var adminUser = await userManager.FindByEmailAsync("admin@example.com");

        if (adminUser == null)
        {
            var newAdmin = new ApplicationUser()
            {
                UserName = "admin@example.com",
                Email = "admin@example.com",
                FirstName = "Admin"
            };

            var result = await userManager.CreateAsync(newAdmin, "AdminPassword123!");
            if (result.Succeeded)
            {
                // Assign the "Admin" role to the new user
                await userManager.AddToRoleAsync(newAdmin, "Admin");
            }
        }
    }
}
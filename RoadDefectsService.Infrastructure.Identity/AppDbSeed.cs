using Microsoft.AspNetCore.Identity;
using RoadDefectsService.Core.Application.DTOs;
using RoadDefectsService.Core.Application.Interfaces.Services;
using RoadDefectsService.Core.Domain.Enums;
using RoadDefectsService.Infrastructure.Identity.Models;

namespace RoadDefectsService.Infrastructure.Identity
{
    internal static class AppDbSeed
    {
        public static void AddRoles(RoleManager<CustomRole> roleManager)
        {
            CustomRole[] roles =
            [
                new() { Name = Role.RoadInspector },
                new() { Name = Role.Operator },
                new() { Name = Role.Admin },
            ];

            List<CustomRole> existRoles = roleManager.Roles.ToList();
            foreach (var role in roles)
            {
                if (!existRoles.Any(existRole => existRole.Name == role.Name))
                {
                    roleManager.CreateAsync(role).Wait();
                }
            }
        }

        public static void AddAdmins(IUserService profileService, UserManager<CustomUser> userManager, List<CreateUserDTO> admins)
        {
            try
            {
                foreach (var admin in admins)
                {
                    CustomUser? customUser = userManager.FindByEmailAsync(admin.Email).GetAwaiter().GetResult();
                    if (customUser is null)
                    {
                        profileService.CreateAdminAsync(admin).Wait();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}

using Microsoft.AspNetCore.Identity;
using RoadDefectsService.Core.Application.DTOs.ContractorService;
using RoadDefectsService.Core.Application.DTOs.UserService;
using RoadDefectsService.Core.Application.Interfaces.Repositories;
using RoadDefectsService.Core.Application.Interfaces.Services;
using RoadDefectsService.Core.Domain.Enums;
using RoadDefectsService.Core.Domain.Models;

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

        public static void AddContractors(IContractorService contractorService, IContractorRepository contractorRepository)
        {
            CreateContractorDTO[] contractors =
            [
                new()
                {
                    Email = "contractor1@gmail.com",
                    ContractorFullName = "contractor1",
                    OrganizationName = "organization1",
                },
                new()
                {
                    Email = "contractor2@gmail.com",
                    ContractorFullName = "contractor2",
                    OrganizationName = "organization2",
                },
                new()
                {
                    Email = "contractor3@gmail.com",
                    ContractorFullName = "contractor3",
                    OrganizationName = "organization3",
                },
                new()
                {
                    Email = "contractor3@gmail.com",
                    ContractorFullName = "contractor3",
                    OrganizationName = "organization3",
                },
                new()
                {
                    Email = "contractor4@gmail.com",
                    ContractorFullName = "contractor4",
                    OrganizationName = "organization4",
                },
                new()
                {
                    Email = "contractor5@gmail.com",
                    ContractorFullName = "contractor5",
                    OrganizationName = "organization5",
                },
            ];
            try
            {
                foreach (var contractor in contractors)
                {
                    bool contractorExist = contractorRepository.AnyByEmailAsync(contractor.Email).GetAwaiter().GetResult();
                    if (!contractorExist)
                    {
                        contractorService.CreateContractorAsync(contractor).Wait();
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

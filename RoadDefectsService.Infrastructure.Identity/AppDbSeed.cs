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
                    CustomUser? customUser = userManager.FindByEmailAsync(admin.Email).Result;
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
                    bool contractorExist = contractorRepository.AnyByEmailAsync(contractor.Email).Result;
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

        public static void AddDefectTypes(IDefectTypeRepository defectTypeResponse)
        {
            DefectType[] defectTypes =
            [
                new()
                {
                    Id = Guid.Parse("53b0a60b-924a-41d7-a5e0-763344d846d3"),
                    Name = "Продольные трещины"
                },
                new()
                {
                    Id = Guid.Parse("1ffafcd5-557a-4365-995f-175ffb5f9cb9"),
                   Name = "Поперечные трещины"
                },
                new()
                {
                    Id = Guid.Parse("5e4ad8ad-8cf6-4159-8d61-c1ff162ed14c"),
                   Name = "Аллигаторные трещины"
                },
                new()
                {
                    Id = Guid.Parse("d29082f6-b230-45a3-806e-f377c8f503c8"),
                   Name = "Ямы"
                },
                new()
                {
                    Id = Guid.Parse("c636a0cd-d260-4546-a21a-1bb0ef2a935e"),
                   Name = "Просадки"
                },
                new()
                {
                    Id = Guid.Parse("e0de7729-d775-401e-aa29-edef541d14e0"),
                   Name = "Выбоины"
                },
                new()
                {
                    Id = Guid.Parse("ecfa2272-aa36-4f9e-b46c-d33652ec0fc1"),
                   Name = "Волны и бугры"
                },
                new()
                {
                    Id = Guid.Parse("fcd22fba-876f-4996-b588-863c0c74fafc"),
                   Name = "Колейность"
                },
                new()
                {
                    Id = Guid.Parse("c4f6a58c-61be-47e1-9a47-d04f0c0e3bb9"),
                   Name = "Износ верхнего слоя"
                },
                new()
                {
                    Id = Guid.Parse("df096cbd-c06e-4a8b-a90c-9d7d551bec77"),
                   Name = "Гладкость (полировка)"
                },
                new()
                {
                    Id = Guid.Parse("e5de55f7-bed2-4c12-b220-5e77c7d66900"),
                   Name = "Застой воды"
                },
                new()
                {
                    Id = Guid.Parse("5c161e82-2ec4-4b2f-b5d2-a34f89137938"),
                   Name = "Стирание разметки"
                },
                new()
                {
                    Id = Guid.Parse("dd01ccf7-910d-44e8-8612-33ffb39f0de0"),
                   Name = "Поврежденные или отсутствующие дорожные знаки"
                },
                new()
                {
                    Id = Guid.Parse("bbe33c4f-6aad-43fa-9b44-f56193ab6d8a"),
                   Name = "Разрушение бордюров"
                },
                new()
                {
                    Id = Guid.Parse("95a9e364-83b7-4365-83a1-163fb8111a8a"),
                   Name = "Эрозия обочин"
                }
            ];

            try
            {
                foreach (var defectType in defectTypes)
                {
                    bool defectTypeExist = defectTypeResponse.AnyByIdAsync(defectType.Id).Result;
                    if (!defectTypeExist)
                    {
                        defectTypeResponse.AddAsync(defectType).Wait();
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

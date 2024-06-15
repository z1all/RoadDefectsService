using Microsoft.AspNetCore.Identity;
using RoadDefectsService.Core.Domain.Enums;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Infrastructure.Identity.Seeds.Creators
{
    public class RolesCreator
    {
        private readonly RoleManager<CustomRole> _roleManager;

        public RolesCreator(RoleManager<CustomRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public void AddModels()
        {
            CustomRole[] roles =
            [
                new() { Name = Role.RoadInspector },
                new() { Name = Role.Operator },
                new() { Name = Role.Admin },
            ];

            List<CustomRole> existRoles = _roleManager.Roles.ToList();
            foreach (var role in roles)
            {
                if (!existRoles.Any(existRole => existRole.Name == role.Name))
                {
                    _roleManager.CreateAsync(role).Wait();
                }
            }
        }
    }
}

using Microsoft.AspNetCore.Identity;
using RoadDefectsService.Core.Application.DTOs;
using RoadDefectsService.Core.Application.Interfaces.Services;
using RoadDefectsService.Core.Application.Models;
using RoadDefectsService.Core.Domain.Enums;
using RoadDefectsService.Infrastructure.Identity.Mappers;
using RoadDefectsService.Infrastructure.Identity.Models;

namespace RoadDefectsService.Infrastructure.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<CustomUser> _userManager;
        private readonly RoleManager<CustomRole> _roleManager;

        public UserService(UserManager<CustomUser> userManager, RoleManager<CustomRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<ExecutionResult> CreateAdminAsync(CreateUserDTO user)
        {
            CustomUser newUser = new()
            {
                FullName = user.FullName,
                Email = user.Email,
                UserName = user.Email
            };

            IdentityResult creatingResult = await _userManager.CreateAsync(newUser, user.Password);
            if (!creatingResult.Succeeded)
            {
                creatingResult.ToExecutionResultError();
            }

            List<string> roles = [Role.Admin, Role.Operator];
            IdentityResult addingRoleResult = await _userManager.AddToRolesAsync(newUser, roles);
            if (!addingRoleResult.Succeeded)
            {
                return addingRoleResult.ToExecutionResultError();
            }

            //_logger.LogInformation($"A new admin has been created with id {user.Id}");

            return new(isSuccess: true);
        }
    }
}

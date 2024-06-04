using Microsoft.AspNetCore.Identity;
using RoadDefectsService.Core.Application.DTOs.UserService;
using RoadDefectsService.Core.Application.Interfaces.Repositories;
using RoadDefectsService.Core.Application.Interfaces.Services;
using RoadDefectsService.Core.Application.Models;
using RoadDefectsService.Core.Domain.Enums;
using RoadDefectsService.Core.Domain.Models;
using RoadDefectsService.Infrastructure.Identity.Mappers;

namespace RoadDefectsService.Infrastructure.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<CustomUser> _userManager;
        private readonly RoleManager<CustomRole> _roleManager;
        private readonly IRoadInspectorRepository _roadInspectorRepository;
        private readonly IOperatorRepository _operatorRepository;

        public UserService(
            UserManager<CustomUser> userManager, RoleManager<CustomRole> roleManager,
            IRoadInspectorRepository roadInspectorRepository, IOperatorRepository operatorRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _roadInspectorRepository = roadInspectorRepository;
            _operatorRepository = operatorRepository;
        }

        public Task<ExecutionResult<List<UserInfoDTO>>> GetUsersAsync(UserFilterDTO userFilter, bool showOperators)
        {
            throw new NotImplementedException();
        }

        public Task<ExecutionResult> EditUserAsync(EditUserDTO editUser, Guid userId, bool editOperator)
        {
            throw new NotImplementedException();
        }

        public Task<ExecutionResult> DeleteUserAsync(Guid userId, bool deleteOperator)
        {
            throw new NotImplementedException();
        }

        public async Task<ExecutionResult> CreateAdminAsync(CreateUserDTO user)
        {
            CustomUser newUser = new()
            {
                HighestRole = Role.Admin,
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

        public Task<ExecutionResult> CreateRoadInspectorAsync(CreateUserDTO createRoadInspector)
        {
            throw new NotImplementedException();
        }

        public Task<ExecutionResult> CreateOperatorAsync(CreateUserDTO createOperator)
        {
            throw new NotImplementedException();
        }
    }
}

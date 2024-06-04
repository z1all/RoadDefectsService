using Microsoft.AspNetCore.Identity;
using RoadDefectsService.Core.Application.DTOs.UserService;
using RoadDefectsService.Core.Application.Interfaces.Repositories;
using RoadDefectsService.Core.Application.Interfaces.Services;
using RoadDefectsService.Core.Application.Mappers;
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
        private readonly IUserRepository _userRepository;

        public UserService(
            UserManager<CustomUser> userManager, RoleManager<CustomRole> roleManager,
            IRoadInspectorRepository roadInspectorRepository, IOperatorRepository operatorRepository,
            IUserRepository userRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _roadInspectorRepository = roadInspectorRepository;
            _operatorRepository = operatorRepository;
            _userRepository = userRepository;
        }

        public async Task<ExecutionResult<UserPagedDTO>> GetUsersAsync(UserFilterDTO userFilter, bool showOperators)
        {
            if (userFilter.Page < 1)
            {
                return new(StatusCodeExecutionResult.BadRequest, keyError: "InvalidPageError", error: "Number of page can't be less than 1.");
            }

            int countUsers = await _userRepository.CountByFilter(userFilter, showOperators);
            int countPage = countUsers == 0 ? 1 : (countUsers + userFilter.Size - 1) / userFilter.Size;
            if (userFilter.Page > countPage)
            {
                return new(StatusCodeExecutionResult.BadRequest, keyError: "InvalidPageError", error: $"Number of page can be from 1 to {countPage}.");
            }

            List<CustomUser> users = await _userRepository.GetAllByFilter(userFilter, showOperators);
            return new UserPagedDTO()
            {
                Users = users.ToToUserInfoDTOList(),
                Pagination = new()
                {
                    Count = countPage,
                    Current = userFilter.Page,
                    Size = userFilter.Size,
                },
            };
        }

        public async Task<ExecutionResult> EditUserAsync(EditUserDTO editUser, Guid userId, bool editOperator)
        {
            CustomUser? user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null)
            {
                return new(StatusCodeExecutionResult.NotFound, "UserNotFound", $"User with id {userId} not found!");
            }

            if (user.HighestRole == Role.Admin)
            {
                return new(StatusCodeExecutionResult.Forbid, "EditAdminFail", $"You cannot edit a user with the administrator role!");
            }

            if (!editOperator && user.HighestRole == Role.Operator)
            {
                return new(StatusCodeExecutionResult.Forbid, "EditOperatorFail", $"You cannot edit a user with the operator role!");
            }

            user.FullName = editUser.FullName;

            await _userManager.UpdateAsync(user);

            return new(isSuccess: true);
        }

        public async Task<ExecutionResult> DeleteUserAsync(Guid userId, bool deleteOperator)
        {
            CustomUser? user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null)
            {
                return new(StatusCodeExecutionResult.NotFound, "UserNotFound", $"User with id {userId} not found!");
            }

            if (user.HighestRole == Role.Admin)
            {
                return new(StatusCodeExecutionResult.Forbid, "DeleteAdminFail", $"You cannot delete a user with the administrator role!");
            }

            if (!deleteOperator && user.HighestRole == Role.Operator)
            {
                return new(StatusCodeExecutionResult.Forbid, "DeleteOperatorFail", $"You cannot delete a user with the operator role!");
            }

            await _userManager.DeleteAsync(user);

            return new(isSuccess: true);
        }

        public Task<ExecutionResult> CreateAdminAsync(CreateUserDTO createAdmin)
        {
            return CreateOperatorAsync(createAdmin, [Role.Admin, Role.Operator]);
        }

        public Task<ExecutionResult> CreateOperatorAsync(CreateUserDTO createOperator)
        {
            return CreateOperatorAsync(createOperator, [Role.Operator]);
        }

        private async Task<ExecutionResult> CreateOperatorAsync(CreateUserDTO createOperator, List<string> roles)
        {
            ExecutionResult<CustomUser> creatingResult = await CreateUserAsync(createOperator, roles);
            if (!creatingResult.IsSuccess)
            {
                return creatingResult;
            }
            CustomUser user = creatingResult.Result!;

            Operator @operator = new()
            {
                Id = user.Id,
                User = user,
            };
            await _operatorRepository.AddAsync(@operator);

            return new(isSuccess: true);
        }

        public async Task<ExecutionResult> CreateRoadInspectorAsync(CreateUserDTO createRoadInspector)
        {
            ExecutionResult<CustomUser> creatingResult = await CreateUserAsync(createRoadInspector, [Role.RoadInspector]);
            if (!creatingResult.IsSuccess)
            {
                return creatingResult;
            }
            CustomUser user = creatingResult.Result!;

            RoadInspector roadInspector = new()
            {
                Id = user.Id,
                User = user,
            };
            await _roadInspectorRepository.AddAsync(roadInspector);

            return new(isSuccess: true);
        }

        /// <summary>
        /// Наивысшая роль указывается первой в roles
        /// </summary>
        private async Task<ExecutionResult<CustomUser>> CreateUserAsync(CreateUserDTO user, List<string> roles)
        {
            CustomUser newUser = new()
            {
                HighestRole = roles[0],
                FullName = user.FullName,
                Email = user.Email,
                UserName = user.Email
            };

            IdentityResult creatingResult = await _userManager.CreateAsync(newUser, user.Password);
            if (!creatingResult.Succeeded)
            {
                return creatingResult.ToExecutionResultError<CustomUser>();
            }

            IdentityResult addingRoleResult = await _userManager.AddToRolesAsync(newUser, roles);
            if (!addingRoleResult.Succeeded)
            {
                return addingRoleResult.ToExecutionResultError<CustomUser>();
            }

            return newUser;
        }
    }
}

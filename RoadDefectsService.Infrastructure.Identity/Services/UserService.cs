using AutoMapper;
using Microsoft.AspNetCore.Identity;
using RoadDefectsService.Core.Application.DTOs.Common;
using RoadDefectsService.Core.Application.DTOs.UserService;
using RoadDefectsService.Core.Application.Helpers;
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
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(
            UserManager<CustomUser> userManager, RoleManager<CustomRole> roleManager,
            IRoadInspectorRepository roadInspectorRepository, IOperatorRepository operatorRepository,
            IUserRepository userRepository, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _roadInspectorRepository = roadInspectorRepository;
            _operatorRepository = operatorRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<ExecutionResult<UserPagedDTO>> GetUsersAsync(UserFilterDTO userFilter, bool showOperators)
        {
            return await FiltrationHelper
               .FilterAsync<UserFilterDTO, CustomUser, UserInfoDTO, UserPagedDTO>(
                   userFilter,
                   (filter) => _userRepository.CountByFilterAsync(filter, showOperators),
                   (filter) => _userRepository.GetAllByFilterAsync(filter, showOperators),
                   (users) => _mapper.Map<List<UserInfoDTO>>(users)
               );
        }

        public async Task<ExecutionResult<UserInfoDTO>> GetUserAsync(Guid userId, bool showAdmins)
        {
            CustomUser? user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null || user.IsDeleted)
            {
                return new(StatusCodeExecutionResult.NotFound, "UserNotFound", $"User with id {userId} not found!");
            }

            if (!showAdmins && user.HighestRole == Role.Admin)
            {
                return new(StatusCodeExecutionResult.Forbid, "GetAdminFail", $"You cannot get a user with the admin role!");
            }

            return _mapper.Map<UserInfoDTO>(user);
        }

        public async Task<ExecutionResult> EditUserAsync(EditUserDTO editUser, Guid userId, bool editOperator)
        {
            CustomUser? user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null || user.IsDeleted)
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

            return ExecutionResult.SucceededResult;
        }

        public async Task<ExecutionResult> DeleteUserAsync(Guid userId, bool deleteOperator)
        {
            CustomUser? user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null || user.IsDeleted)
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

            user.IsDeleted = true;
            await _userManager.UpdateAsync(user);

            IList<string> userRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, userRoles);

            return ExecutionResult.SucceededResult;
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
            ExecutionResult<(CustomUser user, bool isRestored)> creatingResult = await CreateUserAsync(createOperator, roles);
            if (!creatingResult.TryGetResult(out var result))
            {
                return creatingResult;
            }

            if (!result.isRestored)
            {
                OperatorEntity @operator = new()
                {
                    Id = result.user.Id,
                    User = result.user,
                };
                await _operatorRepository.AddAsync(@operator);
            }

            return ExecutionResult.SucceededResult;
        }

        public async Task<ExecutionResult> CreateRoadInspectorAsync(CreateUserDTO createRoadInspector)
        {
            ExecutionResult<(CustomUser user, bool isRestored)> creatingResult = await CreateUserAsync(createRoadInspector, [Role.RoadInspector]);
            if (!creatingResult.TryGetResult(out var result))
            {
                return creatingResult;
            }

            if (!result.isRestored)
            {
                RoadInspectorEntity roadInspector = new()
                {
                    Id = result.user.Id,
                    User = result.user,
                };
                await _roadInspectorRepository.AddAsync(roadInspector);
            }

            return ExecutionResult.SucceededResult;
        }

        /// <summary>
        /// Наивысшая роль указывается первой в roles
        /// </summary>
        private async Task<ExecutionResult<(CustomUser, bool isRestored)>> CreateUserAsync(CreateUserDTO createUser, List<string> roles)
        {
            CustomUser newUser = new()
            {
                HighestRole = roles[0],
                FullName = createUser.FullName,
                Email = createUser.Email,
                UserName = createUser.Email
            };

            bool isRestored = false;
            IdentityResult creatingResult = await _userManager.CreateAsync(newUser, createUser.Password);
            if (!creatingResult.Succeeded)
            {
                CustomUser? existUser = await _userManager.FindByEmailAsync(createUser.Email);
                if (existUser is null || !existUser.IsDeleted)
                {
                    return creatingResult.ToExecutionResultError<(CustomUser, bool)>();
                }

                existUser.FullName = createUser.FullName;
                existUser.HighestRole = roles[0];
                existUser.IsDeleted = false;
                
                await _userManager.UpdateAsync(existUser);
                
                newUser = existUser;
                isRestored = true;
            }

            IdentityResult addingRoleResult = await _userManager.AddToRolesAsync(newUser, roles);
            if (!addingRoleResult.Succeeded)
            {
                return addingRoleResult.ToExecutionResultError<(CustomUser, bool)>();
            }

            return (newUser, isRestored);
        }
    }
}

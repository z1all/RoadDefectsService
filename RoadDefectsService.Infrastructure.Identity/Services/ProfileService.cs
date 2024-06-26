﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using RoadDefectsService.Core.Application.DTOs.Common;
using RoadDefectsService.Core.Application.DTOs.ProfileService;
using RoadDefectsService.Core.Application.Interfaces.Services;
using RoadDefectsService.Core.Application.Models;
using RoadDefectsService.Core.Domain.Models;
using RoadDefectsService.Infrastructure.Identity.Mappers;

namespace RoadDefectsService.Infrastructure.Identity.Services
{
    public class ProfileService : IProfileService
    {
        private readonly UserManager<CustomUser> _userManager;
        private readonly IMapper _mapper;

        public ProfileService(UserManager<CustomUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<ExecutionResult<UserInfoDTO>> GetProfileInfoAsync(Guid userId)
        {
            CustomUser? user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null || user.IsDeleted)
            {
                return new(StatusCodeExecutionResult.NotFound, "UserNotFound", $"User with id {userId} not found!");
            }

            return _mapper.Map<UserInfoDTO>(user);
        }

        public async Task<ExecutionResult> EditProfileInfoAsync(EditProfileDTO editProfile, Guid userId)
        {
            CustomUser? user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null || user.IsDeleted)
            {
                return new(StatusCodeExecutionResult.NotFound, "UserNotFound", $"User with id {userId} not found!");
            }

            user.FullName = editProfile.FullName;
            user.Email = editProfile.Email;

            IdentityResult editProfileResult = await _userManager.UpdateAsync(user);
            if (!editProfileResult.Succeeded)
            {
                return editProfileResult.ToExecutionResultError();
            }

            return ExecutionResult.SucceededResult;
        }

        public async Task<ExecutionResult> ChangePasswordAsync(ChangePasswordDTO changePassword, Guid userId)
        {
            CustomUser? user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null || user.IsDeleted)
            {
                return new(StatusCodeExecutionResult.NotFound, "UserNotFound", $"User with id {userId} not found!");
            }

            IdentityResult changePasswordResult = await _userManager.ChangePasswordAsync(user, changePassword.CurrentPassword, changePassword.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                return changePasswordResult.ToExecutionResultError();
            }

            return ExecutionResult.SucceededResult;   
        }
    }
}

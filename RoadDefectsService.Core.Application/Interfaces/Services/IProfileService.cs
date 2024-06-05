using RoadDefectsService.Core.Application.DTOs.Common;
using RoadDefectsService.Core.Application.DTOs.ProfileService;
using RoadDefectsService.Core.Application.Models;

namespace RoadDefectsService.Core.Application.Interfaces.Services
{
    public interface IProfileService
    {
        Task<ExecutionResult<UserInfoDTO>> GetProfileInfoAsync(Guid userId);
        Task<ExecutionResult> EditProfileInfoAsync(EditProfileDTO editProfile, Guid userId);
        Task<ExecutionResult> ChangePasswordAsync(ChangePasswordDTO changePassword, Guid userId);
    }
}

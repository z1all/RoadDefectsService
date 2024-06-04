using RoadDefectsService.Core.Application.DTOs.AccessTokenService;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Infrastructure.Identity.Mappers
{
    public static class UserMapper
    {
        public static TokenUserInfoDTO ToUserDTO(this CustomUser user)
        {
            return new()
            {
                Id = user.Id,
                Email = user.Email!,
                FullName = user.FullName,
            };
        }
    }
}

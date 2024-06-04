using RoadDefectsService.Core.Application.DTOs.UserService;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.Mappers
{
    public static class UserMapper
    {
        public static UserInfoDTO ToUserInfoDTO(this CustomUser user)
        {
            return new()
            {
                Id = user.Id,
                Email = user.Email!,
                FullName = user.FullName,
                HighestRole = user.HighestRole
            };
        }

        public static List<UserInfoDTO> ToToUserInfoDTOList(this List<CustomUser> users)
        {
            return users.Select(ToUserInfoDTO).ToList();
        }
    }
}

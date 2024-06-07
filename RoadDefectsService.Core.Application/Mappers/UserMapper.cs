using RoadDefectsService.Core.Application.DTOs.Common;
using RoadDefectsService.Core.Application.DTOs.TaskService;
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

        public static RoadInspectorDTO ToRoadInspectorDTO(this RoadInspector roadInspector)
        {
            return new()
            {
                Id = roadInspector.Id,
                Email = roadInspector.User.Email!,
                FullName = roadInspector.User.FullName,
            };
        }
    }
}

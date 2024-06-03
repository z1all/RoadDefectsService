using RoadDefectsService.Core.Application.DTOs;
using RoadDefectsService.Infrastructure.Identity.Models;

namespace RoadDefectsService.Infrastructure.Identity.Mappers
{
    public static class UserMapper
    {
        public static UserDTO ToUserDTO(this CustomUser user)
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

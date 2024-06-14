using RoadDefectsService.Core.Application.Enums;

namespace RoadDefectsService.Core.Application.DTOs.Common
{
    public class UserInfoDTO
    {
        public required Guid Id { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required RoleEnum HighestRole { get; set; }
    }
}

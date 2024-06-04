using RoadDefectsService.Core.Application.Enums;

namespace RoadDefectsService.Core.Application.DTOs.UserService
{
    public class UserFilterDTO
    {
        public string? UserFullName { get; set; }
        public RoleFilter UserRole { get; set; } = RoleFilter.None;

        public int Page { get; set; } = 1;
        public int Size { get; set; } = 10;
    }
}

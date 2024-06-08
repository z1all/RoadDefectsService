using RoadDefectsService.Core.Application.DTOs.Common;
using RoadDefectsService.Core.Application.Enums;

namespace RoadDefectsService.Core.Application.DTOs.UserService
{
    public class UserFilterDTO : BaseFilterDTO
    {
        public string? UserFullName { get; set; }
        public RoleFilter UserRole { get; set; } = RoleFilter.None;
    }
}

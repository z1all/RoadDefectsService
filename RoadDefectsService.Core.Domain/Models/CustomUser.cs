using Microsoft.AspNetCore.Identity;
using RoadDefectsService.Core.Domain.Models.Base;

namespace RoadDefectsService.Core.Domain.Models
{
    public class CustomUser : IdentityUser<Guid>, ISoftDeleteBaseEntity
    {
        public bool IsDeleted { get; set; } = false;
        public required string FullName { get; set; }
        public required string HighestRole { get; set; }
    }
}

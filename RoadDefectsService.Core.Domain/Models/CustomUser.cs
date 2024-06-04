using Microsoft.AspNetCore.Identity;

namespace RoadDefectsService.Core.Domain.Models
{
    public class CustomUser : IdentityUser<Guid>
    {
        public required string FullName { get; set; }
        public required string HighestRole { get; set; }
    }
}

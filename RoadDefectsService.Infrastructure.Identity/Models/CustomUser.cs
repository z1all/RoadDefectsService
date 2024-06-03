using Microsoft.AspNetCore.Identity;

namespace RoadDefectsService.Infrastructure.Identity.Models
{
    public class CustomUser : IdentityUser<Guid>
    {
        public required string FullName { get; set; }
    }
}

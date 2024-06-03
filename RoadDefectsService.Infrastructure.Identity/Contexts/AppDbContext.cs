using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RoadDefectsService.Infrastructure.Identity.Models;

namespace RoadDefectsService.Infrastructure.Identity.Contexts
{
    public class AppDbContext : IdentityDbContext<CustomUser, CustomRole, Guid>
    {
        public AppDbContext(DbContextOptions options) : base(options) { }
    }
}

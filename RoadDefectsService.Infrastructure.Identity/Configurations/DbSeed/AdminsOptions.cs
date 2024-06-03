using RoadDefectsService.Core.Application.DTOs;

namespace RoadDefectsService.Infrastructure.Identity.Configurations.DbSeed
{
    public class AdminsOptions
    {
        public required List<CreateUserDTO> CreateAdmins { get; set; }
    }
}

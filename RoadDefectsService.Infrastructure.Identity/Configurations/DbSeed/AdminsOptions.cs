using RoadDefectsService.Core.Application.DTOs.UserService;

namespace RoadDefectsService.Infrastructure.Identity.Configurations.DbSeed
{
    public class AdminsOptions
    {
        public required List<CreateUserDTO> CreateAdmins { get; set; }
    }
}

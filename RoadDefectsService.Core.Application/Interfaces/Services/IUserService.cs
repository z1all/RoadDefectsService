using RoadDefectsService.Core.Application.DTOs;

namespace RoadDefectsService.Core.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task CreateAdminAsync(CreateUserDTO user);
    }
}

using RoadDefectsService.Core.Application.DTOs;
using RoadDefectsService.Core.Application.Models;

namespace RoadDefectsService.Core.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<ExecutionResult> CreateAdminAsync(CreateUserDTO user);
    }
}

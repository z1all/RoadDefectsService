using RoadDefectsService.Core.Application.DTOs.UserService;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.Interfaces.Repositories
{
    public interface IUserRepository 
    {
        Task<List<CustomUser>> GetAllByFilterAsync(UserFilterDTO userFilter, bool showOperators, bool showDeleted = false);
        Task<int> CountByFilterAsync(UserFilterDTO userFilter, bool showOperators, bool showDeleted = false);
        Task<CustomUser?> GetByIdAsync(Guid id);
    }
}

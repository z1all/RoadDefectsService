using RoadDefectsService.Core.Application.DTOs.UserService;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.Interfaces.Repositories
{
    public interface IUserRepository 
    {
        Task<List<CustomUser>> GetAllByFilterAsync(UserFilterDTO userFilter, bool showOperators);
        Task<int> CountByFilterAsync(UserFilterDTO userFilter, bool showOperators);
    }
}

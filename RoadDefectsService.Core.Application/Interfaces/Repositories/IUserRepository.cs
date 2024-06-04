using RoadDefectsService.Core.Application.DTOs.UserService;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.Interfaces.Repositories
{
    public interface IUserRepository 
    {
        Task<List<CustomUser>> GetAllByFilter(UserFilterDTO userFilter, bool showOperators);
        Task<int> CountByFilter(UserFilterDTO userFilter, bool showOperators);
    }
}

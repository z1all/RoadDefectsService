using RoadDefectsService.Core.Application.Interfaces.Repositories.Base;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.Interfaces.Repositories
{
    public interface IFixationWorkRepository : IBaseWithBaseEntityRepository<FixationWorkEntity>
    {
        Task<FixationWorkEntity?> GetByIdWithTaskAsync(Guid id);
        Task<FixationWorkEntity?> GetByIdWithTaskWithPrevTaskWithFixationDefectAsync(Guid id);
        Task<FixationWorkEntity?> GetByIdWithTaskAndPhotosAsync(Guid id);
        Task<FixationWorkEntity?> GetByIdWithPhotosAndTaskWithPrevTaskAsync(Guid id);
    }
}

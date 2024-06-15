using RoadDefectsService.Core.Application.Interfaces.Repositories.Base;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.Interfaces.Repositories
{
    public interface IFixationWorkRepository : IBaseWithBaseEntityRepository<FixationWork>
    {
        Task<FixationWork?> GetByIdWithTaskAsync(Guid id);
        Task<FixationWork?> GetByIdWithTaskWithPrevTaskWithFixationDefectAsync(Guid id);
        Task<FixationWork?> GetByIdWithTaskAndPhotosAsync(Guid id);
        Task<FixationWork?> GetByIdWithPhotosAndTaskWithPrevTaskAsync(Guid id);
    }
}

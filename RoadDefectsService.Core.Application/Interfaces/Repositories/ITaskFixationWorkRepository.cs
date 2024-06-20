using RoadDefectsService.Core.Application.Interfaces.Repositories.Base;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.Interfaces.Repositories
{
    public interface ITaskFixationWorkRepository : IBaseWithBaseEntityRepository<TaskFixationWorkEntity>
    {
        Task<TaskFixationWorkEntity?> GetByIdWithInspectorAndPrevTaskAndNextTaskAndFixationsWithPhotosAndDefectTypeAsync(Guid id);
        Task<TaskFixationWorkEntity?> GetByIdWithPrevTaskWithFixationDefectAsync(Guid id);
        Task<bool> AnyWithPrevTaskId(Guid prevTaskId);
    }
}

using RoadDefectsService.Core.Application.Interfaces.Repositories.Base;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.Interfaces.Repositories
{
    public interface ITaskFixationWorkRepository : IBaseWithBaseEntityRepository<TaskFixationWork>
    {
        Task<TaskFixationWork?> GetByIdWithInspectorAndPrevTaskAndNextTaskAndFixationsWithPhotosAndDefectTypeAsync(Guid id);
        Task<TaskFixationWork?> GetByIdWithPrevTaskWithFixationDefectAsync(Guid id);
        Task<bool> AnyWithPrevTaskId(Guid prevTaskId);
    }
}

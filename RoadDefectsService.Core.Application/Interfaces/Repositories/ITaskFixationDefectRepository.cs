using RoadDefectsService.Core.Application.Interfaces.Repositories.Base;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.Interfaces.Repositories
{
    public interface ITaskFixationDefectRepository : IBaseWithBaseEntityRepository<TaskFixationDefectEntity>
    {
        Task<TaskFixationDefectEntity?> GetByIdWithInspectorAndDefectWithPhotosAndDefectTypeAsync(Guid id);
        Task<TaskFixationDefectEntity?> GetByIdWithInspectorAndNextTaskAndDefectWithPhotosAndDefectTypeAsync(Guid id);
    }
}

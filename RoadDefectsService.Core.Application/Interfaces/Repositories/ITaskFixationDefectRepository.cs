using RoadDefectsService.Core.Application.Interfaces.Repositories.Base;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.Interfaces.Repositories
{
    public interface ITaskFixationDefectRepository : IBaseWithBaseEntityRepository<TaskFixationDefect>
    {
        Task<TaskFixationDefect?> GetByIdWithInspectorAndDefectWithPhotosAndDefectTypeAsync(Guid id);
    }
}

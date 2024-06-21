using RoadDefectsService.Core.Application.Interfaces.Repositories.Base;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.Interfaces.Repositories
{
    public interface IFixationDefectRepository : IBaseWithBaseEntityRepository<FixationDefectEntity>
    {
        Task<FixationDefectEntity?> GetByIdWithTaskAsync(Guid id);
        Task<FixationDefectEntity?> GetByIdWithTaskAndPhotosAndDefectTypeAsync(Guid id);
        Task<FixationDefectEntity?> GetByIdWithTaskAndPhotosAndDefectTypeAndAssignmentAsync(Guid id);
    }
}

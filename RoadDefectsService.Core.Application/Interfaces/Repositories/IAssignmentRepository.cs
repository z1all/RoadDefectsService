using RoadDefectsService.Core.Application.DTOs.AssignmentService;
using RoadDefectsService.Core.Application.Interfaces.Repositories.Base;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.Interfaces.Repositories
{
    public interface IAssignmentRepository : 
        IBaseWithBaseEntityRepository<AssignmentEntity>, 
        IFilterableRepository<AssignmentFilterDTO, AssignmentEntity>
    {
        Task<AssignmentEntity?> GetByIdWithContractorAndFixationDefectWithDefectTypeAndPhotosAsync(Guid id);
        Task<bool> AnyByFixationDefectIdAsync(Guid fixationDefectId);
        Task<AssignmentEntity?> GetByFixationDefectIdWithAllNestingAsync(Guid fixationDefectId);
    }
}

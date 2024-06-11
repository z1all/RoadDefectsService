using RoadDefectsService.Core.Application.DTOs.AssignmentService;
using RoadDefectsService.Core.Application.Interfaces.Repositories.Base;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.Interfaces.Repositories
{
    public interface IAssignmentRepository : 
        IBaseWithBaseEntityRepository<Assignment>, 
        IFilterableRepository<AssignmentFilterDTO, Assignment>
    {
        Task<Assignment?> GetByIdWithContractorAndFixationDefectWithDefectTypeAndPhotosAsync(Guid id);
    }
}

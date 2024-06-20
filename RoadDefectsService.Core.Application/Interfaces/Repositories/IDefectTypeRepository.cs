using RoadDefectsService.Core.Application.DTOs.DefectTypeService;
using RoadDefectsService.Core.Application.Interfaces.Repositories.Base;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.Interfaces.Repositories
{
    public interface IDefectTypeRepository : IBaseWithBaseEntityRepository<DefectTypeEntity>
    {
        Task<List<DefectTypeEntity>> GetAllByFilterAsync(DefectTypeFilterDTO defectTypeFilter);
    }
}

using RoadDefectsService.Core.Application.DTOs.DefectTypeService;
using RoadDefectsService.Core.Application.Interfaces.Repositories.Base;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.Interfaces.Repositories
{
    public interface IDefectTypeRepository : IBaseWithBaseEntityRepository<DefectType>
    {
        Task<List<DefectType>> GetAllByFilterAsync(DefectTypeFilterDTO defectTypeFilter);
    }
}

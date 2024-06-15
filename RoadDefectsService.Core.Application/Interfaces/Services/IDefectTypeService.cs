using RoadDefectsService.Core.Application.DTOs.DefectTypeService;
using RoadDefectsService.Core.Application.DTOs.FixationService;
using RoadDefectsService.Core.Application.Models;

namespace RoadDefectsService.Core.Application.Interfaces.Services
{
    public interface IDefectTypeService
    {
        Task<ExecutionResult<List<DefectTypeDTO>>> GetDefectTypesAsync(DefectTypeFilterDTO defectTypeFilter);
    }
}

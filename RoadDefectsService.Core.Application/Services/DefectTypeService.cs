using RoadDefectsService.Core.Application.DTOs.DefectTypeService;
using RoadDefectsService.Core.Application.DTOs.FixationService;
using RoadDefectsService.Core.Application.Interfaces.Repositories;
using RoadDefectsService.Core.Application.Interfaces.Services;
using RoadDefectsService.Core.Application.Mappers;
using RoadDefectsService.Core.Application.Models;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.Services
{
    public class DefectTypeService : IDefectTypeService
    {
        private readonly IDefectTypeRepository _defectTypeRepository;

        public DefectTypeService(IDefectTypeRepository defectTypeRepository)
        {
            _defectTypeRepository = defectTypeRepository;
        }

        public async Task<ExecutionResult<List<DefectTypeDTO>>> GetDefectTypesAsync(DefectTypeFilterDTO defectTypeFilter)
        {
            List<DefectType> defectTypes = await _defectTypeRepository.GetAllByFilterAsync(defectTypeFilter);

            return defectTypes.ToDefectTypeDTOList();
        }
    }
}

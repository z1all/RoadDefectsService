using AutoMapper;
using RoadDefectsService.Core.Application.DTOs.DefectTypeService;
using RoadDefectsService.Core.Application.DTOs.FixationService;
using RoadDefectsService.Core.Application.Interfaces.Repositories;
using RoadDefectsService.Core.Application.Interfaces.Services;
using RoadDefectsService.Core.Application.Models;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.Services
{
    public class DefectTypeService : IDefectTypeService
    {
        private readonly IDefectTypeRepository _defectTypeRepository;
        private readonly IMapper _mapper;

        public DefectTypeService(IDefectTypeRepository defectTypeRepository, IMapper mapper)
        {
            _defectTypeRepository = defectTypeRepository;
            _mapper = mapper;
        }

        public async Task<ExecutionResult<List<DefectTypeDTO>>> GetDefectTypesAsync(DefectTypeFilterDTO defectTypeFilter)
        {
            List<DefectTypeEntity> defectTypes = await _defectTypeRepository.GetAllByFilterAsync(defectTypeFilter);

            return _mapper.Map<List<DefectTypeDTO>>(defectTypes);
        }
    }
}

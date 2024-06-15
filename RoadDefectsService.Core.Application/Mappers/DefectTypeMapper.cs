using RoadDefectsService.Core.Application.DTOs.FixationService;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.Mappers
{
    public static class DefectTypeMapper
    {
        public static DefectTypeDTO ToDefectTypeDTO(this DefectType defectType)
        {
            return new()
            {
                Id = defectType.Id,
                Name = defectType.Name,
            };
        }

        public static List<DefectTypeDTO> ToDefectTypeDTOList(this List<DefectType> defectTypes)
        {
            return defectTypes.Select(ToDefectTypeDTO).ToList();
        }
    }
}

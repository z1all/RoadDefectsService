using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.DTOs.FixationService
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
    }
}

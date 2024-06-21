using RoadDefectsService.Core.Application.CQRS.Contractor.DTOs;
using RoadDefectsService.Core.Application.CQRS.DefectType.DTOs;

namespace RoadDefectsService.Core.Application.DTOs.FixationService
{
    public class FixationDefectDTO
    {
        public required Guid Id { get; set; }
        public required DateTime RecordedDateTime { get; set; }
        public required bool IsEliminated { get; set; }
        public required string Address { get; set; }
        public required double? DamagedCanvasSquareMeter { get; set; }
        public required DefectTypeDTO? DefectType { get; set; }
        public required List<PhotoInfoDTO> Photos { get; set; }
        public required ContractorDTO? Contractor { get; set; }
    }
}

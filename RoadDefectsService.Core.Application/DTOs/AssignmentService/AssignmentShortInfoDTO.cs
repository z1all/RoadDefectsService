using RoadDefectsService.Core.Application.DTOs.ContractorService;

namespace RoadDefectsService.Core.Application.DTOs.AssignmentService
{
    public class AssignmentShortInfoDTO
    {
        public required Guid Id { get; set; }
        public required ContractorDTO Contractor { get; set; }
        public required FixationDefectShortInfoDTO FixationDefect { get; set; }
    }
}

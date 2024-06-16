using RoadDefectsService.Core.Application.DTOs.ContractorService;
using RoadDefectsService.Core.Application.DTOs.FixationService;

namespace RoadDefectsService.Core.Application.DTOs.AssignmentService
{
    public class AssignmentDTO
    {
        public required Guid Id { get; set; }
        public required DateTime CreatedDateTime { get; set; }
        public required DateOnly DeadlineDateOnly { get; set; }
        public required ContractorDTO Contractor { get; set; }
        public required FixationDefectDTO FixationDefect { get; set; }
    }
}

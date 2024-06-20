using RoadDefectsService.Core.Application.CQRS.Contractor.DTOs;

namespace RoadDefectsService.Core.Application.DTOs.AssignmentService
{
    public class AssignmentShortInfoDTO
    {
        public required Guid Id { get; set; }
        public required DateTime CreatedDateTime { get; set; }
        public required DateOnly DeadlineDateOnly { get; set; }
        public required ContractorDTO Contractor { get; set; }
        public required FixationDefectShortInfoDTO FixationDefect { get; set; }
    }
}

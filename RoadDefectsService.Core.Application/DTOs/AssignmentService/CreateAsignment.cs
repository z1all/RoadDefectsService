namespace RoadDefectsService.Core.Application.DTOs.AssignmentService
{
    public class CreateAssignmentDTO
    {
        public required DateOnly DeadlineDateOnly { get; set; }
        public required Guid ContractorId { get; set; }
        public required Guid FixationDefectId { get; set; }
    }
}

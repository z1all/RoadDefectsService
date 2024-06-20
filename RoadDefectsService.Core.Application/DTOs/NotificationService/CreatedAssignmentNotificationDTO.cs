using RoadDefectsService.Core.Application.CQRS.Contractor.DTOs;

namespace RoadDefectsService.Core.Application.DTOs.NotificationService
{
    public class CreatedAssignmentNotificationDTO
    {
        public required Guid AssignmentId { get; set; }
        public required DateOnly DeadlineDateOnly { get; set; }
        public required ContractorDTO Contractor { get; set; }
        public required FixationDefectWithPhotoShortInfoDTO FixationDefect { get; set; }
    }
}

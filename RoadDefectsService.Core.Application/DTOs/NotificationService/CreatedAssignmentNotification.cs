using RoadDefectsService.Core.Application.DTOs.ContractorService;
using RoadDefectsService.Core.Application.DTOs.FixationService;

namespace RoadDefectsService.Core.Application.DTOs.NotificationService
{
    public class CreatedAssignmentNotification
    {
        public required Guid AssignmentId { get; set; }
        public required ContractorDTO Contractor { get; set; }
        public required FixationDefectDTO FixationDefect { get; set; }
    }
}

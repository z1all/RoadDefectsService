using RoadDefectsService.Core.Application.CQRS.Contractor.DTOs;
using RoadDefectsService.Core.Application.DTOs.Common;
using RoadDefectsService.Core.Application.DTOs.FixationService;

namespace RoadDefectsService.Core.Application.DTOs.MetricsService
{
    public class GenerateWorkReportDTO
    {
        public required Guid AssignmentId { get; set; }
        public required DateTime CreatedAssignmentDateTime { get; set; }
        public required UserInfoDTO Creator { get; set; }
        public required ContractorDTO Contractor { get; set; }
        public required FixationWorkDTO FixationWork { get; set; }
        public required FixationDefectDTO FixationDefect { get; set; }
    }
}

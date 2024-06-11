using RoadDefectsService.Core.Application.DTOs.Common;
using RoadDefectsService.Core.Application.DTOs.FixationService;
using RoadDefectsService.Core.Domain.Enums;

namespace RoadDefectsService.Core.Application.DTOs.TaskService
{
    public class FixationDefectTaskDTO
    {
        public required Guid Id { get; set; }
        public required DateTime CreatedDateTime { get; set; }
        public required DefectStatus DefectStatus { get; set; }
        public required StatusTask TaskStatus { get; set; }
        public required string ApproximateAddress { get; set; }
        public required string Description { get; set; }
        public required FixationDefectDTO? DefectFixation { get; set; }
        public required RoadInspectorDTO? Executor { get; set; }
    }
}

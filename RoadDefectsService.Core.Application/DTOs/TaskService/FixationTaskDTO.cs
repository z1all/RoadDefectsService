using RoadDefectsService.Core.Application.DTOs.FixationService;
using RoadDefectsService.Core.Domain.Enums;

namespace RoadDefectsService.Core.Application.DTOs.TaskService
{
    public class FixationTaskDTO
    {
        public required Guid Id { get; set; }
        public required bool IsTransfer { get; set; }
        public required string Address { get; set; }
        public required double CoordinateX { get; set; }
        public required double CoordinateY { get; set; }
        public required DateTime CreatedDateTime { get; set; }
        public required DefectStatus DefectStatus { get; set; }
        public required StatusTask TaskStatus { get; set; }

        public required FixationDefectDTO? DefectFixation { get; set; }
        public required RoadInspectorDTO? Executor { get; set; }
    }
}

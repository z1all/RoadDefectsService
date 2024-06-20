using RoadDefectsService.Core.Domain.Enums;

namespace RoadDefectsService.Core.Application.DTOs.TaskService
{
    public class TaskDTO
    {
        public required Guid Id { get; set; }
        public required string Address { get; set; }
        public required DateTime CreatedDateTime { get; set; }
        public required TaskType TaskType { get; set; }
        public required DefectStatus DefectStatus { get; set; }
        public required StatusTask TaskStatus { get; set; }
        public required bool ExistRoadInspector { get; set; }
        public required bool ExistDefectInfo { get; set; }
    }
}

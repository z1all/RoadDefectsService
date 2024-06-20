using RoadDefectsService.Core.Domain.Enums;
using RoadDefectsService.Core.Domain.Models.Base;

namespace RoadDefectsService.Core.Domain.Models
{
    public class TaskEntity : BaseEntity
    {
        public TaskType TaskType { get; set; }
        public required string Address { get; set; }
        public required double CoordinateX { get; set; }
        public required double CoordinateY { get; set; }
        public required bool IsTransfer { get; set; }
        public required DateTime CreatedDateTime { get; set; }
        public required StatusTask TaskStatus { get; set; }
        public DefectStatus DefectStatus
        {
            get
            {
                if (TaskStatus == StatusTask.Completed)
                {
                    return FixationDefectId is null ? DefectStatus.ThereIsNotDefect : DefectStatus.ThereIsDefect;
                }
                return DefectStatus.NotVerified;
            }
        }

        public Guid? FixationDefectId { get; set; }
        public FixationDefectEntity? FixationDefect { get; set; }

        public Guid? RoadInspectorId { get; set; }
        public RoadInspectorEntity? RoadInspector { get; set; }

        public TaskFixationWorkEntity? NextTask { get; set; }
    }
}

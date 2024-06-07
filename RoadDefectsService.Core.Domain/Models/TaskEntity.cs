using RoadDefectsService.Core.Domain.Enums;
using RoadDefectsService.Core.Domain.Models.Base;

namespace RoadDefectsService.Core.Domain.Models
{
    public class TaskEntity : BaseEntity
    {
        public TaskType TaskType { get; set; }
        public required DateTime CreatedDateTime { get; set; }
        public required DefectStatus DefectStatus { get; set; }
        public required StatusTask TaskStatus { get; set; }

        public Guid? RoadInspectorId { get; set; }
        public RoadInspector? RoadInspector { get; set; }
    }
}

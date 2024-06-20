namespace RoadDefectsService.Core.Domain.Models
{
    public class TaskFixationDefectEntity : TaskEntity
    {
        public required string Description { get; set; }

        public TaskFixationDefectEntity() => TaskType = Enums.TaskType.FixationDefectTask;
    }
}

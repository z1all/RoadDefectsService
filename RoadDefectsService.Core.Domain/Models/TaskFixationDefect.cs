namespace RoadDefectsService.Core.Domain.Models
{
    public class TaskFixationDefect : TaskEntity
    {
        public required string Description { get; set; }

        public TaskFixationDefect() => TaskType = Enums.TaskType.FixationDefectTask;
    }
}

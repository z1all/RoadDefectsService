namespace RoadDefectsService.Core.Domain.Models
{
    public class TaskFixationWorkEntity : TaskEntity
    {
        public required Guid PrevTaskId { get; set; }
        public TaskEntity? PrevTask { get; set; }

        public Guid? FixationWorkId { get; set; }
        public FixationWorkEntity? FixationWork { get; set; }

        public TaskFixationWorkEntity() => TaskType = Enums.TaskType.FixationWorkTask;
    }
}

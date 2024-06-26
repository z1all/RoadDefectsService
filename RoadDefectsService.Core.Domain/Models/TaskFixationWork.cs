﻿namespace RoadDefectsService.Core.Domain.Models
{
    public class TaskFixationWork : TaskEntity
    {
        public required Guid PrevTaskId { get; set; }
        public TaskEntity? PrevTask { get; set; }

        public Guid? FixationWorkId { get; set; }
        public FixationWork? FixationWork { get; set; }

        public TaskFixationWork() => TaskType = Enums.TaskType.FixationWorkTask;
    }
}

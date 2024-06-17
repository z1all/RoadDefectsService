namespace RoadDefectsService.Core.Application.DTOs.TaskService
{
    public class CreateFixationWorkTaskDTO
    {
        public required Guid PrevTaskId { get; set; }
        public required bool IsTransfer { get; set; }
    }
}

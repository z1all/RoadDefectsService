using RoadDefectsService.Core.Application.Enums;

namespace RoadDefectsService.Core.Application.DTOs.TaskService
{
    public class ChangeTaskStatusDTO
    {
        public required ChangeTaskStatusEnum ChangeTaskStatus { get; set; } = ChangeTaskStatusEnum.StartTask;
    }
}

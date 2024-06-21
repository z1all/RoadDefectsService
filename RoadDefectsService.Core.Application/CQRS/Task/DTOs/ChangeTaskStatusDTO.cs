using RoadDefectsService.Core.Application.Enums;

namespace RoadDefectsService.Core.Application.CQRS.Task.DTOs
{
    public class ChangeTaskStatusDTO
    {
        public required ChangeTaskStatusEnum ChangeTaskStatus { get; set; } = ChangeTaskStatusEnum.StartTask;
    }
}

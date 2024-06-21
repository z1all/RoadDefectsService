using RoadDefectsService.Core.Application.Enums;

namespace RoadDefectsService.Core.Application.CQRS.Task.DTOs
{
    public class CommonTaskFilterDTO : TaskFilterDTO
    {
        public TaskViewModeFilter? TaskViewMode { get; set; } = TaskViewModeFilter.All;
    }
}

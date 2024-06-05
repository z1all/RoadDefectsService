using RoadDefectsService.Core.Application.Enums;

namespace RoadDefectsService.Core.Application.DTOs.TaskService
{
    public class CommonTaskFiler : TaskFilterDTO
    {
        public TaskViewModeFilter TaskViewMode { get; set; } = TaskViewModeFilter.All;
    }
}

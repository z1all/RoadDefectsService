using RoadDefectsService.Core.Application.DTOs.Common;
using RoadDefectsService.Core.Application.Enums;

namespace RoadDefectsService.Core.Application.DTOs.TaskService
{
    public class TaskFilterDTO : BaseFilterDTO
    {
        public TaskSortType? TaskSort { get; set; } = TaskSortType.None;
        public TaskTypeFilter? TaskType { get; set; } = TaskTypeFilter.None;
        public DefectStatusFilter? DefectStatus { get; set; } = DefectStatusFilter.None;
        public TaskStatusFilter? TaskStatus { get; set; } = TaskStatusFilter.None;
        public string? Address { get; set; }
    }
}

using RoadDefectsService.Core.Application.Enums;

namespace RoadDefectsService.Core.Application.DTOs.TaskService
{
    public class TaskFilterDTO
    {
        public TaskSortType TaskSort { get; set; } = TaskSortType.None;
        public TaskTypeFilter TaskType { get; set; } = TaskTypeFilter.None;
        public DefectStatusFilter DefectStatus { get; set; } = DefectStatusFilter.None;
        public TaskStatusFilter TaskStatus { get; set; } = TaskStatusFilter.None;

        public int Page { get; set; } = 1;
        public int Size { get; set; } = 10;
    }
}

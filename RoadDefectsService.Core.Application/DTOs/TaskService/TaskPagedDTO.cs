using RoadDefectsService.Core.Application.DTOs.Common;

namespace RoadDefectsService.Core.Application.DTOs.TaskService
{
    public class TaskPagedDTO
    {
        public required List<TaskDTO> Tasks { get; set; } 
        public required PageInfoDTO Pagination { get; set; } 
    }
}

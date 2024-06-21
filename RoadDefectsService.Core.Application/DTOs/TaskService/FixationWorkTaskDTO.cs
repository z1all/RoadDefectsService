using RoadDefectsService.Core.Application.CQRS.Task.DTOs;
using RoadDefectsService.Core.Application.DTOs.FixationService;

namespace RoadDefectsService.Core.Application.DTOs.TaskService
{
    public class FixationWorkTaskDTO : FixationTaskDTO
    {
        public required FixationWorkDTO? FixationWork { get; set; }
        public required TaskDTO PrevTask { get; set; }
    }
}

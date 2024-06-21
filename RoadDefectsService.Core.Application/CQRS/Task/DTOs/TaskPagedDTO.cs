using RoadDefectsService.Core.Application.DTOs.Common;

namespace RoadDefectsService.Core.Application.CQRS.Task.DTOs
{
    public class TaskPagedDTO : BasePagedDTO<TaskDTO>
    {
        public List<TaskDTO> Tasks { get => Models; set => Models = value; }
    }
}

using System.Text.Json.Serialization;
using RoadDefectsService.Core.Application.DTOs.Common;

namespace RoadDefectsService.Core.Application.DTOs.TaskService
{
    public class TaskPagedDTO : BasePagedDTO<TaskDTO>
    {
        public List<TaskDTO> Tasks { get => Models; set => Models = value; }
    }
}

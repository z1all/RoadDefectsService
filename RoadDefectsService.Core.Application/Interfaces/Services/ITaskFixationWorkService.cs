using RoadDefectsService.Core.Application.DTOs.TaskService;
using RoadDefectsService.Core.Application.Models;

namespace RoadDefectsService.Core.Application.Interfaces.Services
{
    public interface ITaskFixationWorkService
    {
        Task<ExecutionResult<FixationWorkTaskDTO>> GetFixationWorkTaskAsync(Guid taskId, Guid? inspectorId = null);
        Task<ExecutionResult<CreateTaskResponseDTO>> CreateFixationWorkTaskAsync(CreateFixationWorkTaskDTO createFixationWork);
    }
}

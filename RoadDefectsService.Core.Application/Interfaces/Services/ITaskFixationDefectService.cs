using RoadDefectsService.Core.Application.DTOs.TaskService;
using RoadDefectsService.Core.Application.Models;

namespace RoadDefectsService.Core.Application.Interfaces.Services
{
    public interface ITaskFixationDefectService
    {
        Task<ExecutionResult<FixationDefectTaskDTO>> GetFixationDefectTaskAsync(Guid taskId, Guid? inspectorId = null);
        Task<ExecutionResult> EditFixationDefectTaskAsync(EditFixationDefectTaskDTO editFixationDefect, Guid taskId);
        Task<ExecutionResult<CreateTaskResponseDTO>> CreateFixationDefectTaskAsync(CreateFixationDefectTaskDTO createFixationDefect);
    }
}

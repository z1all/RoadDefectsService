using RoadDefectsService.Core.Application.DTOs.TaskService;
using RoadDefectsService.Core.Application.Interfaces.Repositories;
using RoadDefectsService.Core.Application.Interfaces.Services;
using RoadDefectsService.Core.Application.Models;

namespace RoadDefectsService.Core.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public Task<ExecutionResult<TaskPagedDTO>> GetTasksAsync(CommonTaskFilterDTO taskFilter)
        {
            throw new NotImplementedException();
        }

        public Task<ExecutionResult<TaskPagedDTO>> GetInspectorTasksAsync(TaskFilterDTO taskFilter, Guid inspectorId)
        {
            throw new NotImplementedException();
        }

        public Task<ExecutionResult> AppointTaskAsync(Guid taskId, Guid inspectorId)
        {
            throw new NotImplementedException();
        }
    }
}

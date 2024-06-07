using RoadDefectsService.Core.Application.DTOs.TaskService;
using RoadDefectsService.Core.Application.Helpers;
using RoadDefectsService.Core.Application.Interfaces.Repositories;
using RoadDefectsService.Core.Application.Interfaces.Services;
using RoadDefectsService.Core.Application.Mappers;
using RoadDefectsService.Core.Application.Models;
using RoadDefectsService.Core.Domain.Enums;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IRoadInspectorRepository _roadInspectorRepository;

        public TaskService(ITaskRepository taskRepository, IRoadInspectorRepository roadInspectorRepository)
        {
            _taskRepository = taskRepository;
            _roadInspectorRepository = roadInspectorRepository;
        }

        public async Task<ExecutionResult<TaskPagedDTO>> GetTasksAsync(CommonTaskFilterDTO taskFilter)
        {
            return await FiltrationHelper
                .FilterAsync<CommonTaskFilterDTO, TaskEntity, TaskDTO, TaskPagedDTO>(
                taskFilter, _taskRepository, (tasks) => tasks.ToTaskDTOList());
        }

        public async Task<ExecutionResult> DeleteTaskAsync(Guid taskId)
        {
            TaskEntity? task = await _taskRepository.GetByIdAsync(taskId);
            if (task is null)
            {
                return new(StatusCodeExecutionResult.NotFound, "TaskNotFound", $"Task with id {taskId} not found!");
            }

            await _taskRepository.DeleteAsync(task);

            return ExecutionResult.Success;
        }

        public async Task<ExecutionResult<TaskPagedDTO>> GetInspectorTasksAsync(TaskFilterDTO taskFilter, Guid inspectorId)
        {
            return await FiltrationHelper
               .FilterAsync<TaskFilterDTO, TaskEntity, TaskDTO, TaskPagedDTO>(
                   taskFilter,
                   (filter) => _taskRepository.CountByFilterAsync(filter, inspectorId),
                   (filter) => _taskRepository.GetAllByFilterAsync(filter, inspectorId),
                   (tasks) => tasks.ToTaskDTOList()
               );
        }

        public async Task<ExecutionResult> AppointTaskAsync(Guid taskId, Guid inspectorId)
        {
            TaskEntity? task = await _taskRepository.GetByIdAsync(taskId);
            if (task is null)
            {
                return new(StatusCodeExecutionResult.NotFound, "TaskNotFound", $"Task with id {taskId} not found!");
            }

            bool existRoadInspector = await _roadInspectorRepository.AnyByIdAsync(inspectorId);
            if (!existRoadInspector)
            {
                return new(StatusCodeExecutionResult.NotFound, "RoadInspectorNotFound", $"Road inspector with id {inspectorId} not found!");
            }

            if (task.TaskStatus == StatusTask.Completed)
            {
                return new(StatusCodeExecutionResult.Forbid, "TaskCompleted", $"Assigning an inspector to a completed task!");
            }
            else if (task.TaskStatus == StatusTask.Processing)
            {
                return new(StatusCodeExecutionResult.Forbid, "TaskProcessing", $"Assigning an inspector to a processing task!");
            }

            task.RoadInspectorId = inspectorId;

            await _taskRepository.UpdateAsync(task);
        
            return ExecutionResult.Success;
        }
    }
}

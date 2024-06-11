using RoadDefectsService.Core.Application.DTOs.TaskService;
using RoadDefectsService.Core.Application.Interfaces.Repositories;
using RoadDefectsService.Core.Application.Interfaces.Services;
using RoadDefectsService.Core.Application.Mappers;
using RoadDefectsService.Core.Application.Models;
using RoadDefectsService.Core.Domain.Enums;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.Services
{
    public class TaskFixationWorkService : ITaskFixationWorkService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ITaskFixationWorkRepository _taskFixationWorkRepository;

        public TaskFixationWorkService(ITaskRepository taskRepository, ITaskFixationWorkRepository taskFixationWorkRepository)
        {
            _taskRepository = taskRepository;
            _taskFixationWorkRepository = taskFixationWorkRepository;
        }

        public async Task<ExecutionResult<FixationWorkTaskDTO>> GetFixationWorkTaskAsync(Guid taskId, Guid? inspectorId)
        {
            TaskFixationWork? task = await _taskFixationWorkRepository.GetByIdWithInspectorAndPrevTaskAndFixationsWithPhotosAndDefectTypeAsync(taskId);
            if (task is null)
            {
                return new(StatusCodeExecutionResult.NotFound, "TaskNotFound", $"Task with id {taskId} not found!");
            }
            else if(inspectorId is not null && task.RoadInspectorId != inspectorId)
            {
                return new(StatusCodeExecutionResult.NotFound, "TaskNotFound", $"Inspector with id {inspectorId} doesn't have task with id {taskId}");
            }

            return task.ToFixationWorkTaskDTO();
        }

        public async Task<ExecutionResult<CreateTaskResponseDTO>> CreateFixationWorkTaskAsync(CreateFixationWorkTaskDTO createFixationWork)
        {
            bool existPrevTask = await _taskRepository.AnyByIdAsync(createFixationWork.PrevTaskId);
            if (!existPrevTask)
            {
                return new(StatusCodeExecutionResult.NotFound, "PrevTaskNotFound", $"Prev task with id {createFixationWork.PrevTaskId} not found!");
            }

            bool existWithSamePrevTask = await _taskFixationWorkRepository.AnyWithPrevTaskId(createFixationWork.PrevTaskId);
            if (existWithSamePrevTask)
            {
                return new(StatusCodeExecutionResult.BadRequest, "ExistWithSamePrevTask", $"This task is already a previous one for another task!");
            }

            TaskFixationWork task = new()
            {
                CreatedDateTime = DateTime.UtcNow,
                TaskStatus = StatusTask.Created,
                PrevTaskId = createFixationWork.PrevTaskId
            };

            await _taskFixationWorkRepository.AddAsync(task);

            return new CreateTaskResponseDTO() { CreatedTaskId = task.Id };
        }
    }
}

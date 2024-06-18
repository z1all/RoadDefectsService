using AutoMapper;
using RoadDefectsService.Core.Application.DTOs.TaskService;
using RoadDefectsService.Core.Application.Helpers;
using RoadDefectsService.Core.Application.Interfaces.Repositories;
using RoadDefectsService.Core.Application.Interfaces.Services;
using RoadDefectsService.Core.Application.Models;
using RoadDefectsService.Core.Domain.Enums;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.Services
{
    public class TaskFixationWorkService : ITaskFixationWorkService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ITaskFixationWorkRepository _taskFixationWorkRepository;
        private readonly IMapper _mapper;

        public TaskFixationWorkService(
            ITaskRepository taskRepository, ITaskFixationWorkRepository taskFixationWorkRepository,
            IMapper mapper)
        {
            _taskRepository = taskRepository;
            _taskFixationWorkRepository = taskFixationWorkRepository;
            _mapper = mapper;
        }

        public async Task<ExecutionResult<FixationWorkTaskDTO>> GetFixationWorkTaskAsync(Guid taskId, Guid? inspectorId)
        {
            TaskFixationWork? task = await _taskFixationWorkRepository.GetByIdWithInspectorAndPrevTaskAndNextTaskAndFixationsWithPhotosAndDefectTypeAsync(taskId);
            if (task is null)
            {
                return new(StatusCodeExecutionResult.NotFound, "TaskNotFound", $"Task with id {taskId} not found!");
            }

            ExecutionResult checkResult = CheckTaskHelper.CheckOnTaskOwnerAnsNextTaskOwner(task, inspectorId);
            if (checkResult.IsNotSuccess)
            {
                return ExecutionResult<FixationWorkTaskDTO>.FromError(checkResult);
            }

            return _mapper.Map<FixationWorkTaskDTO>(task);
        }

        public async Task<ExecutionResult<CreateTaskResponseDTO>> CreateFixationWorkTaskAsync(CreateFixationWorkTaskDTO createFixationWork)
        {
            TaskEntity? prevTask = await _taskRepository.GetByIdAsync(createFixationWork.PrevTaskId);
            if (prevTask is null)
            {
                return new(StatusCodeExecutionResult.NotFound, "PrevTaskNotFound", $"Prev task with id {createFixationWork.PrevTaskId} not found!");
            }

            if (prevTask.DefectStatus != DefectStatus.ThereIsDefect)
            {
                return new(StatusCodeExecutionResult.BadRequest, "CreateFixationWorkTaskFail", $"Prev task does not have defect status 'ThereIsDefect'!");
            }

            bool existWithSamePrevTask = await _taskFixationWorkRepository.AnyWithPrevTaskId(createFixationWork.PrevTaskId);
            if (existWithSamePrevTask)
            {
                return new(StatusCodeExecutionResult.BadRequest, "ExistWithSamePrevTask", $"This task is already a previous one for another task!");
            }

            TaskFixationWork task = new()
            {
                ApproximateAddress = prevTask.ApproximateAddress,
                CreatedDateTime = DateTime.UtcNow,
                TaskStatus = createFixationWork.IsTransfer ? StatusTask.Completed : StatusTask.Created,
                PrevTaskId = createFixationWork.PrevTaskId,
                IsTransfer = createFixationWork.IsTransfer,
            };

            await _taskFixationWorkRepository.AddAsync(task);

            return new CreateTaskResponseDTO() { CreatedTaskId = task.Id };
        }
    }
}

using AutoMapper;
using RoadDefectsService.Core.Application.DTOs.TaskService;
using RoadDefectsService.Core.Application.Enums;
using RoadDefectsService.Core.Application.Helpers;
using RoadDefectsService.Core.Application.Interfaces.Repositories;
using RoadDefectsService.Core.Application.Interfaces.Services;
using RoadDefectsService.Core.Application.Models;
using RoadDefectsService.Core.Domain.Enums;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IRoadInspectorRepository _roadInspectorRepository;
        private readonly IMapper _mapper;

        public TaskService(
            ITaskRepository taskRepository, IRoadInspectorRepository roadInspectorRepository,
            IMapper mapper)
        {
            _taskRepository = taskRepository;
            _roadInspectorRepository = roadInspectorRepository;
            _mapper = mapper;
        }

        public async Task<ExecutionResult<TaskPagedDTO>> GetTasksAsync(CommonTaskFilterDTO taskFilter)
        {
            return await FiltrationHelper
                .FilterAsync<CommonTaskFilterDTO, TaskEntity, TaskDTO, TaskPagedDTO>(
                taskFilter, _taskRepository, (tasks) => _mapper.Map<List<TaskDTO>>(tasks));
        }

        public async Task<ExecutionResult> ChangeTaskAsync(EditTaskDTO editTask, Guid taskId)
        {
            TaskEntity? task = await _taskRepository.GetByIdAsync(taskId);
            if (task is null)
            {
                return new(StatusCodeExecutionResult.NotFound, "TaskNotFound", $"Task with id {taskId} not found!");
            }

            if (!task.IsTransfer)
            {
                return new(StatusCodeExecutionResult.BadRequest, "TaskIsNotTransfer", $"The task should be to transfer data from paper to electronic form!");
            }

            task.CreatedDateTime = editTask.CreatedDateTime;
            await _taskRepository.UpdateAsync(task);

            return ExecutionResult.SucceededResult;
        }

        public async Task<ExecutionResult> DeleteTaskAsync(Guid taskId)
        {
            TaskEntity? task = await _taskRepository.GetByIdAsync(taskId);
            if (task is null)
            {
                return new(StatusCodeExecutionResult.NotFound, "TaskNotFound", $"Task with id {taskId} not found!");
            }

            await _taskRepository.DeleteAsync(task);

            return ExecutionResult.SucceededResult;
        }

        public async Task<ExecutionResult<TaskPagedDTO>> GetInspectorTasksAsync(TaskFilterDTO taskFilter, Guid inspectorId)
        {
            return await FiltrationHelper
               .FilterAsync<TaskFilterDTO, TaskEntity, TaskDTO, TaskPagedDTO>(
                   taskFilter,
                   (filter) => _taskRepository.CountByFilterAsync(filter, inspectorId),
                   (filter) => _taskRepository.GetAllByFilterAsync(filter, inspectorId),
                   (tasks) => _mapper.Map<List<TaskDTO>>(tasks)
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

            if (!task.IsTransfer)
            {
                if (task.TaskStatus == StatusTask.Completed)
                {
                    return new(StatusCodeExecutionResult.Forbid, "TaskCompleted", $"Assigning an inspector to a completed task!");
                }
                else if (task.TaskStatus == StatusTask.Processing)
                {
                    return new(StatusCodeExecutionResult.Forbid, "TaskProcessing", $"Assigning an inspector to a processing task!");
                }
            }

            task.RoadInspectorId = inspectorId;

            await _taskRepository.UpdateAsync(task);
        
            return ExecutionResult.SucceededResult;
        }

        public async Task<ExecutionResult> ChangeTaskStatusAsync(ChangeTaskStatusDTO changeTaskStatus, Guid taskId)
        {
            TaskEntity? task = await _taskRepository.GetByIdWithFixationDefectAsync(taskId);
            if (task is null)
            {
                return new(StatusCodeExecutionResult.NotFound, "TaskNotFound", $"Task with id {taskId} not found!");
            }

            if (changeTaskStatus.ChangeTaskStatus == ChangeTaskStatusEnum.FinishTask)
            {
                ExecutionResult checkResult = CheckOnNullProperties(task.FixationDefect);
                if (checkResult.IsNotSuccess) return checkResult;
            }

            if (task.TaskStatus == StatusTask.Completed)
            {
                return new(StatusCodeExecutionResult.Forbid, "TaskCompleted", $"Changing the status of a completed task!");
            }

            if (task.TaskStatus != StatusTask.Processing && changeTaskStatus.ChangeTaskStatus == ChangeTaskStatusEnum.FinishTask)
            {
                return new(StatusCodeExecutionResult.Forbid, "TaskNotProcessing", $"Completing a task that has not been started!");
            }

            task.TaskStatus = changeTaskStatus.ChangeTaskStatus switch
            {
                ChangeTaskStatusEnum.StartTask => StatusTask.Processing,
                ChangeTaskStatusEnum.CancelTask => StatusTask.Created,
                ChangeTaskStatusEnum.FinishTask => StatusTask.Completed,
                _ => task.TaskStatus
            };

            await _taskRepository.UpdateAsync(task);
            
            return ExecutionResult.SucceededResult;
        }

        private ExecutionResult CheckOnNullProperties(FixationDefect? fixationDefect)
        {
            if (fixationDefect is not null &&
               (fixationDefect.ExactAddress is null || fixationDefect.CoordinatesX is null ||
                fixationDefect.CoordinatesY is null || fixationDefect.DamagedCanvasSquareMeter is null ||
                fixationDefect.DefectTypeId is null))
            {
                return new(StatusCodeExecutionResult.BadRequest, "FixationDefectNullProperties", "You cannot complete the task because one or more properties of the fixation defect have a null value!");
            }

            return ExecutionResult.SucceededResult;
        }
    }
}

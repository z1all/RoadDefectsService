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
    public class TaskFixationDefectService : ITaskFixationDefectService
    {
        private readonly ITaskFixationDefectRepository _taskFixationDefectRepository;
        private readonly IMapper _mapper;

        public TaskFixationDefectService(ITaskFixationDefectRepository taskFixationDefectRepository, IMapper mapper)
        {
            _taskFixationDefectRepository = taskFixationDefectRepository;
            _mapper = mapper;
        }

        public async Task<ExecutionResult<FixationDefectTaskDTO>> GetFixationDefectTaskAsync(Guid taskId, Guid? inspectorId = null)
        {
            TaskFixationDefectEntity? task = await _taskFixationDefectRepository.GetByIdWithInspectorAndNextTaskAndDefectWithPhotosAndDefectTypeAndAssignmentAsync(taskId);
            if (task is null)
            {
                return new(StatusCodeExecutionResult.NotFound, "TaskNotFound", $"Task with id {taskId} not found!");
            }

            ExecutionResult checkResult = CheckTaskHelper.CheckOnTaskOwnerAnsNextTaskOwner(task, inspectorId);
            if (checkResult.IsNotSuccess)
            {
                return ExecutionResult<FixationDefectTaskDTO>.FromError(checkResult);
            }

            return _mapper.Map<FixationDefectTaskDTO>(task);
        }

        public async Task<ExecutionResult> EditFixationDefectTaskAsync(EditFixationDefectTaskDTO editFixationDefect, Guid taskId)
        {
            TaskFixationDefectEntity? task = await _taskFixationDefectRepository.GetByIdWithInspectorAndDefectWithPhotosAndDefectTypeAsync(taskId);
            if (task is null)
            {
                return new(StatusCodeExecutionResult.NotFound, "TaskNotFound", $"Task with id {taskId} not found!");
            }

            task.Description = editFixationDefect.Description;
            task.Address = editFixationDefect.Address;
            task.CoordinateX = editFixationDefect.CoordinateX;
            task.CoordinateY = editFixationDefect.CoordinateY;

            await _taskFixationDefectRepository.UpdateAsync(task);

            return ExecutionResult.SucceededResult;
        }

        public async Task<ExecutionResult<CreateTaskResponseDTO>> CreateFixationDefectTaskAsync(CreateFixationDefectTaskDTO createFixationDefect)
        {
            TaskFixationDefectEntity task = new()
            {
                CreatedDateTime = DateTime.UtcNow,
                TaskStatus = createFixationDefect.IsTransfer ? StatusTask.Completed : StatusTask.Created,
                Address = createFixationDefect.Address,
                CoordinateX = createFixationDefect.CoordinateX,
                CoordinateY = createFixationDefect.CoordinateY,
                Description = createFixationDefect.Description,
                IsTransfer = createFixationDefect.IsTransfer,
            };

            await _taskFixationDefectRepository.AddAsync(task);

            return new CreateTaskResponseDTO() { CreatedTaskId = task.Id };
        }
    }
}

using AutoMapper;
using RoadDefectsService.Core.Application.DTOs.TaskService;
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
            TaskFixationDefect? task = await _taskFixationDefectRepository.GetByIdWithInspectorAndDefectWithPhotosAndDefectTypeAsync(taskId);
            if (task is null)
            {
                return new(StatusCodeExecutionResult.NotFound, "TaskNotFound", $"Task with id {taskId} not found!");
            }
            else if (inspectorId is not null && task.RoadInspectorId != inspectorId)
            {
                return new(StatusCodeExecutionResult.NotFound, "TaskNotFound", $"Inspector with id {inspectorId} doesn't have task with id {taskId}");
            }

            return _mapper.Map<FixationDefectTaskDTO>(task);
        }

        public async Task<ExecutionResult> EditFixationDefectTaskAsync(CreateEditFixationDefectTaskDTO editFixationDefect, Guid taskId)
        {
            TaskFixationDefect? task = await _taskFixationDefectRepository.GetByIdWithInspectorAndDefectWithPhotosAndDefectTypeAsync(taskId);
            if (task is null)
            {
                return new(StatusCodeExecutionResult.NotFound, "TaskNotFound", $"Task with id {taskId} not found!");
            }

            task.Description = editFixationDefect.Description;
            task.ApproximateAddress = editFixationDefect.ApproximateAddress;

            await _taskFixationDefectRepository.UpdateAsync(task);

            return ExecutionResult.SucceededResult;
        }

        public async Task<ExecutionResult<CreateTaskResponseDTO>> CreateFixationDefectTaskAsync(CreateEditFixationDefectTaskDTO createFixationDefect)
        {
            TaskFixationDefect task = new()
            {
                CreatedDateTime = DateTime.UtcNow,
                TaskStatus = StatusTask.Created,
                ApproximateAddress = createFixationDefect.ApproximateAddress,
                Description = createFixationDefect.Description,
            };

            await _taskFixationDefectRepository.AddAsync(task);

            return new CreateTaskResponseDTO() { CreatedTaskId = task.Id };
        }
    }
}

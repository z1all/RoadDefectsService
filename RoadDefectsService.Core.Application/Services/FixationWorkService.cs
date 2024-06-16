using AutoMapper;
using RoadDefectsService.Core.Application.DTOs.FixationService;
using RoadDefectsService.Core.Application.Helpers;
using RoadDefectsService.Core.Application.Interfaces.Repositories;
using RoadDefectsService.Core.Application.Interfaces.Services;
using RoadDefectsService.Core.Application.Models;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.Services
{
    public class FixationWorkService : IFixationWorkService
    {
        private readonly IFixationWorkRepository _fixationWorkRepository;
        private readonly ITaskFixationWorkRepository _taskFixationWorkRepository;
        private readonly IMapper _mapper;

        public FixationWorkService(
            IFixationWorkRepository fixationWorkRepository, ITaskFixationWorkRepository taskFixationWorkRepository,
            IMapper mapper)
        {
            _fixationWorkRepository = fixationWorkRepository;
            _taskFixationWorkRepository = taskFixationWorkRepository;
            _mapper = mapper;
        }

        public async Task<ExecutionResult<FixationWorkDTO>> GetFixationWorkAsync(Guid fixationWorkId, Guid? userId)
        {
            FixationWork? fixationWork = await _fixationWorkRepository.GetByIdWithTaskAndPhotosAsync(fixationWorkId);
            if (fixationWork is null)
            {
                return new(StatusCodeExecutionResult.NotFound, "FixationWorkNotFound", $"Fixation work defect with id {fixationWorkId} not found!");
            }

            ExecutionResult checkResult = CheckTaskHelper.CheckOnTaskOwner(fixationWork.TaskFixationWork!, userId);
            if (checkResult.IsNotSuccess)
            {
                return ExecutionResult<FixationWorkDTO>.FromError(checkResult);
            }

            return _mapper.Map<FixationWorkDTO>(fixationWork);
        }

        public async Task<ExecutionResult> DeleteFixationWorkAsync(Guid fixationWorkId, Guid? userId)
        {
            FixationWork? fixationWork = await _fixationWorkRepository.GetByIdWithTaskAsync(fixationWorkId);
            if (fixationWork is null)
            {
                return new(StatusCodeExecutionResult.NotFound, "FixationWorkNotFound", $"Fixation work defect with id {fixationWorkId} not found!");
            }

            ExecutionResult checkResult = CheckTaskHelper.CheckOnTaskOwnerAndProcessingTaskStatus(fixationWork.TaskFixationWork!, userId);
            if (checkResult.IsNotSuccess) return checkResult;

            await _fixationWorkRepository.DeleteAsync(fixationWork);

            return ExecutionResult.SucceededResult;
        }

        public async Task<ExecutionResult<CreateFixationResponseDTO>> CreateFixationWorkAsync(CreateFixationWorkDTO createFixationWork, Guid? userId)
        {
            TaskFixationWork? task = await _taskFixationWorkRepository.GetByIdAsync(createFixationWork.TaskFixationWorkId);
            if (task is null)
            {
                return new(StatusCodeExecutionResult.NotFound, "TaskNotFound", $"Task with id {createFixationWork.TaskFixationWorkId} not found!");
            }

            ExecutionResult checkResult = CheckTaskHelper.CheckOnTaskOwnerAndProcessingTaskStatus(task, userId);
            if (checkResult.IsNotSuccess)
            {
                return ExecutionResult<CreateFixationResponseDTO>.FromError(checkResult);
            }

            if (task.FixationDefectId.HasValue)
            {
                return new(StatusCodeExecutionResult.NotFound, "TaskNotFound", $"The task with id {createFixationWork.TaskFixationWorkId} already has a defect fixation with id {task.FixationDefectId}!");
            }

            FixationWork fixationWork = new()
            {
                RecordedDateTime = DateTime.UtcNow,
            };
            await _fixationWorkRepository.AddAsync(fixationWork);

            task.FixationWork = fixationWork;
            await _taskFixationWorkRepository.UpdateAsync(task);

            return new CreateFixationResponseDTO() { CreatedFixationId = fixationWork.Id };
        }

        public async Task<ExecutionResult> ChangeFixationWorkAsync(EditFixationWorkDTO editFixationWork, Guid fixationWorkId, Guid? userId)
        {
            FixationWork? fixationWork = await _fixationWorkRepository.GetByIdWithTaskWithPrevTaskWithFixationDefectAsync(fixationWorkId);
            if (fixationWork is null)
            {
                return new(StatusCodeExecutionResult.NotFound, "FixationWorkNotFound", $"Fixation work defect with id {fixationWorkId} not found!");
            }

            ExecutionResult checkResult = CheckTaskHelper.CheckOnTaskOwnerAndProcessingTaskStatus(fixationWork.TaskFixationWork!, userId);
            if (checkResult.IsNotSuccess) return checkResult;

            fixationWork.RecordedDateTime = DateTime.UtcNow;
            fixationWork.WorkDone = editFixationWork.WorkDone;

            if (fixationWork.TaskFixationWork?.PrevTask?.FixationDefect is not null)
            {
                fixationWork.TaskFixationWork.PrevTask.FixationDefect.IsEliminated = editFixationWork.WorkDone;
            }

            await _fixationWorkRepository.UpdateAsync(fixationWork);

            return ExecutionResult.SucceededResult;
        }
    }
}

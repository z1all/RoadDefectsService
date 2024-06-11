using RoadDefectsService.Core.Application.DTOs.FixationService;
using RoadDefectsService.Core.Application.Helpers;
using RoadDefectsService.Core.Application.Interfaces.Repositories;
using RoadDefectsService.Core.Application.Interfaces.Services;
using RoadDefectsService.Core.Application.Mappers;
using RoadDefectsService.Core.Application.Models;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.Services
{
    public class FixationDefectService : IFixationDefectService
    {
        private readonly IFixationDefectRepository _fixationDefectRepository;
        private readonly ITaskRepository _taskRepository;
        private readonly IDefectTypeRepository _defectTypeRepository;

        public FixationDefectService(
            IFixationDefectRepository fixationDefectRepository, ITaskRepository taskRepository,
            IDefectTypeRepository defectTypeRepository)
        {
            _fixationDefectRepository = fixationDefectRepository;
            _taskRepository = taskRepository;
            _defectTypeRepository = defectTypeRepository;
        }

        public async Task<ExecutionResult<FixationDefectDTO>> GetFixationDefectAsync(Guid fixationDefectId, Guid? userId)
        {
            FixationDefect? fixationDefect = await _fixationDefectRepository.GetByIdWithTaskAndPhotosAndDefectTypeAsync(fixationDefectId);
            if (fixationDefect is null)
            {
                return new(StatusCodeExecutionResult.NotFound, "FixationDefectNotFound", $"Fixation defect with id {fixationDefectId} not found!");
            }

            ExecutionResult checkResult = CheckTaskHelper.CheckOnTaskOwner(fixationDefect.Task!, userId);
            if (checkResult.IsNotSuccess)
            {
                return new() { Errors = checkResult.Errors };
            }

            return fixationDefect.ToFixationDefectDTO();
        }

        public async Task<ExecutionResult> DeleteFixationDefectAsync(Guid fixationDefectId, Guid? userId)
        {
            FixationDefect? fixationDefect = await _fixationDefectRepository.GetByIdWithTaskAsync(fixationDefectId);
            if (fixationDefect is null)
            {
                return new(StatusCodeExecutionResult.NotFound, "FixationDefectNotFound", $"Fixation defect with id {fixationDefectId} not found!");
            }

            ExecutionResult checkResult = CheckTaskHelper.CheckOnTaskOwnerAndProcessingTaskStatus(fixationDefect.Task!, userId);
            if (checkResult.IsNotSuccess) return checkResult;

            await _fixationDefectRepository.DeleteAsync(fixationDefect);

            return ExecutionResult.SucceededResult;
        }

        public async Task<ExecutionResult<CreateFixationResponseDTO>> CreateFixationDefectAsync(CreateFixationDefectDTO createFixationDefect, Guid? userId)
        {
            TaskEntity? task = await _taskRepository.GetByIdAsync(createFixationDefect.TaskId);
            if (task is null)
            {
                return new(StatusCodeExecutionResult.NotFound, "TaskNotFound", $"Task with id {createFixationDefect.TaskId} not found!");
            }

            ExecutionResult checkResult = CheckTaskHelper.CheckOnTaskOwnerAndProcessingTaskStatus(task, userId);
            if (checkResult.IsNotSuccess) return new() { Errors = checkResult.Errors };

            if (task.FixationDefectId.HasValue)
            {
                return new(StatusCodeExecutionResult.NotFound, "TaskNotFound", $"The task with id {createFixationDefect.TaskId} already has a defect fixation with id {task.FixationDefectId}!");
            }

            FixationDefect fixationDefect = new()
            {
                RecordedDateTime = DateTime.UtcNow,
            };
            await _fixationDefectRepository.AddAsync(fixationDefect);

            task.FixationDefect = fixationDefect;
            await _taskRepository.UpdateAsync(task);

            return new CreateFixationResponseDTO() { CreatedFixationId = fixationDefect.Id };
        }

        public async Task<ExecutionResult> ChangeFixationDefectAsync(EditFixationDefectDTO editFixationDefect, Guid fixationDefectId, Guid? userId)
        {
            FixationDefect? fixationDefect = await _fixationDefectRepository.GetByIdWithTaskAsync(fixationDefectId);
            if (fixationDefect is null)
            {
                return new(StatusCodeExecutionResult.NotFound, "FixationDefectNotFound", $"Fixation defect with id {fixationDefectId} not found!");
            }

            ExecutionResult checkResult = CheckTaskHelper.CheckOnTaskOwnerAndProcessingTaskStatus(fixationDefect.Task!, userId);
            if (checkResult.IsNotSuccess) return checkResult;

            bool existDefectType = await _defectTypeRepository.AnyByIdAsync(editFixationDefect.DefectTypeId);
            if (!existDefectType)
            {
                return new(StatusCodeExecutionResult.NotFound, "DefectTypeNotFound", $"Defect type defect with id {editFixationDefect.DefectTypeId} not found!");
            }

            fixationDefect.RecordedDateTime = DateTime.UtcNow;
            fixationDefect.ExactAddress = editFixationDefect.ExactAddress;
            fixationDefect.CoordinatesX = editFixationDefect.CoordinatesX;
            fixationDefect.CoordinatesY = editFixationDefect.CoordinatesY;
            fixationDefect.DamagedCanvasSquareMeter = editFixationDefect.DamagedCanvasSquareMeter;
            fixationDefect.DefectTypeId = editFixationDefect.DefectTypeId;

            await _fixationDefectRepository.UpdateAsync(fixationDefect);

            return ExecutionResult.SucceededResult;
        }
    }
}

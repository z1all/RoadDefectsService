using RoadDefectsService.Core.Application.DTOs.PhotoService;
using RoadDefectsService.Core.Application.Helpers;
using RoadDefectsService.Core.Application.Interfaces.Repositories;
using RoadDefectsService.Core.Application.Interfaces.Services;
using RoadDefectsService.Core.Application.Models;
using RoadDefectsService.Core.Domain.Enums;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IFileService _fileService;
        private readonly IPhotoRepository _photoRepository;
        private readonly IFixationWorkRepository _fixationWorkRepository;
        private readonly IFixationDefectRepository _fixationDefectRepository;

        public PhotoService(
            IFileService fileService, IPhotoRepository photoRepository,
            IFixationWorkRepository fixationWorkRepository, IFixationDefectRepository fixationDefectRepository)
        {
            _fileService = fileService;
            _photoRepository = photoRepository;
            _fixationWorkRepository = fixationWorkRepository;
            _fixationDefectRepository = fixationDefectRepository;
        }

        public async Task<ExecutionResult<PhotoUploadResponseDTO>> AddPhotoAsync(PhotoDTO addPhoto, Guid fixationId, Guid? userId)
        {
            ExecutionResult<FixationType> checkResult = await CheckOnTaskOwnerAndTaskStatusAsync(fixationId, userId);
            if (!checkResult.TryGetResult(out FixationType fixationType))
            {
                return new() { Errors = checkResult.Errors };
            }

            Photo photo = new()
            {
                Id = Guid.NewGuid(),
                Name = addPhoto.Name,
                Type = addPhoto.Type,
                FixationDefectId = fixationType == FixationType.FixationDefect ? fixationId : null,
                FixationWorkId = fixationType == FixationType.FixationWork ? fixationId : null,
            };

            bool saveResult = await _fileService.SaveFileAsync(photo.PathName, addPhoto.File);
            if (!saveResult)
            {
                return new(StatusCodeExecutionResult.InternalServer, "SavePhotoFail", "Error while saving a photo!");
            }

            await _photoRepository.AddAsync(photo);

            return new PhotoUploadResponseDTO() { UploadedPhotoId = photo.Id };
        }

        public async Task<ExecutionResult> DeletePhotoAsync(Guid fixationId, Guid photoId, Guid? userId)
        {
            Photo? photo = await _photoRepository.GetByIdAndFixationIdAsync(photoId, fixationId);
            if (photo is null)
            {
                return new(StatusCodeExecutionResult.NotFound, "PhotoNotFound", $"Photo with id {photoId} not found in fixation with id {fixationId}!");
            }

            ExecutionResult checkResult = await CheckOnTaskOwnerAndTaskStatusAsync(photo, userId);
            if (checkResult.IsNotSuccess)
            {
                return checkResult;
            }

            bool deleteResult = _fileService.DeleteFile(photo.PathName);
            if (!deleteResult)
            {
                return new(StatusCodeExecutionResult.InternalServer, "DeletePhotoFail", "Error while deleting a photo!");
            }

            await _photoRepository.DeleteAsync(photo);

            return ExecutionResult.SucceededResult;
        }

        public async Task<ExecutionResult<PhotoDTO>> GetPhotoAsync(Guid fixationId, Guid photoId, Guid? userId)
        {
            Photo? photo = await _photoRepository.GetByIdAndFixationIdAsync(photoId, fixationId);
            if (photo is null)
            {
                return new(StatusCodeExecutionResult.NotFound, "PhotoNotFound", $"Photo with id {photoId} not found in fixation with id {fixationId}!");
            }

            ExecutionResult checkResult = await CheckOnTaskOwnerAsync(photo, userId);
            if (checkResult.IsNotSuccess)
            {
                return new() { Errors = checkResult.Errors };
            }

            byte[]? photoFile = await _fileService.GetFileAsync(photo.PathName);
            if (photoFile is null)
            {
                return new(StatusCodeExecutionResult.InternalServer, "GetPhotoFail", $"Error while getting a photo!");
            }

            return new PhotoDTO()
            {
                Name = photo.Name,
                Type = photo.Type,
                File = photoFile,
            };
        }

        private Task<ExecutionResult<FixationType>> CheckOnTaskOwnerAsync(Photo photo, Guid? userId)
        {
            return CheckTaskAsync(userId, () => GetTaskAsync(photo), CheckTaskHelper.CheckOnTaskOwner);
        }

        private Task<ExecutionResult<FixationType>> CheckOnTaskOwnerAndTaskStatusAsync(Photo photo, Guid? userId)
        {
            return CheckTaskAsync(userId, () => GetTaskAsync(photo), CheckTaskHelper.CheckOnTaskOwnerAndTaskStatus);
        }

        private Task<ExecutionResult<FixationType>> CheckOnTaskOwnerAndTaskStatusAsync(Guid fixationId, Guid? userId)
        {
            return CheckTaskAsync(userId, () => GetTaskAsync(fixationId), CheckTaskHelper.CheckOnTaskOwnerAndTaskStatus);
        }

        private async Task<ExecutionResult<FixationType>> CheckTaskAsync(
            Guid? userId, Func<Task<ExecutionResult<(TaskEntity, FixationType)>>> getTaskAsync, 
            Func<TaskEntity, Guid?, ExecutionResult> checker)
        {
            ExecutionResult<(TaskEntity task, FixationType fixationType)> getTaskResult = await getTaskAsync();
            if (!getTaskResult.TryGetResult(out var task))
            {
                return new() { Errors = getTaskResult.Errors };
            }

            ExecutionResult checkResult = checker(task.task, userId);
            if (checkResult.IsNotSuccess)
            {
                return new() { Errors = checkResult.Errors };
            }

            return task.fixationType;
        }

        private async Task<ExecutionResult<(TaskEntity, FixationType)>> GetTaskAsync(Photo photo)
        {
            if (photo.FixationDefectId is not null)
            {
                FixationDefect? fixationDefect = await _fixationDefectRepository.GetByIdWithTaskAsync(photo.FixationDefectId.Value);
                if (fixationDefect is not null) return (fixationDefect.Task!, FixationType.FixationDefect);
            }
            else if (photo.FixationWorkId is not null)
            {
                FixationWork? fixationWork = await _fixationWorkRepository.GetByIdWithTaskAsync(photo.FixationWorkId.Value);
                if (fixationWork is not null) return (fixationWork.TaskFixationWork!, FixationType.FixationWork);
            }

            return new(StatusCodeExecutionResult.InternalServer, "FixationNotFound", $"Fixation not found!");
        }

        private async Task<ExecutionResult<(TaskEntity, FixationType)>> GetTaskAsync(Guid fixationId)
        {
            FixationDefect? fixationDefect = await _fixationDefectRepository.GetByIdWithTaskAsync(fixationId);
            if (fixationDefect is not null) return (fixationDefect.Task!, FixationType.FixationDefect);

            FixationWork? fixationWork = await _fixationWorkRepository.GetByIdWithTaskAsync(fixationId);
            if (fixationWork is not null) return (fixationWork.TaskFixationWork!, FixationType.FixationWork);

            return new(StatusCodeExecutionResult.NotFound, "FixationNotFound", $"Fixation with id {fixationId} not found!");
        }
    }
}

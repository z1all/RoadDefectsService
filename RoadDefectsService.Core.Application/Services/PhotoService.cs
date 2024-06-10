using RoadDefectsService.Core.Application.DTOs.PhotoService;
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

        public async Task<ExecutionResult<PhotoUploadResponseDTO>> AddPhotoAsync(PhotoDTO addPhoto, Guid ownerId)
        {
            Photo photo = new()
            {
                Id = Guid.NewGuid(),
                OwnerId = ownerId,
                Name = addPhoto.Name,
                Type = addPhoto.Type,
            };

            bool saveResult = await _fileService.SaveFileAsync(photo.PathName, addPhoto.File);
            if (!saveResult)
            {
                return new(StatusCodeExecutionResult.InternalServer, "SavePhotoFail", $"Error while saving a photo!");
            }

            await _photoRepository.AddAsync(photo);

            return new PhotoUploadResponseDTO() { UploadedPhotoId = photo.Id };
        }

        public async Task<ExecutionResult> DeletePhotoAsync(Guid photoId, Guid? ownerId)
        {
            Photo? photo = await _photoRepository.GetByIdAsync(photoId);
            if (photo is null)
            {
                return new(StatusCodeExecutionResult.NotFound, "PhotoNotFound", $"Photo with id {photoId} not found!");
            }

            if (ownerId.HasValue && photo.OwnerId != ownerId.Value)
            {
                return new(StatusCodeExecutionResult.Forbid, "DeletePhotoFail", $"You can't delete a photo because you don't own it!");
            }

            ExecutionResult checkResult = await CheckPhotoReferentsAsync(photo);
            if (checkResult.IsNotSuccess)
            {
                return checkResult;
            }

            bool deleteResult = _fileService.DeleteFile(photo.PathName);
            if (!deleteResult)
            {
                return new(StatusCodeExecutionResult.InternalServer, "DeletePhotoFail", $"Error while deleting a photo!");
            }

            await _photoRepository.DeleteAsync(photo);

            return ExecutionResult.SucceededResult;
        }

        private Task<ExecutionResult> CheckPhotoReferentsAsync(Photo photo)
        {
            if (photo.FixationDefectId is not null)
            {
                return CheckPhotoReferentsAsync(
                    photo.FixationDefectId.Value, 
                    _fixationDefectRepository.GetByIdWithTaskAsync, 
                    fixation => fixation.Task!.TaskStatus);
            }
            else if (photo.FixationWorkId is not null)
            {
                return CheckPhotoReferentsAsync(
                    photo.FixationWorkId.Value, 
                    _fixationWorkRepository.GetByIdWithTaskAsync,
                    fixation => fixation.TaskFixationWork!.TaskStatus);
            }

            return Task.FromResult(ExecutionResult.SucceededResult);
        }

        private async Task<ExecutionResult> CheckPhotoReferentsAsync<TFixation>(Guid fixationId, Func<Guid, Task<TFixation?>> getByIdAsync, Func<TFixation, StatusTask> getStatus)
        {
            TFixation? fixation = await getByIdAsync(fixationId);
            if (fixation is null)
            {
                return new(StatusCodeExecutionResult.NotFound, "FixationNotFound", $"Fixation with id {fixationId} not found!");
            }

            StatusTask status = getStatus(fixation);
            if (status == StatusTask.Completed)
            {
                return new(StatusCodeExecutionResult.BadRequest, "TaskCompleted", "You cannot modify a completed task.");
            }
            else if (status == StatusTask.Created)
            {
                return new(StatusCodeExecutionResult.BadRequest, "TaskNotProcessing", "You cannot change a task that has not been started.");
            }

            return ExecutionResult.SucceededResult;
        }

        public async Task<ExecutionResult<PhotoDTO>> GetPhotoAsync(Guid photoId, Guid? ownerId)
        {
            Photo? photo = await _photoRepository.GetByIdAsync(photoId);
            if (photo is null)
            {
                return new(StatusCodeExecutionResult.NotFound, "PhotoNotFound", $"Photo with id {photoId} not found!");
            }

            if (ownerId.HasValue && photo.OwnerId != ownerId.Value)
            {
                return new(StatusCodeExecutionResult.Forbid, "DeletePhotoFail", $"You can't delete a photo because you don't own it!");
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
    }
}

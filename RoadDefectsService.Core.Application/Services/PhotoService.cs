using RoadDefectsService.Core.Application.DTOs.PhotoService;
using RoadDefectsService.Core.Application.Interfaces.Repositories;
using RoadDefectsService.Core.Application.Interfaces.Services;
using RoadDefectsService.Core.Application.Models;
using RoadDefectsService.Core.Domain.Models;

namespace RoadDefectsService.Core.Application.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IFileService _fileService;
        private readonly IPhotoRepository _photoRepository;

        public PhotoService(IFileService fileService, IPhotoRepository photoRepository)
        {
            _fileService = fileService;
            _photoRepository = photoRepository;
        }

        public async Task<ExecutionResult<PhotoUploadResponseDTO>> AddPhotoAsync(PhotoDTO addPhoto)
        {
            Photo photo = new()
            {
                Id = Guid.NewGuid(),
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

        public async Task<ExecutionResult> DeletePhotoAsync(Guid photoId)
        {
            Photo? photo = await _photoRepository.GetByIdAsync(photoId);
            if (photo is null)
            {
                return new(StatusCodeExecutionResult.NotFound, "PhotoNotFound", $"Photo with id {photoId} not found!");
            }

            bool deleteResult = _fileService.DeleteFile(photo.PathName);
            if (!deleteResult)
            {
                return new(StatusCodeExecutionResult.InternalServer, "DeletePhotoFail", $"Error while deleting a photo!");
            }

            await _photoRepository.DeleteAsync(photo);

            return ExecutionResult.SucceededResult;
        }

        public async Task<ExecutionResult<PhotoDTO>> GetPhotoAsync(Guid photoId)
        {
            Photo? photo = await _photoRepository.GetByIdAsync(photoId);
            if (photo is null)
            {
                return new(StatusCodeExecutionResult.NotFound, "PhotoNotFound", $"Photo with id {photoId} not found!");
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

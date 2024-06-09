using RoadDefectsService.Core.Application.DTOs.PhotoService;
using RoadDefectsService.Core.Application.Interfaces.Repositories;
using RoadDefectsService.Core.Application.Interfaces.Services;
using RoadDefectsService.Core.Application.Models;

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

        public Task<ExecutionResult<PhotoUploadResponseDTO>> AddPhotoAsync(PhotoDTO photo)
        {
            throw new NotImplementedException();
        }

        public Task<ExecutionResult> DeletePhotoAsync(Guid photoId)
        {
            throw new NotImplementedException();
        }

        public Task<ExecutionResult<PhotoDTO>> GetPhotoAsync(Guid photoId)
        {
            throw new NotImplementedException();
        }
    }
}

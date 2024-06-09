using Microsoft.Extensions.Options;
using RoadDefectsService.Core.Application.Configurations.FileStorage;
using RoadDefectsService.Core.Application.Interfaces.Services;

namespace RoadDefectsService.Core.Application.Services
{
    public class FileService : IFileService
    {
        private readonly FileStorageOptions _fileStorageOptions;

        public FileService(IOptions<FileStorageOptions> fileStorageOptions)
        {
            _fileStorageOptions = fileStorageOptions.Value;
        }


    }
}

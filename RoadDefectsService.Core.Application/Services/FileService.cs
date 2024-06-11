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

        public async Task<byte[]?> GetFileAsync(string name)
        {
            string path = Path.Combine(_fileStorageOptions.StoragePath, name);
            if (File.Exists(path))
            {
                return await File.ReadAllBytesAsync(path);
            }

            return null;
        }

        public async Task<bool> SaveFileAsync(string name, byte[] file)
        {
            Directory.CreateDirectory(_fileStorageOptions.StoragePath);
            string path = Path.Combine(_fileStorageOptions.StoragePath, name);
            using (var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                try
                {
                    await fileStream.WriteAsync(file);
                }
                catch
                {
                    return false;
                }
            }

            return true;
        }

        public bool DeleteFile(string name)
        {
            string path = Path.Combine(_fileStorageOptions.StoragePath, name);
            if (!File.Exists(path))
            {
                return false;
            }

            try
            {
                File.Delete(path);
                return true;
            }
            catch
            {
                return false;
            }
        }        
    }
}

namespace RoadDefectsService.Core.Application.Interfaces.Services
{
    public interface IFileService
    {
        Task<bool> SaveFileAsync(string name, byte[] file);
        Task<byte[]?> GetFileAsync(string name);
        bool DeleteFile(string name);  
    }
}

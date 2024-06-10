using RoadDefectsService.Core.Application.DTOs.PhotoService;
using RoadDefectsService.Core.Application.Models;

namespace RoadDefectsService.Core.Application.Interfaces.Services
{
    public interface IPhotoService
    {
        /// <summary>
        /// Возвращает фото в двоичной форме
        /// 
        /// Если ownerId равен null, то не будет проверки на владельца
        /// </summary>
        Task<ExecutionResult<PhotoDTO>> GetPhotoAsync(Guid photoId, Guid? ownerId);

        /// <summary>
        /// Сохраняет фото в нужной директории, которая указана в конфигурации, также сохраняет информацию о фото в бд
        /// </summary>
        Task<ExecutionResult<PhotoUploadResponseDTO>> AddPhotoAsync(PhotoDTO addPhoto, Guid ownerId);

        /// <summary>
        /// Фото можно удалить только в том случае, если он не прикреплено к фиксации дефекта или выполненных работ
        /// 
        /// Если ownerId равен null, то не будет проверки на владельца
        /// </summary>
        Task<ExecutionResult> DeletePhotoAsync(Guid photoId, Guid? ownerId);
    }
}

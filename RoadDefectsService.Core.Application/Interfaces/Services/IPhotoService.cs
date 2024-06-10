using RoadDefectsService.Core.Application.DTOs.PhotoService;
using RoadDefectsService.Core.Application.Models;

namespace RoadDefectsService.Core.Application.Interfaces.Services
{
    public interface IPhotoService
    {
        /// <summary>
        /// Возвращает фото в двоичной форме
        /// 
        /// Если userId равен null, то не будет проверки на владельца
        /// </summary>
        Task<ExecutionResult<PhotoDTO>> GetPhotoAsync(Guid fixationId, Guid photoId, Guid? userId);

        /// <summary>
        /// Сохраняет фото в нужной директории, которая указана в конфигурации, также сохраняет информацию о фото в бд
        /// </summary>
        Task<ExecutionResult<PhotoUploadResponseDTO>> AddPhotoAsync(PhotoDTO addPhoto, Guid fixationId, Guid? userId);

        /// <summary>
        /// Фото можно удалить только в том случае, если он не прикреплено к фиксации дефекта или выполненных работ
        /// 
        /// Если userId равен null, то не будет проверки на владельца
        /// </summary>
        Task<ExecutionResult> DeletePhotoAsync(Guid fixationId, Guid photoId, Guid? userId);
    }
}

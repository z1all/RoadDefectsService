using RoadDefectsService.Core.Application.DTOs.PhotoService;
using RoadDefectsService.Core.Application.Models;

namespace RoadDefectsService.Core.Application.Interfaces.Services
{
    public interface IPhotoService
    {
        /// <summary>
        /// Возвращает фото в двоичной форме
        /// </summary>
        Task<ExecutionResult<PhotoDTO>> GetPhotoAsync(Guid photoId);

        /// <summary>
        /// Сохраняет фото в нужной директории, которая указана в конфигурации, также сохраняет информацию о фото в бд
        /// </summary>
        Task<ExecutionResult<PhotoUploadResponseDTO>> AddPhotoAsync(PhotoDTO addPhoto);

        /// <summary>
        /// Фото можно удалить только в том случае, если он не прикреплено к фиксации дефекта или выполненных работ
        /// </summary>
        Task<ExecutionResult> DeletePhotoAsync(Guid photoId);
    }
}

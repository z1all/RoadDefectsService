using Microsoft.AspNetCore.Mvc;
using RoadDefectsService.Core.Application.DTOs.PhotoService;
using RoadDefectsService.Core.Application.Interfaces.Services;
using RoadDefectsService.Core.Application.Models;
using RoadDefectsService.Presentation.Web.Attributes;
using RoadDefectsService.Presentation.Web.Controllers.Base;
using RoadDefectsService.Presentation.Web.DTOs;

namespace RoadDefectsService.Presentation.Web.Controllers
{
    /// <response code="401">Unauthorized</response>
    [Route("api/photo")]
    [ApiController]
    public class PhotoController : BaseController
    {
        private readonly IPhotoService _photoService;

        public PhotoController(IPhotoService photoService)
        {
            _photoService = photoService;
        }

        /// <summary>
        /// Скачать фото (Не реализовано) (Не все модели указаны)
        /// </summary>
        /// <remarks> 
        /// Доступ: Все
        /// </remarks>
        [HttpGet("{photoId}")]
        [CustomeAuthorize]
        [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DownloadPhoto([FromRoute] Guid photoId)
        {
            ExecutionResult<PhotoDTO> response = await _photoService.GetPhotoAsync(photoId);
            if (response.TryGetResult(out PhotoDTO photo))
            {
                return ExecutionResultHandler(response);
            }


            //if (!fileDTO.Type.TryMapToContentType(out var contentType))
            //{
            //    return ExecutionResultHandler(new ExecutionResult(StatusCodeExecutionResult.InternalServer, "DocumentTypeError", $"Unknown document type {fileDTO.Type}"));
            //}
            return File(photo.File, "contentType!", photo.Name);
        }

        /// <summary>
        /// Удалить фото (Не реализовано) (Не все модели указаны)
        /// </summary>
        /// <remarks> 
        /// Доступ: Все
        /// </remarks>
        /// <response code="204">NoContent</response>
        [HttpDelete("{photoId}")]
        [CustomeAuthorize]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeletePhoto([FromRoute] Guid photoId)
        {
            return await ExecutionResultHandlerAsync(() => _photoService.DeletePhotoAsync(photoId));
        }

        /// <summary>
        /// Загрузить фото (Не реализовано) (Не все модели указаны)
        /// </summary>
        /// <remarks> 
        /// Доступ: Все
        /// </remarks>
        [HttpPost]
        [CustomeAuthorize]
        [ProducesResponseType(typeof(PhotoUploadResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PhotoUploadResponseDTO>> UploadPhoto(PhotoUpload fileUpload)
        {
            return await ExecutionResultHandlerAsync(async applicantId =>
            {
                PhotoDTO photo = new()
                {
                    Name = Path.GetFileNameWithoutExtension(fileUpload.File.FileName),
                    Type = Path.GetExtension(fileUpload.File.FileName),
                    File = await GetFileAsync(fileUpload.File),
                };

                return await _photoService.AddPhotoAsync(photo);
            });
        }

        private static async Task<byte[]> GetFileAsync(IFormFile formFile)
        {
            byte[] file = [];
            using (var stream = new MemoryStream())
            {
                await formFile.CopyToAsync(stream);
                file = stream.ToArray();
            }
            return file;
        }
    }
}

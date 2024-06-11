using Microsoft.AspNetCore.Mvc;
using RoadDefectsService.Core.Application.DTOs.PhotoService;
using RoadDefectsService.Core.Application.Helpers;
using RoadDefectsService.Core.Application.Interfaces.Services;
using RoadDefectsService.Core.Application.Models;
using RoadDefectsService.Presentation.Web.Attributes;
using RoadDefectsService.Presentation.Web.Controllers.Base;
using RoadDefectsService.Presentation.Web.DTOs;
using RoadDefectsService.Presentation.Web.Helpers;

namespace RoadDefectsService.Presentation.Web.Controllers
{
    /// <response code="401">Unauthorized</response>
    [Route("api")]
    [ApiController]
    [SwaggerControllerOrder(Order = 4)]
    public class PhotoController : BaseController
    {
        private readonly IPhotoService _photoService;
        private readonly PhotoTypeHelper _photoTypeHelper;

        public PhotoController(IPhotoService photoService, PhotoTypeHelper photoTypeHelper)
        {
            _photoService = photoService;
            _photoTypeHelper = photoTypeHelper;
        }

        /// <summary>
        /// Скачать фото
        /// </summary>
        /// <remarks> 
        /// Доступ: Все
        /// </remarks>
        [HttpGet("fixation/{fixationId}/photo/{photoId}")]
        [CustomeAuthorize]
        [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DownloadPhoto([FromRoute] Guid fixationId, [FromRoute] Guid photoId)
        {
            if (!HttpContext.TryGetUserId(out Guid userId))
            {
                return ExecutionResultHandler(new ExecutionResult(StatusCodeExecutionResult.InternalServer, "UnknowError", "Unknow error"));
            }

            ExecutionResult<PhotoDTO> response = await _photoService.GetPhotoAsync(fixationId, photoId, userId.GetUserIdIfRoadInspectorOrNull(HttpContext));
            if (!response.TryGetResult(out PhotoDTO photo))
            {
                return ExecutionResultHandler(response);
            }

            if (!_photoTypeHelper.TryMapToContentType(photo.Type, out string? contentType))
            {
                return ExecutionResultHandler(new ExecutionResult(StatusCodeExecutionResult.InternalServer, "DocumentTypeError", $"Unknown document type {photo.Type}"));
            }
            return File(photo.File, contentType!, photo.Name);
        }

        /// <summary>
        /// Удалить фото
        /// </summary>
        /// <remarks> 
        /// Доступ: Все
        /// </remarks>
        /// <response code="204">NoContent</response>
        [HttpDelete("fixation/{fixationId}/photo/{photoId}")]
        [CustomeAuthorize]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeletePhoto([FromRoute] Guid fixationId, [FromRoute] Guid photoId)
        {
            return await ExecutionResultHandlerAsync((userId) =>
            {
                return _photoService.DeletePhotoAsync(fixationId, photoId, userId.GetUserIdIfRoadInspectorOrNull(HttpContext));
            });
        }

        /// <summary>
        /// Загрузить фото
        /// </summary>
        /// <remarks> 
        /// Доступ: Все
        /// </remarks>
        [HttpPost("fixation/{fixationId}/photo")]
        [CustomeAuthorize]
        [ProducesResponseType(typeof(PhotoUploadResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PhotoUploadResponseDTO>> UploadPhoto([FromRoute] Guid fixationId, PhotoUpload fileUpload)
        {
            return await ExecutionResultHandlerAsync(async (userId) =>
            {
                PhotoDTO photo = new()
                {
                    Name = Path.GetFileNameWithoutExtension(fileUpload.Photo.FileName),
                    Type = Path.GetExtension(fileUpload.Photo.FileName),
                    File = await GetFileAsync(fileUpload.Photo),
                };

                return await _photoService.AddPhotoAsync(photo, fixationId, userId.GetUserIdIfRoadInspectorOrNull(HttpContext));
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

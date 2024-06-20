using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RoadDefectsService.Core.Application.Configurations.FileStorage;
using RoadDefectsService.Core.Application.Models;
using RoadDefectsService.Presentation.Web.Controllers.Base;
using RoadDefectsService.Presentation.Web.Helpers;
using Swashbuckle.AspNetCore.Annotations;

namespace RoadDefectsService.Presentation.Web.Controllers
{
    [ApiController]
    public class FileController : BaseController
    {
        private readonly FileStorageOptions _options;
        private readonly PhotoTypeHelper _photoTypeHelper;

        public FileController(IOptions<FileStorageOptions> options, PhotoTypeHelper photoTypeHelper)
        {
            _options = options.Value;
            _photoTypeHelper = photoTypeHelper;
        }

        [HttpGet("{imageName}")]
        [SwaggerIgnore]
        public IActionResult GetImage(string imageName)
        {
            var imagePath = Path.Combine(_options.StoragePath, imageName);
            if (!System.IO.File.Exists(imagePath))
            {
                return NotFound();
            }

            if (imageName.Contains("/") || imageName.Contains("\\") || imageName.Contains(".."))
            {
                return ExecutionResultHandler(new ExecutionResult(StatusCodeExecutionResult.Forbid, "AccessIsDenied", $"Access is denied"));
            }

            FileStream imageFileStream = new(imagePath, FileMode.Open, FileAccess.Read);

            var fileType = "." + Path.GetExtension(imageName).TrimStart('.').ToLower();
            if (!_photoTypeHelper.TryMapToContentType(fileType, out var contentType))
            {
                return ExecutionResultHandler(new ExecutionResult(StatusCodeExecutionResult.InternalServer, "FileTypeError", $"Unknown file type {fileType}"));
            }

            return File(imageFileStream, contentType!);
        }
    }
}

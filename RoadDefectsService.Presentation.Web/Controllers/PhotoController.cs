using Microsoft.AspNetCore.Mvc;
using RoadDefectsService.Presentation.Web.Attributes;

namespace RoadDefectsService.Presentation.Web.Controllers
{
    [Route("api/photo")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        /// <summary>
        /// Скачать фото (Не реализовано) (Не все модели указаны)
        /// </summary>
        /// <remarks> 
        /// Доступ: Все
        /// </remarks>
        [HttpGet("{photoId}")]
        [CustomeAuthorize]
        public async Task<ActionResult> DownloadPhoto([FromRoute] Guid photoId)
        {
            return Ok();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using RoadDefectsService.Presentation.Web.Attributes;
using RoadDefectsService.Presentation.Web.Controllers.Base;

namespace RoadDefectsService.Presentation.Web.Controllers
{
    [Route("api/fixation_work")]
    [ApiController]
    public class FixationWorkController : BaseController
    {
        /// <summary>
        /// Фиксация выполненных работ (Не реализовано) (Не все модели указаны)
        /// </summary>
        /// <remarks> 
        /// Доступ: Все
        /// </remarks>
        [HttpGet("{fixationWorkId}")]
        [CustomeAuthorize]
        public async Task<ActionResult> GetFixationWork([FromRoute] Guid fixationWorkId)
        {  
            return Ok();
        }
    }
}

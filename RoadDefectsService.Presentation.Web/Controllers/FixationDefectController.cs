using Microsoft.AspNetCore.Mvc;
using RoadDefectsService.Presentation.Web.Attributes;

namespace RoadDefectsService.Presentation.Web.Controllers
{
    [Route("api/fixation_defect")]
    [ApiController]
    public class FixationDefectController : ControllerBase
    {
        /// <summary>
        /// Фиксация дефекта (Не реализовано) (Не все модели указаны)
        /// </summary>
        /// <remarks> 
        /// Доступ: Все
        /// </remarks>
        [HttpGet("{fixationDefectId}")]
        [CustomeAuthorize]
        public async Task<ActionResult> GetFixationDefect([FromRoute] Guid fixationDefectId)
        {
            return Ok();
        }
    }
}

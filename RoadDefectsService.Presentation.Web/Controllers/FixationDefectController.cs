using Microsoft.AspNetCore.Mvc;
using RoadDefectsService.Core.Application.DTOs.FixationService;
using RoadDefectsService.Presentation.Web.Attributes;
using RoadDefectsService.Presentation.Web.DTOs;

namespace RoadDefectsService.Presentation.Web.Controllers
{
    /// <response code="401">Unauthorized</response>
    /// <response code="403">Forbidden</response>
    [Route("api/fixation_defect")]
    [ApiController]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public class FixationDefectController : ControllerBase
    {
        /// <summary>
        /// Фиксация дефекта (Не реализовано)
        /// </summary>
        /// <remarks> 
        /// Доступ: Все
        /// </remarks>
        [HttpGet("{fixationDefectId}")]
        [CustomeAuthorize]
        [ProducesResponseType(typeof(FixationDefectDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FixationDefectDTO>> GetFixationDefect([FromRoute] Guid fixationDefectId)
        {
            return Ok();
        }

        /// <summary>
        /// Удаление фиксации дефекта (Не реализовано)
        /// </summary>
        /// <remarks> 
        /// Доступ: Все
        /// </remarks>
        /// <response code="204">No Content</response> 
        [HttpDelete("{fixationDefectId}")]
        [CustomeAuthorize]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteFixationDefect([FromRoute] Guid fixationDefectId)
        {
            return NoContent();
        }

        /// <summary>
        /// Зафиксировать дефект (Не реализовано)
        /// </summary>
        /// <remarks> 
        /// Доступ: Все
        /// </remarks>
        /// <response code="204">No Content</response> 
        [HttpPost]
        [CustomeAuthorize]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateFixationDefect([FromBody] CreateFixationDefectDTO createFixationDefect)
        {
            return NoContent();
        }

        /// <summary>
        /// Редактирование фиксации дефекта (Не реализовано)
        /// </summary>
        /// <remarks> 
        /// Доступ: Все
        /// </remarks>
        /// <response code="204">No Content</response> 
        [HttpPut("{fixationDefectId}")]
        [CustomeAuthorize]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> ChangeFixationDefect([FromRoute] Guid fixationDefectId, [FromBody] EditFixationDefectDTO editFixationDefect)
        {
            return NoContent();
        }
    }
}

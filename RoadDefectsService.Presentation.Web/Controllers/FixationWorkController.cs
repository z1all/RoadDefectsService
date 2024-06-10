using Microsoft.AspNetCore.Mvc;
using RoadDefectsService.Core.Application.DTOs.FixationService;
using RoadDefectsService.Presentation.Web.Attributes;
using RoadDefectsService.Presentation.Web.Controllers.Base;
using RoadDefectsService.Presentation.Web.DTOs;

namespace RoadDefectsService.Presentation.Web.Controllers
{
    /// <response code="401">Unauthorized</response>
    /// <response code="403">Forbidden</response>
    [Route("api/fixation_work")]
    [ApiController]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public class FixationWorkController : BaseController
    {
        /// <summary>
        /// Фиксация выполненных работ (Не реализовано) 
        /// </summary>
        /// <remarks> 
        /// Доступ: Все
        /// </remarks>
        [HttpGet("{fixationWorkId}")]
        [CustomeAuthorize]
        [ProducesResponseType(typeof(FixationWorkDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FixationWorkDTO>> GetFixationWork([FromRoute] Guid fixationWorkId)
        {  
            return Ok();
        }

        /// <summary>
        /// Удаление фиксации выполненных работ (Не реализовано)
        /// </summary>
        /// <remarks> 
        /// Доступ: Все
        /// </remarks>
        /// <response code="204">No Content</response> 
        [HttpDelete("{fixationWorkId}")]
        [CustomeAuthorize]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteFixationWork([FromRoute] Guid fixationWorkId)
        {
            return NoContent();
        }

        /// <summary>
        /// Зафиксировать выполнение работ (Не реализовано)
        /// </summary>
        /// <remarks> 
        /// Доступ: Все
        /// </remarks>
        /// <response code="204">No Content</response> 
        [HttpPost]
        [CustomeAuthorize]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateFixationDefect([FromBody] CreateFixationWorkDTO createFixationWork)
        {
            return NoContent();
        }

        /// <summary>
        /// Редактирование фиксации выполненных работ (Не реализовано)
        /// </summary>
        /// <remarks> 
        /// Доступ: Все
        /// </remarks>
        /// <response code="204">No Content</response> 
        [HttpPut("{fixationWorkId}")]
        [CustomeAuthorize]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> ChangeFixationDefect([FromRoute] Guid fixationWorkId, [FromBody] EditFixationWorkDTO editFixationWork)
        {
            return NoContent();
        }
    }
}

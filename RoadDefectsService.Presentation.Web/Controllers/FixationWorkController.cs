using Microsoft.AspNetCore.Mvc;
using RoadDefectsService.Core.Application.DTOs.FixationService;
using RoadDefectsService.Core.Application.Interfaces.Services;
using RoadDefectsService.Presentation.Web.Attributes;
using RoadDefectsService.Presentation.Web.Controllers.Base;
using RoadDefectsService.Presentation.Web.DTOs;
using RoadDefectsService.Core.Application.Helpers;

namespace RoadDefectsService.Presentation.Web.Controllers
{
    /// <response code="401">Unauthorized</response>
    /// <response code="403">Forbidden</response>
    [Route("api/fixation_work")]
    [ApiController]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    [SwaggerControllerOrder(Order = 7)]
    public class FixationWorkController : BaseController
    {
        private readonly IFixationWorkService _fixationWorkService;

        public FixationWorkController(IFixationWorkService fixationWorkService)
        {
            _fixationWorkService = fixationWorkService;
        }

        /// <summary>
        /// Фиксация выполненных работ
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
            return await ExecutionResultHandlerAsync((userId) =>
                _fixationWorkService.GetFixationWorkAsync(fixationWorkId, userId.GetUserIdIfRoadInspectorOrNull(HttpContext)));
        }

        /// <summary>
        /// Удаление фиксации выполненных работ
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
            return await ExecutionResultHandlerAsync((userId) =>
                _fixationWorkService.DeleteFixationWorkAsync(fixationWorkId, userId.GetUserIdIfRoadInspectorOrNull(HttpContext)));
        }

        /// <summary>
        /// Создать фиксацию выполненных работ
        /// </summary>
        /// <remarks> 
        /// Доступ: Все
        /// </remarks>
        /// <response code="204">No Content</response> 
        [HttpPost]
        [CustomeAuthorize]
        [ProducesResponseType(typeof(CreateFixationResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreateFixationResponseDTO>> CreateFixationDefect([FromBody] CreateFixationWorkDTO createFixationWork)
        {
            return await ExecutionResultHandlerAsync((userId) =>
                _fixationWorkService.CreateFixationWorkAsync(createFixationWork, userId.GetUserIdIfRoadInspectorOrNull(HttpContext)));
        }

        /// <summary>
        /// Редактирование фиксации выполненных работ
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
            return await ExecutionResultHandlerAsync((userId) =>
                _fixationWorkService.ChangeFixationWorkAsync(editFixationWork, fixationWorkId, userId.GetUserIdIfRoadInspectorOrNull(HttpContext)));
        }
    }
}

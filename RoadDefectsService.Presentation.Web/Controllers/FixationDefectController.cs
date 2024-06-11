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
    [Route("api/fixation_defect")]
    [ApiController]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    [SwaggerControllerOrder(Order = 6)]
    public class FixationDefectController : BaseController
    {
        private readonly IFixationDefectService _fixationDefectService;

        public FixationDefectController(IFixationDefectService fixationDefectService)
        {
            _fixationDefectService = fixationDefectService;
        }

        /// <summary>
        /// Фиксация дефекта
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
            return await ExecutionResultHandlerAsync((userId) =>
                _fixationDefectService.GetFixationDefectAsync(fixationDefectId, userId.GetUserIdIfRoadInspectorOrNull(HttpContext)));
        }

        /// <summary>
        /// Удаление фиксации дефектаs
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
            return await ExecutionResultHandlerAsync((userId) =>
                _fixationDefectService.DeleteFixationDefectAsync(fixationDefectId, userId.GetUserIdIfRoadInspectorOrNull(HttpContext)));
        }

        /// <summary>
        /// Создать фиксацию дефекта
        /// </summary>
        /// <remarks> 
        /// Доступ: Все
        /// </remarks>
        /// <response code="204">No Content</response> 
        [HttpPost]
        [CustomeAuthorize]
        [ProducesResponseType(typeof(CreateFixationResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreateFixationResponseDTO>> CreateFixationDefect([FromBody] CreateFixationDefectDTO createFixationDefect)
        {
            return await ExecutionResultHandlerAsync((userId) =>
               _fixationDefectService.CreateFixationDefectAsync(createFixationDefect, userId.GetUserIdIfRoadInspectorOrNull(HttpContext)));
        }

        /// <summary>
        /// Редактирование фиксации дефекта
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
            return await ExecutionResultHandlerAsync((userId) =>
               _fixationDefectService.ChangeFixationDefectAsync(editFixationDefect, fixationDefectId, userId.GetUserIdIfRoadInspectorOrNull(HttpContext)));
        }
    }
}

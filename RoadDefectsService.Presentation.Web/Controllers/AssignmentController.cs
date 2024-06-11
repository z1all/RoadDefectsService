using Microsoft.AspNetCore.Mvc;
using RoadDefectsService.Core.Application.DTOs.AssignmentService;
using RoadDefectsService.Core.Domain.Enums;
using RoadDefectsService.Presentation.Web.Attributes;
using RoadDefectsService.Presentation.Web.Controllers.Base;
using RoadDefectsService.Presentation.Web.DTOs;

namespace RoadDefectsService.Presentation.Web.Controllers
{
    /// <response code="401">Unauthorized</response>
    /// <response code="403">Forbidden</response>
    [Route("api/assignment")]
    [ApiController]
    [CustomeAuthorize(Roles = Role.RoadInspector)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public class AssignmentController : BaseController
    {
        /// <summary>
        /// Поручения на выполнения работ (Не реализовано)
        /// </summary>
        /// <remarks> Доступ: Дорожный инспектор </remarks>
        [HttpGet("assignments")]
        [ProducesResponseType(typeof(AssignmentPagedDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult<AssignmentPagedDTO>> GetAssignments([FromQuery] AssignmentFilterDTO assignmentFilter)
        {
            return Ok();
        }

        /// <summary>
        /// Поручение на выполнение работ (Не реализовано)
        /// </summary>
        /// <remarks> Доступ: Дорожный инспектор </remarks>
        [HttpGet("{assignmentId}")]
        [ProducesResponseType(typeof(AssignmentDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AssignmentDTO>> GetAssignment([FromRoute] Guid assignmentId)
        {
            return Ok();
        }

        /// <summary>
        /// Создать поручение на выполнение работ (Не реализовано)
        /// </summary>
        /// <remarks> Доступ: Дорожный инспектор </remarks>
        /// <response code="204">NoContent</response> 
        [HttpPost]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateAssignment([FromBody] CreateAssignmentDTO contractor)
        {
            return Ok();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using RoadDefectsService.Core.Application.DTOs.AssignmentService;
using RoadDefectsService.Core.Application.Helpers;
using RoadDefectsService.Core.Application.Interfaces.Services;
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
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    [SwaggerControllerOrder(Order = 5)]
    public class AssignmentController : BaseController
    {
        private readonly IAssignmentService _assignmentService;

        public AssignmentController(IAssignmentService assignmentService)
        {
            _assignmentService = assignmentService;
        }

        /// <summary>
        /// Поручения на выполнения работ
        /// </summary>
        /// <remarks> Доступ: Оператор и админ </remarks>
        [HttpGet("assignments")]
        [CustomeAuthorize(Roles = Role.Operator)]
        [ProducesResponseType(typeof(AssignmentPagedDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult<AssignmentPagedDTO>> GetAssignments([FromQuery] AssignmentFilterDTO assignmentFilter)
        {
            return await ExecutionResultHandlerAsync(() => _assignmentService.GetAssignmentsAsync(assignmentFilter));
        }

        /// <summary>
        /// Поручение на выполнение работ
        /// </summary>
        /// <remarks> Доступ: Оператор и админ </remarks>
        [HttpGet("{assignmentId}")]
        [CustomeAuthorize(Roles = Role.Operator)]
        [ProducesResponseType(typeof(AssignmentDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AssignmentDTO>> GetAssignment([FromRoute] Guid assignmentId)
        {
            return await ExecutionResultHandlerAsync(() => _assignmentService.GetAssignmentAsync(assignmentId));
        }

        /// <summary>
        /// Создать поручение на выполнение работ
        /// </summary>
        /// <remarks> Доступ: Дорожный инспектор </remarks>
        /// <response code="204">NoContent</response> 
        [HttpPost]
        [CustomeAuthorize(Roles = Role.RoadInspector)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateAssignment([FromBody] CreateAssignmentDTO createAssignment)
        {
            return await ExecutionResultHandlerAsync((userId)
                => _assignmentService.CreateAssignmentAsync(createAssignment, userId.GetUserIdIfRoadInspectorOrNull(HttpContext)));

        }
    }
}

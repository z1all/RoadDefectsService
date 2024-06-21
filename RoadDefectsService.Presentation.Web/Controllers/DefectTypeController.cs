using MediatR;
using Microsoft.AspNetCore.Mvc;
using RoadDefectsService.Core.Application.CQRS.DefectType.DTOs;
using RoadDefectsService.Core.Application.CQRS.DefectType.Queries;
using RoadDefectsService.Presentation.Web.Attributes;
using RoadDefectsService.Presentation.Web.Controllers.Base;
using RoadDefectsService.Presentation.Web.DTOs;

namespace RoadDefectsService.Presentation.Web.Controllers
{
    /// <response code="401">Unauthorized</response>
    [Route("api/defect_type")]
    [ApiController]
    [SwaggerControllerOrder(Order = 5)]
    public class DefectTypeController : BaseController
    {
        private readonly IMediator _mediator;

        public DefectTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Типы дефектов
        /// </summary>
        /// <remarks> 
        /// Доступ: Все
        /// </remarks>
        [HttpGet]
        [CustomeAuthorize]
        [ProducesResponseType(typeof(List<DefectTypeDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<DefectTypeDTO>>> GetDefectTypes([FromQuery] DefectTypeFilterDTO defectTypeFilter)
        {
            return await ExecutionResultHandlerAsync(()
                => _mediator.Send(new GetDefectTypesByFilterQuery() { DefectTypeFilter = defectTypeFilter }));
        }
    }
}

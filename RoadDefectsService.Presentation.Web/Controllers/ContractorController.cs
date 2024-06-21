using MediatR;
using Microsoft.AspNetCore.Mvc;
using RoadDefectsService.Core.Application.CQRS.Contractor.Commands;
using RoadDefectsService.Core.Application.CQRS.Contractor.DTOs;
using RoadDefectsService.Core.Application.CQRS.Contractor.Queries;
using RoadDefectsService.Core.Domain.Enums;
using RoadDefectsService.Presentation.Web.Attributes;
using RoadDefectsService.Presentation.Web.Controllers.Base;
using RoadDefectsService.Presentation.Web.DTOs;

namespace RoadDefectsService.Presentation.Web.Controllers
{
    /// <response code="401">Unauthorized</response>
    /// <response code="403">Forbidden</response>
    [Route("api/contractor")]
    [ApiController]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    [SwaggerControllerOrder(Order = 3)]
    public class ContractorController : BaseController
    {
        private readonly IMediator _mediator;

        public ContractorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Все подрядчики
        /// </summary>
        /// <remarks> Доступ: Все </remarks>
        [HttpGet("contractors")]
        [CustomeAuthorize]
        [ProducesResponseType(typeof(ContractorPagedDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult<ContractorPagedDTO>> GetContractors([FromQuery] ContractorFilterDTO contractorFilter)
        {
            return await ExecutionResultHandlerAsync(() => _mediator.Send(new GetContractorsByFiltersQuery() { ContractorFilter = contractorFilter}));
        }

        /// <summary>
        /// Конкретный подрядчик
        /// </summary>
        /// <remarks> Доступ: Оператор и админ </remarks>
        [HttpGet("{contractorId}")]
        [CustomeAuthorize(Roles = Role.Operator)]
        [ProducesResponseType(typeof(ContractorDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ContractorDTO>> GetContractor([FromRoute] Guid contractorId)
        {
            return await ExecutionResultHandlerAsync(() => _mediator.Send(new GetContractorByIdQuery() { ContractorId = contractorId }));
        }

        /// <summary>
        /// Создать подрядчика
        /// </summary>
        /// <remarks> Доступ: Оператор и админ </remarks>
        /// <response code="204">NoContent</response> 
        [HttpPost]
        [CustomeAuthorize(Roles = Role.Operator)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateContractor([FromBody] CreateContractorDTO contractor)
        {
            return await ExecutionResultHandlerAsync(() => _mediator.Send(new CreateContractorCommand() { CreateContractor = contractor }));
        }

        /// <summary>
        /// Редактировать информацию подрядчика
        /// </summary>
        /// <remarks> Доступ: Оператор и админ </remarks>
        /// <response code="204">NoContent</response> 
        [HttpPut("{contractorId}")]
        [CustomeAuthorize(Roles = Role.Operator)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> ChangeContractor([FromRoute] Guid contractorId, [FromBody] EditContractorDTO contractor)
        {
            return await ExecutionResultHandlerAsync(() => _mediator.Send(new EditContractorCommand() { ContractorId = contractorId, EditContractor = contractor }));
        }

        /// <summary>
        /// Удалить подрядчика
        /// </summary>
        /// <remarks> Доступ: Оператор и админ </remarks>
        /// <response code="204">NoContent</response> 
        [HttpDelete("{contractorId}")]
        [CustomeAuthorize(Roles = Role.Operator)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteContractor([FromRoute] Guid contractorId)
        {
            return await ExecutionResultHandlerAsync(() => _mediator.Send(new DeleteContractorCommand() { ContractorId = contractorId }));
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoadDefectsService.Core.Application.DTOs;
using RoadDefectsService.Core.Domain.Enums;
using RoadDefectsService.Presentation.Web.DTOs;

namespace RoadDefectsService.Presentation.Web.Controllers
{
    /// <response code="401">Unauthorized</response>
    /// <response code="403">Forbidden</response>
    [Route("api/contractor")]
    [ApiController]
    [Authorize(Roles = Role.Operator)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public class ContractorController : ControllerBase
    {
        /// <summary>
        /// Все подрядчики (Не реализовано)
        /// </summary>
        /// <remarks> Доступ: Оператор и админ </remarks>
        [HttpGet("contractors")]
        [ProducesResponseType(typeof(List<ContractorDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ContractorDTO>>> GetСontractors()
        {
            return Ok();
        }

        /// <summary>
        /// Конкретный подрядчик (Не реализовано)
        /// </summary>
        /// <remarks> Доступ: Оператор и админ </remarks>
        [HttpGet("{contractorId}")]
        [ProducesResponseType(typeof(ContractorDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ContractorDTO>> GetСontractor([FromRoute] Guid contractorId)
        {
            return Ok();
        }

        /// <summary>
        /// Создать подрядчика (Не реализовано)
        /// </summary>
        /// <remarks> Доступ: Оператор и админ </remarks>
        /// <response code="204">NoContent</response> 
        [HttpPost]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateСontractor([FromBody] CreateContractorDTO contractor)
        {
            return NoContent();
        }

        /// <summary>
        /// Редактировать информацию подрядчика (Не реализовано)
        /// </summary>
        /// <remarks> Доступ: Оператор и админ </remarks>
        /// <response code="204">NoContent</response> 
        [HttpPut("{contractorId}")]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> ChangeСontractor([FromRoute] Guid contractorId, [FromBody] CreateContractorDTO contractor)
        {
            return NoContent();
        }
    }
}

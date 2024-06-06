using Microsoft.AspNetCore.Mvc;
using RoadDefectsService.Core.Application.DTOs.TaskService;
using RoadDefectsService.Core.Domain.Enums;
using RoadDefectsService.Presentation.Web.Attributes;
using RoadDefectsService.Presentation.Web.DTOs;

namespace RoadDefectsService.Presentation.Web.Controllers
{
    [Route("api/fixation_defect")]
    [ApiController]
    public class TaskFixationDefectController : ControllerBase
    {
        /// <summary>
        /// Посмотреть задачу (Не реализовано)
        /// </summary>
        /// <remarks> Доступ: Все </remarks>
        [HttpGet("{taskId}")]
        [CustomeAuthorize]
        [ProducesResponseType(typeof(FixationDefectTaskDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FixationDefectTaskDTO>> GetFixationDefectTask([FromRoute] Guid taskId)
        {
            return Ok();
        }

        /// <summary>
        /// Редактировать задачу (Не реализовано)
        /// </summary> 
        /// <remarks> Доступ: Оператор и админ </remarks>
        [HttpPut("{taskId}")]
        [CustomeAuthorize(Roles = Role.Operator)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> ChangeFixationDefectTask([FromRoute] Guid taskId, [FromBody] CreateEditFixationDefectTaskDTO editFixationDefect)
        {
            return NoContent();
        }

        /// <summary>
        /// Создать задачу (Не реализовано) 
        /// </summary>
        /// <remarks> Доступ: Оператор и админ </remarks>
        [HttpPost]
        [CustomeAuthorize(Roles = Role.Operator)]
        [ProducesResponseType(typeof(CreateTaskResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreateTaskResponseDTO>> CreateFixationDefectTask([FromBody] CreateEditFixationDefectTaskDTO createFixationDefect)
        {
            return Ok();
        }
    }
}

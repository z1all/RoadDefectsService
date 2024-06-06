using Microsoft.AspNetCore.Mvc;
using RoadDefectsService.Core.Application.DTOs.TaskService;
using RoadDefectsService.Core.Domain.Enums;
using RoadDefectsService.Presentation.Web.Attributes;
using RoadDefectsService.Presentation.Web.DTOs;

namespace RoadDefectsService.Presentation.Web.Controllers
{
    [Route("api/fixation_work")]
    [ApiController]
    public class TaskFixationWorkController : ControllerBase
    {
        /// <summary>
        /// Посмотреть задачу по фиксации выполненных работ (Не реализовано) 
        /// </summary>
        /// <remarks> Доступ: Все </remarks>
        [HttpGet("{taskId}")]
        [CustomeAuthorize]
        [ProducesResponseType(typeof(FixationWorkTaskDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FixationWorkTaskDTO>> GetFixationWorkTask([FromRoute] Guid taskId)
        {
            return Ok();
        }

        /// <summary>
        /// Редактировать задачу по фиксации выполненных работ (Не реализовано) (Не все модели указаны)
        /// </summary> 
        /// <remarks> Доступ: Оператор и админ </remarks>
        [HttpPut("{taskId}")]
        [CustomeAuthorize(Roles = Role.Operator)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> ChangeFixationWorkTask([FromRoute] Guid taskId, [FromBody] EditFixationWorkTaskDTO editFixationWork)
        {
            return NoContent();
        }

        /// <summary>
        /// Создать задачу по фиксации выполненных работ (Не реализовано)
        /// </summary>
        /// <remarks> Доступ: Оператор и админ </remarks>
        [HttpPost]
        [CustomeAuthorize(Roles = Role.Operator)]
        [ProducesResponseType(typeof(CreateTaskResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreateTaskResponseDTO>> CreateFixationWorkTask([FromBody] CreateFixationWorkTaskDTO createFixationWork)
        {
            return Ok();
        }
    }
}

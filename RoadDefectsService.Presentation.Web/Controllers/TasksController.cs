using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoadDefectsService.Presentation.Web.Controllers.Base;
using RoadDefectsService.Core.Domain.Enums;
using RoadDefectsService.Core.Application.DTOs;
using RoadDefectsService.Presentation.Web.DTOs;
using RoadDefectsService.Presentation.Web.Attributes;

namespace RoadDefectsService.Presentation.Web.Controllers
{
    /// <response code="401">Unauthorized</response>
    /// <response code="403">Forbidden</response>
    [Route("api/task")]
    [ApiController]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public class TasksController : BaseController
    {
        /// <summary>
        /// Посмотреть список задач со статусами (Не реализовано)
        /// </summary>
        /// <remarks> Доступ: Оператор и админ </remarks>
        [HttpGet("tasks")]
        [ProducesResponseType(typeof(List<TaskDTO>), StatusCodes.Status200OK)]
        [CustomeAuthorize(Roles = Role.Operator)]
        public async Task<ActionResult<List<TaskDTO>>> GetTasks()
        {
            return Ok();
        }

        /// <summary>
        /// Посмотреть задачу (Не реализовано)
        /// </summary>
        /// <remarks> Доступ: Все </remarks>
        [HttpGet("fixation_defect/{taskId}")]
        [ProducesResponseType(typeof(FixationDefectTaskDTO), StatusCodes.Status200OK)]
        [CustomeAuthorize]
        public async Task<ActionResult<FixationDefectTaskDTO>> GetFixationDefectTask([FromRoute] Guid taskId)
        {
            return Ok();
        }

        /// <summary>
        /// Редактировать задачу (Не реализовано) (Не все модели указаны)
        /// </summary> 
        /// <remarks> Доступ: Оператор и админ </remarks>
        [HttpPut("fixation_defect/{taskId}")]
        [CustomeAuthorize(Roles = Role.Operator)]
        public async Task<ActionResult> ChangeFixationDefectTask([FromRoute] Guid taskId, [FromBody] CreateFixationDefectTaskDTO fixationTask)
        {
            return NoContent();
        }

        /// <summary>
        /// Создать задачу (Не реализовано) (Не все модели указаны)
        /// </summary>
        /// <remarks> Доступ: Оператор и админ </remarks>
        [HttpPost("fixation_defect")]
        [CustomeAuthorize(Roles = Role.Operator)]
        public async Task<ActionResult> CreateFixationDefectTask([FromBody] CreateFixationDefectTaskDTO fixationTask)
        {
            return NoContent();
        }

        /// <summary>
        /// Посмотреть задачу по фиксации выполненных работ (Не все модели указаны)
        /// </summary>
        /// <remarks> Доступ: Все </remarks>
        [HttpGet("fixation_work/{taskId}")]
        [CustomeAuthorize]
        public async Task<ActionResult> GetFixationWorkTask([FromRoute] Guid taskId)
        {
            return Ok();
        }

        /// <summary>
        /// Редактировать задачу по фиксации выполненных работ (Не реализовано) (Не все модели указаны)
        /// </summary> 
        /// <remarks> Доступ: Оператор и админ </remarks>
        [HttpPut("fixation_work/{taskId}")]
        [CustomeAuthorize(Roles = Role.Operator)]
        public async Task<ActionResult> ChangeFixationWorkTask([FromRoute] Guid taskId)
        {
            return NoContent();
        }

        /// <summary>
        /// Создать задачу по фиксации выполненных работ (Не реализовано) (Не все модели указаны)
        /// </summary>
        /// <remarks> Доступ: Оператор и админ </remarks>
        [HttpPost("fixation_work")]
        [CustomeAuthorize(Roles = Role.Operator)]
        public async Task<ActionResult> CreateFixationWorkTask()
        {
            return NoContent();
        }


        /// <summary>
        /// Задачи дорожного инспектора (Не реализовано)
        /// </summary>
        /// <remarks> Доступ: Оператор и админ </remarks>
        [HttpGet("inspector/{inspectorId}")]
        [ProducesResponseType(typeof(List<TaskDTO>), StatusCodes.Status200OK)]
        [CustomeAuthorize(Roles = Role.Operator)]
        public async Task<ActionResult<List<TaskDTO>>> GetInspectorTasks([FromRoute] Guid inspectorId)
        {
            return Ok();
        }

        /// <summary>
        /// Назначить задачу дорожному инспектору (Не реализовано)
        /// </summary>
        /// <remarks> Доступ: Оператор и админ </remarks>
        /// <response code="204">No Content</response> 
        [HttpPost("inspector/{inspectorId}")]
        [CustomeAuthorize(Roles = Role.Operator)]
        public async Task<ActionResult> AppointTask([FromRoute] Guid inspectorId, [FromBody] AppointTaskDTO appointTask)
        {
            return NoContent();
        }

        /// <summary>
        /// Личные задачи (Не реализовано)
        /// </summary>
        /// <remarks> Доступ: Дорожный инспектор </remarks>
        [HttpGet("own")]
        [CustomeAuthorize(Roles = Role.RoadInspector)]
        [ProducesResponseType(typeof(List<TaskDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<TaskDTO>>> GetOwnTask()
        {
            return NoContent();
        }
    }
}

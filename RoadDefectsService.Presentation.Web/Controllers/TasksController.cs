using Microsoft.AspNetCore.Mvc;
using RoadDefectsService.Presentation.Web.Controllers.Base;
using RoadDefectsService.Core.Domain.Enums;
using RoadDefectsService.Core.Application.DTOs;
using RoadDefectsService.Presentation.Web.DTOs;
using RoadDefectsService.Presentation.Web.Attributes;
using RoadDefectsService.Core.Application.DTOs.TaskService;

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
        [ProducesResponseType(typeof(TaskPagedDTO), StatusCodes.Status200OK)]
        [CustomeAuthorize(Roles = Role.Operator)]
        public async Task<ActionResult<TaskPagedDTO>> GetTasks([FromQuery] CommonTaskFiler taskFilter)
        {
            return Ok();
        }

        /// <summary>
        /// Посмотреть задачу (Не реализовано)
        /// </summary>
        /// <remarks> Доступ: Все </remarks>
        [HttpGet("fixation_defect/{taskId}")]
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
        [HttpPut("fixation_defect/{taskId}")]
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
        [HttpPost("fixation_defect")]
        [CustomeAuthorize(Roles = Role.Operator)]
        [ProducesResponseType(typeof(CreateTaskResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreateTaskResponseDTO>> CreateFixationDefectTask([FromBody] CreateEditFixationDefectTaskDTO createFixationDefect)
        {
            return Ok();
        }

        /// <summary>
        /// Посмотреть задачу по фиксации выполненных работ (Не реализовано) 
        /// </summary>
        /// <remarks> Доступ: Все </remarks>
        [HttpGet("fixation_work/{taskId}")]
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
        [HttpPut("fixation_work/{taskId}")]
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
        [HttpPost("fixation_work")]
        [CustomeAuthorize(Roles = Role.Operator)]
        [ProducesResponseType(typeof(CreateTaskResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreateTaskResponseDTO>> CreateFixationWorkTask([FromBody] CreateFixationWorkTaskDTO createFixationWork)
        {
            return Ok();
        }


        /// <summary>
        /// Задачи дорожного инспектора (Не реализовано)
        /// </summary>
        /// <remarks> Доступ: Оператор и админ </remarks>
        [HttpGet("inspector/{inspectorId}")]
        [CustomeAuthorize(Roles = Role.Operator)]
        [ProducesResponseType(typeof(TaskPagedDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TaskPagedDTO>> GetInspectorTasks([FromRoute] Guid inspectorId, [FromQuery] TaskFilterDTO taskFilter)
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
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
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
        [ProducesResponseType(typeof(TaskPagedDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult<TaskPagedDTO>> GetOwnTask([FromQuery] TaskFilterDTO taskFilter)
        {
            return NoContent();
        }
    }
}

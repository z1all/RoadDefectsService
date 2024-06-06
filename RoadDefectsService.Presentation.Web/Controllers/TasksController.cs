using Microsoft.AspNetCore.Mvc;
using RoadDefectsService.Presentation.Web.Controllers.Base;
using RoadDefectsService.Core.Domain.Enums;
using RoadDefectsService.Presentation.Web.DTOs;
using RoadDefectsService.Presentation.Web.Attributes;
using RoadDefectsService.Core.Application.DTOs.TaskService;
using RoadDefectsService.Core.Application.Interfaces.Services;

namespace RoadDefectsService.Presentation.Web.Controllers
{
    /// <response code="401">Unauthorized</response>
    /// <response code="403">Forbidden</response>
    [Route("api/task")]
    [ApiController]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public class TasksController : BaseController
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        /// <summary>
        /// Посмотреть список задач со статусами (Не реализовано)
        /// </summary>
        /// <remarks> Доступ: Оператор и админ </remarks>
        [HttpGet("tasks")]
        [ProducesResponseType(typeof(TaskPagedDTO), StatusCodes.Status200OK)]
        [CustomeAuthorize(Roles = Role.Operator)]
        public async Task<ActionResult<TaskPagedDTO>> GetTasks([FromQuery] CommonTaskFilterDTO taskFilter)
        {
            return await ExecutionResultHandlerAsync(() => _taskService.GetTasksAsync(taskFilter));
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
            return await ExecutionResultHandlerAsync(() => _taskService.GetInspectorTasksAsync(taskFilter, inspectorId));
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
            return await ExecutionResultHandlerAsync(() => _taskService.AppointTaskAsync(appointTask.TaskId, inspectorId));
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
            return await ExecutionResultHandlerAsync((inspectorId) => _taskService.GetInspectorTasksAsync(taskFilter, inspectorId));
        }
    }
}

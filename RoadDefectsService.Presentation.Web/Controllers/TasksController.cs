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
    [SwaggerControllerOrder(Order = 10)]
    public class TasksController : BaseController
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        /// <summary>
        /// Посмотреть список задач со статусами
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
        /// Изменить служебные данные задачи (Для переноса данных в электронный вид)
        /// </summary>
        /// <remarks> Доступ: Оператор и админ </remarks>
        /// <response code="204">No Content</response> 
        [HttpPut("{taskId}")]
        [CustomeAuthorize(Roles = Role.Operator)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TaskPagedDTO>> ChangeTask([FromRoute] Guid taskId, [FromBody] EditTaskDTO editTask)
        {
            return await ExecutionResultHandlerAsync(() => _taskService.ChangeTaskAsync(editTask, taskId));
        }

        /// <summary>
        /// Удалить задачу
        /// </summary>
        /// <remarks> Доступ: Оператор и админ </remarks>
        /// <response code="204">No Content</response> 
        [HttpDelete("{taskId}")]
        [CustomeAuthorize(Roles = Role.Operator)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteTask([FromRoute] Guid taskId)
        {
            return await ExecutionResultHandlerAsync(() => _taskService.DeleteTaskAsync(taskId));
        }

        /// <summary>
        /// Задачи дорожного инспектора
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
        /// Назначить задачу дорожному инспектору
        /// </summary>
        /// <remarks> 
        /// Доступ: Оператор и админ 
        /// 
        /// Задачу можно назначить только, когда она имеет статус Created
        /// 
        /// Если задача имеет флаг IsTransfer, то ее можно назначить независимо от того, какой статус она имеет
        /// </remarks>
        /// <response code="204">No Content</response> 
        [HttpPost("inspector/{inspectorId}")]
        [CustomeAuthorize(Roles = Role.Operator)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> AppointTask([FromRoute] Guid inspectorId, [FromBody] AppointTaskDTO appointTask)
        {
            return await ExecutionResultHandlerAsync(() => _taskService.AppointTaskAsync(appointTask.TaskId, inspectorId));
        }

        /// <summary>
        /// Личные задачи
        /// </summary>
        /// <remarks> Доступ: Дорожный инспектор </remarks>
        [HttpGet("own")]
        [CustomeAuthorize(Roles = Role.RoadInspector)]
        [ProducesResponseType(typeof(TaskPagedDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult<TaskPagedDTO>> GetOwnTask([FromQuery] TaskFilterDTO taskFilter)
        {
            return await ExecutionResultHandlerAsync((inspectorId) => _taskService.GetInspectorTasksAsync(taskFilter, inspectorId));
        }

        /// <summary>
        /// Изменить статус задачи (Начать выполнять, Отменить выполнение, Завершить)
        /// </summary>
        /// <remarks> Доступ: Дорожный инспектор </remarks>
        /// <response code="204">No Content</response> 
        [HttpPost("{taskId}")]
        [CustomeAuthorize(Roles = Role.RoadInspector)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> ChangeStatusTask([FromRoute] Guid taskId, [FromQuery] ChangeTaskStatusDTO changeStatus)
        {
            return await ExecutionResultHandlerAsync(() => _taskService.ChangeTaskStatusAsync(changeStatus, taskId));
        }
    }
}

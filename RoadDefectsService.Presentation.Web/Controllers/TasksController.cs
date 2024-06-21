using MediatR;
using Microsoft.AspNetCore.Mvc;
using RoadDefectsService.Presentation.Web.Controllers.Base;
using RoadDefectsService.Presentation.Web.DTOs;
using RoadDefectsService.Presentation.Web.Attributes;
using RoadDefectsService.Core.Domain.Enums;
using RoadDefectsService.Core.Application.CQRS.Task.DTOs;
using RoadDefectsService.Core.Application.CQRS.Task.Queries;
using RoadDefectsService.Core.Application.CQRS.Task.Commands;

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
        private readonly IMediator _mediator;

        public TasksController(IMediator mediator)
        {
            _mediator = mediator;
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
            return await ExecutionResultHandlerAsync(() 
                => _mediator.Send(new GetTasksByFiltersQuery() { TaskFilter = taskFilter }));
        }

        /// <summary>
        /// Изменить служебные данные задачи (Для переноса данных в электронный вид)
        /// </summary>
        /// <remarks> Доступ: Оператор и админ </remarks>
        /// <response code="204">No Content</response> 
        [HttpPut("{taskId}")]
        [CustomeAuthorize(Roles = Role.Operator)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TaskPagedDTO>> EditTaskMetaInfo([FromRoute] Guid taskId, [FromBody] EditTaskMetaInfoDTO editTask)
        {
            return await ExecutionResultHandlerAsync(() 
                => _mediator.Send(new EditTaskMetaInfoCommand() { TaskId = taskId, EditTaskMetaInfo = editTask}));
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
            return await ExecutionResultHandlerAsync(() 
                => _mediator.Send(new DeleteTaskCommand() { TaskId = taskId }));
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
            return await ExecutionResultHandlerAsync(() 
                => _mediator.Send(new GetInspectorTasksByFiltersQuery() { RoadInspectorId = inspectorId, TaskFilter = taskFilter}));
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
            return await ExecutionResultHandlerAsync(() 
                => _mediator.Send(new AppointTaskToRoadInspectorCommand() { TaskId = appointTask.TaskId, RoadInspectorId = inspectorId}));
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
            return await ExecutionResultHandlerAsync((inspectorId)
                => _mediator.Send(new GetInspectorTasksByFiltersQuery() { RoadInspectorId = inspectorId, TaskFilter = taskFilter }));
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
            return await ExecutionResultHandlerAsync(() 
                => _mediator.Send(new ChangeTaskStatusCommand() { TaskId = taskId, ChangeTaskStatus = changeStatus.ChangeTaskStatus }));
        }
    }
}

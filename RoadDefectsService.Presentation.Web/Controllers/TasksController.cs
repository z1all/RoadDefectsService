using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoadDefectsService.Presentation.Web.Controllers.Base;
using RoadDefectsService.Core.Domain.Enums;

namespace RoadDefectsService.Presentation.Web.Controllers
{
    [Route("api/task")]
    [ApiController]
    [Authorize(Roles = Role.Operator)]
    public class TasksController : BaseController
    {
        /// <summary>
        /// Посмотреть список задач со статусами (Не реализовано)
        /// </summary>
        /// <remarks> Доступ: Оператор и админ </remarks>
        [HttpGet("tasks")]
        public async Task<ActionResult> GetTasks()
        {
            return Ok();
        }

        /// <summary>
        /// Посмотреть задачу (Не реализовано)
        /// </summary>
        /// <remarks> Доступ: Оператор и админ </remarks>
        [HttpGet("{taskId}")]
        public async Task<ActionResult> GetTask([FromRoute] Guid taskId)
        {
            return Ok();
        }

        /// <summary>
        /// Редактировать задачу (Не реализовано)
        /// </summary> 
        /// <remarks> Доступ: Оператор и админ </remarks>
        [HttpPut("{taskId}")]
        public async Task<ActionResult> ChangeTask([FromRoute] Guid taskId)
        {
            return Ok();
        }

        /// <summary>
        /// Создать задачу (Не реализовано)
        /// </summary>
        /// <remarks> Доступ: Оператор и админ </remarks>
        [HttpPost]
        public async Task<ActionResult> CreateTask()
        {
            return Ok();
        }

        /// <summary>
        /// Задачи дорожного инспектора (Не реализовано)
        /// </summary>
        /// <remarks> Доступ: Оператор и админ </remarks>
        [HttpGet("inspector/{inspectorId}")]
        public async Task<ActionResult> GetInspectorTasks([FromRoute] Guid inspectorId)
        {
            return Ok();
        }

        /// <summary>
        /// Назначить задачу дорожному инспектору (Не реализовано)
        /// </summary>
        /// <remarks> Доступ: Оператор и админ </remarks>
        [HttpPost("inspector/{inspectorId}")]
        public async Task<ActionResult> AppointTask([FromRoute] Guid inspectorId)
        {
            return Ok();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using RoadDefectsService.Core.Application.DTOs.TaskService;
using RoadDefectsService.Core.Application.Interfaces.Services;
using RoadDefectsService.Core.Domain.Enums;
using RoadDefectsService.Presentation.Web.Attributes;
using RoadDefectsService.Presentation.Web.Controllers.Base;
using RoadDefectsService.Presentation.Web.DTOs;

namespace RoadDefectsService.Presentation.Web.Controllers
{
    [Route("api/fixation_defect")]
    [ApiController]
    public class TaskFixationDefectController : BaseController
    {
        private readonly ITaskFixationDefectService _taskFixationDefectService;

        public TaskFixationDefectController(ITaskFixationDefectService taskFixationDefectService)
        {
            _taskFixationDefectService = taskFixationDefectService;
        }

        /// <summary>
        /// Посмотреть задачу
        /// </summary>
        /// <remarks> 
        /// Доступ: Все 
        /// 
        /// Инспектор может посмотреть только свои задачи
        /// </remarks>
        [HttpGet("{taskId}")]
        [CustomeAuthorize]
        [ProducesResponseType(typeof(FixationDefectTaskDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FixationDefectTaskDTO>> GetFixationDefectTask([FromRoute] Guid taskId)
        {
            if (HttpContext.User.IsInRole(Role.RoadInspector))
            {
                return await ExecutionResultHandlerAsync((userId) => _taskFixationDefectService.GetFixationDefectTaskAsync(taskId, userId));
            }
            return await ExecutionResultHandlerAsync(() => _taskFixationDefectService.GetFixationDefectTaskAsync(taskId));
        }

        /// <summary>
        /// Редактировать задачу
        /// </summary> 
        /// <remarks> Доступ: Оператор и админ </remarks>
        [HttpPut("{taskId}")]
        [CustomeAuthorize(Roles = Role.Operator)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> ChangeFixationDefectTask([FromRoute] Guid taskId, [FromBody] CreateEditFixationDefectTaskDTO editFixationDefect)
        {
            return await ExecutionResultHandlerAsync(() => _taskFixationDefectService.EditFixationDefectTaskAsync(editFixationDefect, taskId));
        }

        /// <summary>
        /// Создать задачу
        /// </summary>
        /// <remarks> Доступ: Оператор и админ </remarks>
        [HttpPost]
        [CustomeAuthorize(Roles = Role.Operator)]
        [ProducesResponseType(typeof(CreateTaskResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreateTaskResponseDTO>> CreateFixationDefectTask([FromBody] CreateEditFixationDefectTaskDTO createFixationDefect)
        {
            return await ExecutionResultHandlerAsync(() => _taskFixationDefectService.CreateFixationDefectTaskAsync(createFixationDefect));
        }
    }
}

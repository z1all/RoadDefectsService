using Microsoft.AspNetCore.Mvc;
using RoadDefectsService.Core.Application.DTOs.Common;
using RoadDefectsService.Core.Application.DTOs.UserService;
using RoadDefectsService.Core.Application.Interfaces.Services;
using RoadDefectsService.Core.Domain.Enums;
using RoadDefectsService.Presentation.Web.Attributes;
using RoadDefectsService.Presentation.Web.Controllers.Base;
using RoadDefectsService.Presentation.Web.DTOs;

namespace RoadDefectsService.Presentation.Web.Controllers
{
    /// <response code="401">Unauthorized</response>
    [Route("api/user")]
    [ApiController]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    [SwaggerControllerOrder(Order = 2)]
    public class UserController : BaseController
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Все пользователи системы
        /// </summary>
        /// <remarks> 
        /// Доступ: Оператор и админ 
        /// 
        /// Оператор видит только дорожных инспекторов
        /// 
        /// Админ всех пользователей
        /// </remarks>
        [HttpGet("users")]
        [CustomeAuthorize(Roles = Role.Operator)]
        [ProducesResponseType(typeof(UserPagedDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult<UserPagedDTO>> GetUsers([FromQuery] UserFilterDTO userFilter)
        {
            bool isAdmin = HttpContext.User.IsInRole(Role.Admin);

            return await ExecutionResultHandlerAsync(() => _userService.GetUsersAsync(userFilter, showOperators: isAdmin));
        }

        /// <summary>
        /// Конкретный пользователь
        /// </summary>
        /// <remarks> 
        /// Доступ: Оператор и админ 
        /// 
        /// Оператор видит только дорожных инспекторов
        /// 
        /// Админ всех пользователей
        /// </remarks>
        /// <response code="204">No Content</response>
        /// <response code="403">Forbidden</response>
        [HttpGet("{userId}")]
        [CustomeAuthorize(Roles = Role.Operator)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserInfoDTO>> GetUser([FromRoute] Guid userId)
        {
            bool isAdmin = HttpContext.User.IsInRole(Role.Admin);

            return await ExecutionResultHandlerAsync(() => _userService.GetUserAsync(userId, showAdmins: isAdmin));
        }


        /// <summary>
        /// Изменить профиль пользователя
        /// </summary>
        /// <remarks> 
        /// Доступ: Оператор и админ 
        /// 
        /// Оператор может изменять только дорожных инспекторов
        /// 
        /// Админ всех пользователей, кроме других админов
        /// </remarks>
        /// <response code="403">Forbidden</response>
        /// <response code="204">No Content</response>
        [HttpPut("{userId}")]
        [CustomeAuthorize(Roles = Role.Operator)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> ChangeUser([FromRoute] Guid userId, [FromBody] EditUserDTO editUser)
        {
            bool isAdmin = HttpContext.User.IsInRole(Role.Admin);

            return await ExecutionResultHandlerAsync(() => _userService.EditUserAsync(editUser, userId, editOperator: isAdmin));
        }

        /// <summary>
        /// Удалить пользователя
        /// </summary>
        /// <remarks> 
        /// Доступ: Оператор и админ 
        /// 
        /// Оператор может удалить только дорожных инспекторов
        /// 
        /// Админ всех пользователей, кроме других админов
        /// </remarks>
        /// <response code="204">No Content</response>
        /// <response code="403">Forbidden</response>
        [HttpDelete("{userId}")]
        [CustomeAuthorize(Roles = Role.Operator)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteUser([FromRoute] Guid userId)
        {
            bool isAdmin = HttpContext.User.IsInRole(Role.Admin);

            return await ExecutionResultHandlerAsync(() => _userService.DeleteUserAsync(userId, deleteOperator: isAdmin));
        }

        /// <summary>
        /// Создать дорожного инспектора
        /// </summary>
        /// <remarks> Доступ: Оператор и админ </remarks>
        /// <response code="204">No Content</response>
        [HttpPost("road_inspector")]
        [CustomeAuthorize(Roles = Role.Operator)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateRoadInspector([FromBody] CreateUserDTO createRoadInspector)
        {
            return await ExecutionResultHandlerAsync(() => _userService.CreateRoadInspectorAsync(createRoadInspector));
        }

        /// <summary>
        /// Создать оператора
        /// </summary>
        /// <remarks> Доступ: Админ </remarks>
        /// <response code="204">No Content</response>
        [HttpPost("operator")]
        [CustomeAuthorize(Roles = Role.Admin)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateOperator([FromBody] CreateUserDTO createOperator)
        {
            return await ExecutionResultHandlerAsync(() => _userService.CreateOperatorAsync(createOperator));
        }
    }
}

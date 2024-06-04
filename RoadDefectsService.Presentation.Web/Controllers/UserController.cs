using Microsoft.AspNetCore.Mvc;
using RoadDefectsService.Core.Application.DTOs.UserService;
using RoadDefectsService.Core.Application.Interfaces.Services;
using RoadDefectsService.Core.Domain.Enums;
using RoadDefectsService.Presentation.Web.Attributes;
using RoadDefectsService.Presentation.Web.Controllers.Base;

namespace RoadDefectsService.Presentation.Web.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : BaseController
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Все пользователи системы (Не реализовано) 
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
        public async Task<ActionResult<List<UserInfoDTO>>> GetUsers([FromQuery] UserFilterDTO userFilter)
        {
            bool isAdmin = HttpContext.User.IsInRole(Role.Admin);

            return await ExecutionResultHandlerAsync(() => _userService.GetUsersAsync(userFilter, showOperators: isAdmin));
        }

        /// <summary>
        /// Изменить профиль пользователя (Не реализовано) 
        /// </summary>
        /// <remarks> 
        /// Доступ: Оператор и админ 
        /// 
        /// Оператор может изменять только дорожных инспекторов
        /// 
        /// Админ всех пользователей, кроме других админов
        /// </remarks>
        [HttpPut("{userId}")]
        [CustomeAuthorize(Roles = Role.Operator)]
        public async Task<ActionResult> ChangeUser([FromRoute] Guid userId, [FromBody] EditUserDTO editUser)
        {
            bool isAdmin = HttpContext.User.IsInRole(Role.Admin);

            return await ExecutionResultHandlerAsync(() => _userService.EditUserAsync(editUser, userId, editOperator: isAdmin));
        }

        /// <summary>
        /// Удалить пользователя (Не реализовано) 
        /// </summary>
        /// <remarks> 
        /// Доступ: Оператор и админ 
        /// 
        /// Оператор может удалить только дорожных инспекторов
        /// 
        /// Админ всех пользователей, кроме других админов
        /// </remarks>
        [HttpDelete("{userId}")]
        [CustomeAuthorize(Roles = Role.Operator)]
        public async Task<ActionResult> DeleteUser([FromRoute] Guid userId)
        {
            bool isAdmin = HttpContext.User.IsInRole(Role.Admin);

            return await ExecutionResultHandlerAsync(() => _userService.DeleteUserAsync(userId, deleteOperator: isAdmin));
        }

        /// <summary>
        /// Создать дорожного инспектора (Не реализовано) 
        /// </summary>
        /// <remarks> Доступ: Оператор и админ </remarks>
        [HttpPost("road_inspector")]
        [CustomeAuthorize(Roles = Role.Operator)]
        public async Task<ActionResult> CreateRoadInspector([FromBody] CreateUserDTO createRoadInspector)
        {
            return await ExecutionResultHandlerAsync(() => _userService.CreateRoadInspectorAsync(createRoadInspector));
        }

        /// <summary>
        /// Создать оператора (Не реализовано)
        /// </summary>
        /// <remarks> Доступ: Админ </remarks>
        [HttpPost("operator")]
        [CustomeAuthorize(Roles = Role.Admin)]
        public async Task<ActionResult> CreateOperator([FromBody] CreateUserDTO createOperator)
        {
            return await ExecutionResultHandlerAsync(() => _userService.CreateOperatorAsync(createOperator));
        }
    }
}

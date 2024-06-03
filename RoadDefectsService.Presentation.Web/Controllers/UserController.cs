using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoadDefectsService.Core.Domain.Enums;
using RoadDefectsService.Presentation.Web.Attributes;
using RoadDefectsService.Presentation.Web.Controllers.Base;

namespace RoadDefectsService.Presentation.Web.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : BaseController
    {
        /// <summary>
        /// Все пользователи системы (Не реализовано) (Не все модели указаны)
        /// </summary>
        /// <remarks> 
        /// Доступ: Оператор и админ 
        /// 
        /// Оператор видит только дорожных инспекторов
        /// Админ всех пользователей
        /// </remarks>
        [HttpGet("users")]
        [CustomeAuthorize(Roles = Role.Operator)]
        public async Task<ActionResult> GetUsers()
        {
            return Ok();
        }

        /// <summary>
        /// Конкретный пользователь (Не реализовано) (Не все модели указаны)
        /// </summary>
        /// <remarks> 
        /// Доступ: Оператор и админ 
        /// 
        /// Оператор может смотреть только дорожных инспекторов
        /// Админ всех пользователей
        /// </remarks>
        [HttpGet("{userId}")]
        [CustomeAuthorize(Roles = Role.Operator)]
        public async Task<ActionResult> GetUser([FromRoute] Guid userId)
        {
            return Ok();
        }

        /// <summary>
        /// Создать дорожного инспектора (Не реализовано) (Не все модели указаны)
        /// </summary>
        /// <remarks> Доступ: Оператор и админ </remarks>
        [HttpPost("road_inspector")]
        [CustomeAuthorize(Roles = Role.Operator)]
        public async Task<ActionResult> CreateRoadInspector()
        {
            return Ok();
        }

        /// <summary>
        /// Изменить профиль дорожного инспектора (Не реализовано) (Не все модели указаны)
        /// </summary>
        /// <remarks> Доступ: Оператор и админ </remarks>
        [HttpPut("road_inspector")]
        [CustomeAuthorize(Roles = Role.Operator)]
        public async Task<ActionResult> ChangeRoadInspector()
        {
            return Ok();
        }

        /// <summary>
        /// Создать оператора (Не реализовано) (Не все модели указаны)
        /// </summary>
        /// <remarks> Доступ: Админ </remarks>
        [HttpPost("operator")]
        [CustomeAuthorize(Roles = Role.Admin)]
        public async Task<ActionResult> CreateOperator()
        {
            return Ok();
        }

        /// <summary>
        /// Изменить профиль оператора (Не реализовано) (Не все модели указаны)
        /// </summary>
        /// <remarks> Доступ: Админ </remarks>
        [HttpPut("operator")]
        [CustomeAuthorize(Roles = Role.Admin)]
        public async Task<ActionResult> ChangeOperator()
        {
            return Ok();
        }
    }
}

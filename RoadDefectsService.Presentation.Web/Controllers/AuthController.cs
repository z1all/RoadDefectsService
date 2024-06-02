using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoadDefectsService.Core.Application.DTOs;
using RoadDefectsService.Presentation.Web.Controllers.Base;
using RoadDefectsService.Presentation.Web.DTOs;

namespace RoadDefectsService.Presentation.Web.Controllers
{
    [Route("api/auth")]
    [ApiController]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public class AuthController : BaseController
    {
        /// <summary>
        /// (Не реализовано)
        /// </summary>
        /// <remarks> Доступ: Все </remarks>
        [HttpPost("login")]
        [ProducesResponseType(typeof(TokenResponseDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult<TokenResponseDTO>> Login([FromBody] LoginDTO login)
        {
            return Ok();
        }

        /// <summary>
        /// (Не реализовано)
        /// </summary>
        /// <remarks> Доступ: Все </remarks>
        /// <response code="204">NoContent</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        [HttpPost("logout")]
        [Authorize]
        public async Task<ActionResult> Logout()
        {
            return NoContent();
        }
    }
}

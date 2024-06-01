using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoadDefectsService.Core.Application.DTOs;
using RoadDefectsService.Presentation.Web.DTOs;

namespace RoadDefectsService.Presentation.Web.Controllers
{
    /// <response code="401">Unauthorized</response>
    [Route("api/profile")]
    [ApiController]
    [Authorize]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public class ProfileController : ControllerBase
    {
        /// <summary>
        /// (Не реализовано)
        /// </summary>
        /// <remarks> Доступ: Все </remarks>
        [HttpGet]
        [ProducesResponseType(typeof(ProfileDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult<ProfileDTO>> GetProfile()
        {
            return Ok();
        }

        /// <summary>
        /// (Не реализовано)
        /// </summary>
        /// <remarks> Доступ: Все </remarks>
        /// <response code="204">NoContent</response>
        [HttpPut]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> ChangeProfile([FromBody] ProfileDTO profile)
        {
            return NoContent();
        }
    }
}

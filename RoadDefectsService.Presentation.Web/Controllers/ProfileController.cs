using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RoadDefectsService.Presentation.Web.Controllers
{
    [Route("api/profile")]
    [ApiController]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        /// <summary>
        /// (Не реализовано)
        /// </summary>
        /// <remarks> Доступ: Все </remarks>
        [HttpGet]
        public async Task<ActionResult> GetProfile()
        {
            return Ok();
        }

        /// <summary>
        /// (Не реализовано)
        /// </summary>
        /// <remarks> Доступ: Все </remarks>
        [HttpPut]
        public async Task<ActionResult> ChangeProfile()
        {
            return Ok();
        }
    }
}

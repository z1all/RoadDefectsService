using Microsoft.AspNetCore.Mvc;
using RoadDefectsService.Core.Application.DTOs.Common;
using RoadDefectsService.Core.Application.DTOs.ProfileService;
using RoadDefectsService.Core.Application.Interfaces.Services;
using RoadDefectsService.Presentation.Web.Attributes;
using RoadDefectsService.Presentation.Web.Controllers.Base;
using RoadDefectsService.Presentation.Web.DTOs;

namespace RoadDefectsService.Presentation.Web.Controllers
{
    /// <response code="401">Unauthorized</response>
    [Route("api/profile")]
    [ApiController]
    [CustomeAuthorize]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public class ProfileController : BaseController
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        /// <summary>
        /// (Реализовано)
        /// </summary>
        /// <remarks> Доступ: Все </remarks>
        [HttpGet]
        [ProducesResponseType(typeof(UserInfoDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult<UserInfoDTO>> GetProfile()
        {
            return await ExecutionResultHandlerAsync(_profileService.GetProfileInfoAsync);
        }

        /// <summary>
        /// (Реализовано)
        /// </summary>
        /// <remarks> Доступ: Все </remarks>
        /// <response code="204">NoContent</response>
        [HttpPut]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> ChangeProfile([FromBody] EditProfileDTO editProfile)
        {
            return await ExecutionResultHandlerAsync((userId) => _profileService.EditProfileInfoAsync(editProfile, userId));
        }

        /// <summary>
        /// (Реализовано)
        /// </summary>
        /// <remarks> Доступ: Все </remarks>
        /// <response code="204">NoContent</response>
        [HttpPut("password")]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordDTO changePassword)
        {
            return await ExecutionResultHandlerAsync((userId) => _profileService.ChangePasswordAsync(changePassword, userId));
        }
    }
}

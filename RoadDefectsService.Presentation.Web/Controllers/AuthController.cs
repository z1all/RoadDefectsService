using Microsoft.AspNetCore.Mvc;
using RoadDefectsService.Core.Application.DTOs.AuthService;
using RoadDefectsService.Core.Application.Interfaces.Services;
using RoadDefectsService.Core.Application.Models;
using RoadDefectsService.Presentation.Web.Attributes;
using RoadDefectsService.Presentation.Web.Controllers.Base;
using RoadDefectsService.Presentation.Web.DTOs;
using RoadDefectsService.Presentation.Web.Helpers;

namespace RoadDefectsService.Presentation.Web.Controllers
{
    [Route("api/auth")]
    [ApiController]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    [SwaggerControllerOrder(Order = 0)]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks> 
        /// Доступ: Все 
        ///
        /// Данные админов
        /// 
        ///     {
        ///         "email": "admin1@gmail.com",
        ///         "password": "stringA1"
        ///     }
        ///     
        ///     {
        ///         "email": "admin2@gmail.com",
        ///         "password": "stringA2"
        ///     }
        ///     
        ///     {
        ///         "email": "admin3@gmail.com",
        ///         "password": "stringA3"
        ///     }
        /// </remarks>
        [HttpPost("login")]
        [ProducesResponseType(typeof(TokenResponseDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult<TokenResponseDTO>> Login([FromBody] LoginDTO login)
        {
            return await ExecutionResultHandlerAsync(() => _authService.LoginAsync(login));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks> Доступ: Все </remarks>
        /// <response code="204">NoContent</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        [HttpPost("logout")]
        [CustomeAuthorize]
        public async Task<ActionResult> Logout()
        {
            if (!HttpContext.TryGetAccessTokenJTI(out Guid accessTokenJTI))
            {
                return ExecutionResultHandler(new ExecutionResult(StatusCodeExecutionResult.InternalServer, "UnknowError", "Unknow error"));
            }

            return await ExecutionResultHandlerAsync(() => _authService.LogoutAsync(accessTokenJTI));
        }
    }
}
